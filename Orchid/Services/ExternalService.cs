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
        private static string ExtAPI = "https://www.dnd5eapi.co/2014/";
        private HttpClient client;
        public ExternalService()
        {
            HttpClientHandler handler = new();
            handler.CookieContainer = new System.Net.CookieContainer();
            this.client = new HttpClient(handler);
        }

        //function
        //removes ' and spaces in text to fine the appropriate table or column in the db
        public static string ConverterToDb(string text)
        {
            text.Replace("'", "");
            text.Replace(" ", "");
            return text;
        }

        //function
        //turn a string, Example:"skill-arcana" to a list of Proficiencies
        public static string ParseProficiencies(string text)
        {
            string newtext;
            newtext = text.Remove(0,6);
            newtext = newtext.Replace("-", "_");
            return newtext;
        }


        //function
        //get every class in the api
        public async Task<List<string>> GetClasses()
        {
            string url = ExtAPI + "api/classes";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ClassRootobject result = JsonSerializer.Deserialize<ClassRootobject>(resContent);
                    List<ClassRootResults> cList = result.results.ToList();
                    List<string> classes = new List<string>();
                    foreach (ClassRootResults c in cList)
                    {
                        classes.Add(c.index);
                    }
                    return classes;
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

        //function
        //get every Race in the api
        public async Task<List<string>> GetRaces()
        {
            string url = ExtAPI + "api/races";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    RaceRootobject result = JsonSerializer.Deserialize<RaceRootobject>(resContent);
                    List<RaceRootResult> rList = result.results.ToList();
                    List<string> races = new List<string>();
                    foreach (RaceRootResult r in rList)
                    {
                        races.Add(r.index);
                    }
                    return races;
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



        //function
        //get the details of a given class
        public async Task<ExtApiClass> GetClassDetails(string Class)
        {
            string url = ExtAPI + "api/classes/" + Class;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ExtApiClass result = JsonSerializer.Deserialize<ExtApiClass>(resContent);
                    return result;
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


        //function
        //get every feat in the api
        public async Task<List<string>> GetFeats()
        {
            string url = ExtAPI + "api/feats";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    FeatRootobject result = JsonSerializer.Deserialize<FeatRootobject>(resContent);
                    List<FeatRootResults> fList = result.results.ToList();
                    List<string> feats = new List<string>();
                    foreach (FeatRootResults f in fList)
                    {
                        feats.Add(f.index);
                    }
                    return feats;
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

        //function
        //get the details of a given feat
        public async Task<ExtApifeat> GetFeatDetails(string feat)
        {
            string url = ExtAPI + "api/feats/" + feat;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ExtApifeat result = JsonSerializer.Deserialize<ExtApifeat>(resContent);
                    return result;
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


        public async Task<List<MyExtApiSpell>> GetSpells()
        {
            string url = ExtAPI + "api/spells/";
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
                            desc = await GetSpellDesc(s.url)
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
        public async Task<string> GetSpellDesc(string url)
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

        //function
        //get every skill of a given class
        public async Task<(List<string> list,int count)> GetSkills(Class item)
        {
            string url = ExtAPI + "api/classes/" + item.ClassName;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    //ClassRootobject result = JsonSerializer.Deserialize<ClassRootobject>(resContent);
                    ExtApiClass c = JsonSerializer.Deserialize<ExtApiClass>(resContent);
                    List<Option> partofoptions = c.proficiency_choices.LastOrDefault().from.options.ToList();
                    List<string> skills = new List<string>();
                    foreach (Option i in partofoptions)
                    {
                        skills.Add(ParseProficiencies(i.item.index));
                    }
                    int count = c.proficiency_choices.LastOrDefault().choose;
                    return (skills,count);
                }
                else
                {
                    return (null,0);
                }
            }
            catch (Exception ex)
            {
                return (null, 0);
            }
        }

    }
}
