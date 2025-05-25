using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Orchid.Models;
using System.Dynamic;

namespace Orchid.Services
{
    public class OrchidWebAPIProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110" : $"http://{serverIP}:5110";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "zbwv4jk5-5029.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://zbwv4jk5-5029.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://zbwv4jk5-5029.euw.devtunnels.ms/";
        #endregion

        public OrchidWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return OrchidWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{OrchidWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }

        //This method call the Login web API on the server and return the AppUser object with the given Email and Password
        //or null if the call fails
        public async Task<AppUser?> LoginAsync(LoginInfo userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    AppUser? result = JsonSerializer.Deserialize<AppUser>(resContent, options);
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

        //This method call the Register web API on the server and return the AppUser object with the given ID
        //or null if the call fails
        public async Task<AppUser?> Register(AppUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}register";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    AppUser? result = JsonSerializer.Deserialize<AppUser>(resContent, options);
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

        //This method call the updateUser web API on the server and return a boolean if the change was a seccess
        //or null if the call fails
        public async Task<bool> UpdateAppUser(AppUser u)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateUser";
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<AppUser>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //This method call the GetAllUsers web API on the server and return a all users in DATA BASE as Model.AppUser
        //or null if the call fails
        public async Task<List<AppUser>> GetAllUsers()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getAllUsers";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<AppUser>? result = JsonSerializer.Deserialize<List<AppUser>>(resContent, options);
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

        //This method call the GetAllCharacters web API on the server and return a all users in DATA BASE as Model.AppUser
        //or null if the call fails
        public async Task<List<Character>> GetAllCharacters(AppUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getAllCharacters";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Character>? result = JsonSerializer.Deserialize<List<Character>>(resContent, options);
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

        //This method call the CreateCharacter web API on the server and return the Character object with the given ID
        //or null if the call fails
        public async Task<Character?> CreateCharacter(Character character)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}createCharacter";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(character);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Character item = JsonSerializer.Deserialize<Character>(resContent, options);
                    return item;
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

        //not dynamic
        //#region AddClass
        ////This method call the CreateCharacter web API on the server and return the Character object with the given ID
        ////or null if the call fails
        //public async Task<Class?> AddClass(Class item)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}addClass";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(item);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            Class? result = JsonSerializer.Deserialize<Class>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //#region AddRace
        ////This method call the CreateCharacter web API on the server and return the Character object with the given ID
        ////or null if the call fails
        //public async Task<Race?> AddRace(Race item)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}addRace";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(item);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            Race? result = JsonSerializer.Deserialize<Race>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //#region AddSkills
        ////This method call the CreateCharacter web API on the server and return the Character object with the given ID
        ////or null if the call fails
        //public async Task<ProficienciesSkill?> AddSkills(Character character,ProficienciesSkill skill)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}addSkills";
        //    try
        //    {
        //        //Call the server API
        //        (Character character, ProficienciesSkill skill) tuple = (character, skill);
        //        string json = JsonSerializer.Serialize(tuple);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            ProficienciesSkill? result = JsonSerializer.Deserialize<ProficienciesSkill>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //#region RemoveClasses
        ////This method call the CreateCharacter web API on the server and return the Character object with the given ID
        ////or null if the call fails
        //public async Task<Class?> RemoveClasses(Character character)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}removeClasses";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(character);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            Class? result = JsonSerializer.Deserialize<Class>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //#region RemoveRace
        ////This method call the CreateCharacter web API on the server and return the Character object with the given ID
        ////or null if the call fails
        //public async Task<Race?> RemoveRace(Character character)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}removeRaces";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(character);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            Race? result = JsonSerializer.Deserialize<Race>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //#region RemoveSkills
        ////This method call the CreateCharacter web API on the server and return the Character object with the given ID
        ////or null if the call fails
        //public async Task<Skill?> RemoveSkills(Character character)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}removeSkills";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(character);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            Skill? result = JsonSerializer.Deserialize<Skill>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //#region GetAllClasses
        ////This method call the GetAllCharacters web API on the server and return a all users in DATA BASE as Model.AppUser
        ////or null if the call fails
        //public async Task<List<Class>> GetAllClasses(Character character)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}getAllClasses";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(character);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            List<Class>? result = JsonSerializer.Deserialize<List<Class>>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        //#region GetAllSkills
        ////This method call the GetAllCharacters web API on the server and return a all users in DATA BASE as Model.AppUser
        ////or null if the call fails
        //public async Task<List<string>> GetAllSkills(Character character)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}getSkills";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(character);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            List<string>? result = JsonSerializer.Deserialize<List<string>>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion
        
        //#region GetRace
        ////This method call the GetRace web API on the server and return a all users in DATA BASE as Model.AppUser
        ////or null if the call fails
        //public async Task<Race> GetRace(Character character)
        //{
        //    //Set URI to the specific function API
        //    string url = $"{this.baseUrl}getRace";
        //    try
        //    {
        //        //Call the server API
        //        string json = JsonSerializer.Serialize(character);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //        //Check status
        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Extract the content as string
        //            string resContent = await response.Content.ReadAsStringAsync();
        //            //Desrialize result
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            Race result = JsonSerializer.Deserialize<Race>(resContent, options);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion

        //This method call the StoreCharacter web API on the server  and Stores a character expando object there
        public async Task StoreCharacter(ExpandoObject character, int Cid, int Uid)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}storeCharacter";
            try
            {
                ExpandoObject ToSend = new ExpandoObject();
                ToSend.TryAdd("character", character);
                ToSend.TryAdd("Uid", Uid);
                ToSend.TryAdd("Cid", Cid);


                //Call the server API
                string json = JsonSerializer.Serialize(ToSend);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        //This method call the GetDynamicCharacter web API on the server and a json object that of the same ID of the given character
        //or null if the call fails
        public async Task<ExpandoObject> GetDynamicCharacter(int Uid,Character character)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getDynamicCharacter";
            try
            {
                //Call the server API
                int id = character.Id;
                ExpandoObject ToSend = new ExpandoObject();

                ToSend.TryAdd("Cid",id);
                ToSend.TryAdd("Uid", Uid);
                string json = JsonSerializer.Serialize(ToSend);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ExpandoObject result = JsonSerializer.Deserialize<ExpandoObject>(resContent, options);
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

        //This method call the GetJsonCharacter web API on the server and a json object that of the same ID of the given character
        //or null if the call fails
        public async Task<string> GetJsonCharacter(int Uid, int Cid)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getJsonCharacter";
            try
            {
                //Call the server API
                ExpandoObject ToSend = new ExpandoObject();

                ToSend.TryAdd("Cid", Cid);
                ToSend.TryAdd("Uid", Uid);
                string json = JsonSerializer.Serialize(ToSend);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    return resContent;
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


        //This method call the GetAllFilters web API on the server and return a all Filters in DATA BASE
        //or null if the call fails
        public async Task<List<Filter>> GetAllFilters()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getAllFilters";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize("");
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Filter>? result = JsonSerializer.Deserialize<List<Filter>>(resContent, options);
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

        //This method call the GetCharactersANDFilters web API on the server and return a all character in DATA BASE
        //or null if the call fails
        public async Task<List<Character>> GetCharactersFORFilters(List<Filter> filters)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getCharactersFORFilters";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(filters);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Character>? result = JsonSerializer.Deserialize<List<Character>>(resContent, options);
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

        //This method call the GetUserId web API on the server and return a a user api based on a cheracter
        //or null if the call fails
        public async Task<int> GetUserId(Character character)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getUserId";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(character);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    int result = JsonSerializer.Deserialize<int>(resContent, options);
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //This method call the updateCharFilters web API on the server and return a boolean if the change was a seccess
        //or null if the call fails
        public async Task<bool> UpdateCharFilters(ChPlusFilters u)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateCharFilters";
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<ChPlusFilters>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //This method call the GetAppeal web API on the server and return an Appeal of the given user
        //or null if the call fails
        public async Task<string> GetAppeal(AppUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getAppeal";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = resContent;
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //This method call the GetBanReason web API on the server and return an BanReason of the given user
        //or null if the call fails
        public async Task<string> GetBanReason(AppUser user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}getBanReason";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = resContent;
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //This method call the UnbanUser web API on the server and return a boolean if the change was a seccess
        //or null if the call fails
        public async Task<bool> UnbanUser(AppUser u)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}unbanUser";
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<AppUser>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //This method call the SetBanReason web API on the server and return the BanReason object with the given ID
        //or null if the call fails
        public async Task<BanReason?> SetBanReason(BanReason banReason)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}setBanReason";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(banReason);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    BanReason? result = JsonSerializer.Deserialize<BanReason>(resContent, options);
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

        //This method call the SetAppeal web API on the server and return the AppUser object with the given ID
        //or null if the call fails
        public async Task<Appeal?> SetAppeal(Appeal appeal)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}setAppeal";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(appeal);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Appeal? result = JsonSerializer.Deserialize<Appeal>(resContent, options);
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
    }
}
