using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Model;
using Newtonsoft.Json;

namespace ConsumeREST
{
    internal class ConsumeWorker
    {

        private const string URI = "https://itemrest.azurewebsites.net/api/localitems/";

        internal async void Start()
        {
            List<Item> allItems = await GetAllItems();
            foreach (Item i in allItems)
            {
                Console.WriteLine(i);
            }

            //Item singleItem = await GetOneItemAsync(1);
            //Console.WriteLine(singleItem);

            try
            {
                Item item1 = await GetOneItemAsync(1);
                Console.WriteLine(item1);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }
            PostNewItem();
            UpdateItem();
        }

        public async Task<List<Item>> GetAllItems()
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(URI);
                List <Item> iList = JsonConvert.DeserializeObject<List<Item>>(json);

                return iList;
            }
        }

        public async Task<Item> GetOneItemAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI + id);

                if (resp.IsSuccessStatusCode) // all 2x status codes
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    Item sItem = JsonConvert.DeserializeObject<Item>(json);
                    return sItem;
                }
                    throw new KeyNotFoundException($"Status code={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }
        }

        public async void PostNewItem()
        {
            using (HttpClient client = new HttpClient())
            {
                Item item = new Item(23, "Fedt item", "Super kvali", 9.9);
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                HttpResponseMessage resp = await client.PostAsync(URI, content);
                if (resp.IsSuccessStatusCode)
                {
                    return;
                }
                throw new ArgumentException("oprettelse fejlede");
            }
        }

        public async void UpdateItem()
        {
            using (HttpClient client = new HttpClient())
            {
                Item item = new Item(23, "nyt opdateret item", "mindre super kvali", 9.9);
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                HttpResponseMessage resp = await client.PutAsync(URI, content);
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        return;
                    }
                    throw new ArgumentException("opdatering fejlede");
                }
            }
        }
    }
}