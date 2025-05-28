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
using System.Diagnostics;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom;
using static iText.IO.Codec.TiffWriter;
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
            await ((App)Application.Current).CheckAndRequestStoragePermission();
            // Create cache directory if it doesn't exist
            if (!Directory.Exists(FileSystem.AppDataDirectory))
            {
                Directory.CreateDirectory(FileSystem.AppDataDirectory);
            }
            // Path to your template PDF (you would need to include this in your app resources)
            string templatePath = await ExtractPdfTemplateToAccessibleLocation();

            // Path for the filled PDF
            string outputPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "character_sheet.pdf");

            // Make sure any existing file is deleted to avoid conflicts
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            //// If we're on a platform where the template isn't embedded as a resource, create a placeholder
            //if (!File.Exists(templatePath))
            //{
            //    // In a real application, you'd bundle the template with your app
            //    await LoadTemplateFromResources(templatePath);
            //}



            // Fill the PDF with character data
            using (PdfReader reader = new PdfReader(templatePath))
            {
                //using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                //{
                using (var writer = new PdfWriter(outputPath))
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
                            List<string> spells = characterData.character.Spells;
                            if (spells.Count != 0)
                            {
                                SetFieldValue(form, $"Spells {startingId}", characterData.character.Spells[index]);
                            }
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
                        if (form != null && ((App)Application.Current).CurrentCharacter.ImgId != "")
                        {
                            var field = form.GetField("CHARACTER IMAGE");

                            if (field != null)
                            {
                                // Load image
                                var imageData = ImageDataFactory.Create($"{((App)Application.Current).CurrentCharacter.ImgId}");
                                // Get field rectangle and page
                                // Get field rectangle and convert to Rectangle object
                                var fieldWidget = field.GetWidgets().First();
                                var fieldRectArray = fieldWidget.GetRectangle();

                                // Convert PdfArray to Rectangle
                                var fieldRect = new Rectangle(
                                    fieldRectArray.GetAsNumber(0).FloatValue(), // x
                                    fieldRectArray.GetAsNumber(1).FloatValue(), // y
                                    fieldRectArray.GetAsNumber(2).FloatValue() - fieldRectArray.GetAsNumber(0).FloatValue(), // width
                                    fieldRectArray.GetAsNumber(3).FloatValue() - fieldRectArray.GetAsNumber(1).FloatValue()  // height
                                );

                                // Get the page containing the field
                                var page = fieldWidget.GetPage();

                                // Calculate image dimensions to fit in field
                                float fieldWidth = fieldRect.GetWidth();
                                float fieldHeight = fieldRect.GetHeight();

                                float imageWidth = imageData.GetWidth();
                                float imageHeight = imageData.GetHeight();

                                float scaleX = fieldWidth / imageWidth;
                                float scaleY = fieldHeight / imageHeight;
                                float scale = Math.Min(scaleX, scaleY);

                                float scaledWidth = imageWidth * scale;
                                float scaledHeight = imageHeight * scale;

                                // Center the image in the field
                                float x = fieldRect.GetX() + (fieldWidth - scaledWidth) / 2;
                                float y = fieldRect.GetY() + (fieldHeight - scaledHeight) / 2;

                                // Create canvas for the page and add image
                                var canvas = new PdfCanvas(page);
                                canvas.AddImageFittedIntoRectangle(imageData,
                                    new Rectangle(x, y, scaledWidth, scaledHeight), false);

                                // Optional: Make field read-only
                                field.SetReadOnly(true);
                            }
                        }
                    }
                }
                //}
                return outputPath;
            }
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

        // Helper method to extract the embedded template to a usable location
        async Task<string> ExtractPdfTemplateToAccessibleLocation()
        {
            try
            {
                // Define where the template will be extracted to
                string extractedTemplatePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "template.pdf");

                // If we already extracted it previously, just return the path
                if (File.Exists(extractedTemplatePath))
                    return extractedTemplatePath;

                // Get the embedded resource as a stream
                // Note: The path format has changed to reflect the standard MauiAsset path
                using (Stream resourceStream = await FileSystem.OpenAppPackageFileAsync("DnD_5E_CharacterSheet_FormFillable.pdf"))
                {
                    if (resourceStream == null)
                        return null;

                    // Create the file we'll copy to
                    using (FileStream fileStream = File.Create(extractedTemplatePath))
                    {
                        await resourceStream.CopyToAsync(fileStream);
                    }

                    return extractedTemplatePath;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to extract template: {ex.Message}");
                return null;
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