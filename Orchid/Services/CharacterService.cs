using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Orchid.Models;
using Microsoft.Maui.Storage;

namespace Orchid.Services
{
    public interface ICharacterService
    {
        Task<CharacterData> GetCharacterDataAsync();
    }

    public class CharacterService : ICharacterService
    {
        private const string CharacterFileName = "character_data.json";

        //gets the character data from the json file in server
        public async Task<CharacterData> GetCharacterDataAsync()
        {
            try
            {
                // Check if we have saved character data
                int Cid = ((App)Application.Current).CurrentCharacter.Id;
                int Uid = (int)((App)Application.Current).CurrentCharacter.UserId;
                Orchid.Services.OrchidWebAPIProxy temp = new OrchidWebAPIProxy();
                string json = await temp.GetJsonCharacter(Uid,Cid);
                CharacterData data = JsonSerializer.Deserialize<CharacterData>(json);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading character data: {ex.Message}");
                //return CreateSampleCharacter();
            }
            return null;
        }





    }
}
