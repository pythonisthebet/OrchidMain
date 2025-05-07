using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Orchid.Models;
using iText.Forms;
using iText.Kernel.Pdf;
using Microsoft.Maui;
using Microsoft.Maui.Storage;
using System.Resources;
//using GameController;
//using PdfKit;

namespace Orchid.Services
{
    public interface IPdfService
    {
        Task<string> FillCharacterSheet(CharacterData characterData);
    }

    public class PdfService : IPdfService
    {
        // Map ability score indexes to their names
        private readonly Dictionary<int, string> _abilityScores = new Dictionary<int, string>
        {
            { 0, "Strength" },
            { 1, "Dexterity" },
            { 2, "Constitution" },
            { 3, "Intelligence" },
            { 4, "Wisdom" },
            { 5, "Charisma" }
        };

        public async Task<string> FillCharacterSheet(CharacterData characterData)
        {
            // Path to your template PDF (you would need to include this in your app resources)
            string templatePath = @"Orchid\Resources\PdfTemplate\DnD_5E_CharacterSheet_FormFillable.pdf";

            // Path for the filled PDF
            string outputPath = Path.Combine(FileSystem.CacheDirectory, "character_sheet.pdf");

            // If we're on a platform where the template isn't embedded as a resource, create a placeholder
            if (!File.Exists(templatePath))
            {
                // In a real application, you'd bundle the template with your app
                await LoadTemplateFromResources(templatePath);
            }

            // Fill the PDF with character data
            using (PdfReader reader = new PdfReader(templatePath))
            {
                using (PdfWriter writer = new PdfWriter(outputPath))
                {
                    using (iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(reader, writer))
                    {
                        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);

                        // Character class and level
                        string classAndLevel = "";
                        for (int i = 0; i < characterData.character.Classes.Count; i++)
                        {
                            classAndLevel += $"{characterData.character.Classes[i]} {characterData.character.ClassLevels[i]}";
                            if (i < characterData.character.Classes.Count - 1)
                                classAndLevel += ", ";
                        }
                        SetFieldValue(form, "ClassLevel", classAndLevel);

                        // Ability scores
                        foreach (var score in characterData.character.Scores)
                        {
                            if (_abilityScores.TryGetValue(score.Key, out string abilityName))
                            {
                                string PdfAbilityName = abilityName.Length >= 3 ? abilityName.Substring(0, 3).ToUpper() : abilityName.ToUpper();
                                SetFieldValue(form, PdfAbilityName, score.Value.ToString());
                                // Also calculate and fill the modifier
                                int modifier = (score.Value - 10) / 2;
                                SetFieldValue(form, $"{PdfAbilityName}mod", modifier.ToString(modifier >= 0 ? "+#" : "#"));
                            }
                        }

                        // Equipment - combine into a single string
                        string equipment = string.Join(", ", characterData.character.equipment);
                        SetFieldValue(form, "Equipment", equipment);

                        // Spells - combine into a single string
                        bool isAddingSpells = true;
                        int startingId = 1016;
                        int FirstFiveCount = 5;
                        int index = 0;
                        while (isAddingSpells)
                        {
                            if (FirstFiveCount == 0)
                            {
                                startingId += 3;
                            }

                            SetFieldValue(form, $"Spells {startingId}", characterData.character.Spells[index]);
                            index++;
                            FirstFiveCount--;
                            startingId++;
                            if (characterData.character.Spells.Count <= index + 1)
                            {
                                isAddingSpells = false;
                            }
                        }
                        Orchid.Services.OrchidWebAPIProxy temp = new OrchidWebAPIProxy();
                        List<AppUser> temp2 = await temp.GetAllUsers();
                        AppUser temp3 = temp2.First(u => u.Id == characterData.Uid);
                        List<Character> character = await temp.GetAllCharacters(temp3);
                        Character realcharacter = character.First(u => u.Id == characterData.Cid);
                        string name = realcharacter.CharacterName;

                        // Character ID
                        SetFieldValue(form, "CharacterName", $"{name}");
                    }
                }
            }

            return outputPath;
        }

        private void SetFieldValue(PdfAcroForm form, string fieldName, string value)
        {
            if (form.GetField(fieldName) != null)
            {
                form.GetField(fieldName).SetValue(value);
            }
        }

        private async Task LoadTemplateFromResources(string targetPath)
        {
            try
            {
                // Try to load template from app resources
                using var stream = await FileSystem.OpenAppPackageFileAsync("DnD_5E_CharacterSheet_FormFillable.pdf");
                using var fileStream = File.Create(targetPath);
                await stream.CopyToAsync(fileStream);
            }
            catch
            {
                // If that fails, create a simple placeholder PDF
                CreatePlaceholderPdf(targetPath);
            }
        }

        private void CreatePlaceholderPdf(string path)
        {
            // In a real app, you would include your template as an embedded resource
            // This is just a placeholder for demonstration purposes
            //using (PdfWriter writer = new PdfWriter(path))
            //{
            //    using (iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer))
            //    {
            //        iText.Layout.Document document = new iText.Layout.Document(pdf);
            //        document.Add(new iText.Layout.Element.Paragraph("D&D Character Sheet Template"));

            //        // Add some form fields so we can test the functionality
            //        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);

            //        // Add class level field
            //        form.AddTextField("ClassLevel", 100, 700, 400, 30);
            //        document.Add(new iText.Layout.Element.Paragraph("Class & Level:"));

            //        // Add ability score fields
            //        int yPosition = 650;
            //        foreach (var ability in _abilityScores.Values)
            //        {
            //            form.AddTextField(ability, 100, yPosition, 50, 30);
            //            form.AddTextField($"{ability}Mod", 160, yPosition, 50, 30);
            //            document.Add(new iText.Layout.Element.Paragraph(ability).SetFixedPosition(20, yPosition + 10, 100));
            //            yPosition -= 40;
            //        }

            //        // Add equipment field
            //        form.AddTextField("Equipment", 100, 400, 400, 100);
            //        document.Add(new iText.Layout.Element.Paragraph("Equipment:"));

            //        // Add spells field
            //        form.AddTextField("Spells", 100, 280, 400, 100);
            //        document.Add(new iText.Layout.Element.Paragraph("Spells:"));

            //        // Add character ID field
            //        form.AddTextField("CharacterID", 100, 200, 400, 30);
            //        document.Add(new iText.Layout.Element.Paragraph("Character ID:"));

            //        document.Close();
            //    }
            //}
        }
    }
}