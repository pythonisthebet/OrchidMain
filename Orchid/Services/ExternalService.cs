using Orchid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Orchid.Services
{
    public class ExternalService
    {
        private static string ExtAPI = "https://www.dnd5eapi.co/";
        private HttpClient client;
        public ExternalService()
        {
            HttpClientHandler handler = new();
            handler.CookieContainer = new System.Net.CookieContainer();
            this.client = new HttpClient(handler);
        }

        public async Task<List<MyExtApiSpell>> GetSpells()
        {
            string url = ExtAPI + "api/spells";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ExtApiSpellRoot result = JsonSerializer.Deserialize<ExtApiSpellRoot>(resContent);
                    List<ExtApiSpell> sList = result.spells.ToList();
                    List<MyExtApiSpell> newList = new();
                    foreach (ExtApiSpell s in sList)
                    {
                        newList.Add(new MyExtApiSpell()
                        {
                            index = s.index,
                            name = s.name,
                            level = s.level,
                            desc = await GetDesc(s.url)
                        });
                    }
                    return newList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> GetDesc(string url)
        {
            string _url = ExtAPI + url;
            try
            {
                HttpResponseMessage response = await client.GetAsync(_url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    SpellInfo result = JsonSerializer.Deserialize<SpellInfo>(resContent);
                    return result.desc.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
