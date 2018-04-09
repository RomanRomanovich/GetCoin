using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GetCoin
{
    class Program
    {
        static void Main(string[] args)
        {

            string web = "https://api.coinmarketcap.com/v1/ticker/";
            WebRequest webRequest = WebRequest.Create(web);
            Stream stream = webRequest.GetResponse().GetResponseStream();
            StreamReader noValidJson = new StreamReader(stream); // получаем не валидный JSON в текстовой кодировке 

            //Создаем валидный JSON
            string tempValidJson = "{\"items\":" + noValidJson.ReadToEnd() + "}"; // создаем валидный JSON 
            string validJson = tempValidJson.Replace("\n", "").Replace(" ", "");
           

            //Создаем класс для десериализации
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(CoinList));
            CoinList allCoin = new CoinList();

            //Создаем поток MemoryStream и помещаем в него валидный JSON
            using (Stream jsonStream = new MemoryStream())
            {
                StreamWriter jsonWriter = new StreamWriter(jsonStream);
                jsonWriter.Write(validJson);
                jsonWriter.Flush();
                //Устанавливаем начальную позицию в потоке
                jsonStream.Position = 0;
                //Десериализуем JSON в экземпляра класса CoinList
                allCoin = (CoinList)jsonSerializer.ReadObject(jsonStream);

            }
            
            
            Console.ReadLine();
        }
    }









    public class CoinList
    {
        public Coin[] items { get; set; }
    }


        public class Coin
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
        public double price_usd { get; set; }
        public double price_btc { get; set; }
        public double _24h_volume_usd { get; set; }
        public double market_cap_usd { get; set; }
        public double available_supply { get; set; }
        public double total_supply { get; set; }
        public string max_supply { get; set; }
        public double percent_change_1h { get; set; }
        public double percent_change_24h { get; set; }
        public double percent_change_7d { get; set; }
        public double last_updated { get; set; }
    }









}
