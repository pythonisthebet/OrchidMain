using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;
using Orchid.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Orchid.CustomViewModelBase;

namespace Orchid.ViewModels
{
    public class CharacterSheetViewModel : BaseViewModel
    {
        private OrchidWebAPIProxy Orchidservice = new OrchidWebAPIProxy();
        private readonly IPdfService _pdfService;
        private readonly ICharacterService _characterService;
        private readonly FileSaverImplementation _fileSaver;

        private CharacterData _characterData;
        private string _pdfPath;
        private Uri _pdfFileUri;
        private string _characterName;
        private string _classAndLevel;
        private bool _canPrint;
        private bool _isDataLoaded;

        private bool isAdmin;
        public bool IsAdmin
        {
            get
            {
                return ((App)Application.Current).LoggedInUser.IsAdmin;
            }
            set
            {
                this.isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        public CharacterSheetViewModel()
        {
            // Initialize services

            _pdfService = new PdfService();
            _characterService = new CharacterService();
            _fileSaver = new FileSaverImplementation();

            // Initialize commands
            GeneratePdfCommand = new Command(async () => await GeneratePdfAsync());
            PrintPdfCommand = new Command(async () => await PrintPdfAsync(), () => CanPrint);

            // Set default values
            Title = "Character Sheet";
            IsBusy = false;
            CanPrint = false;
            IsDataLoaded = false;
        }

        #region Properties

        public CharacterData CharacterData
        {
            get => _characterData;
            set => SetProperty(ref _characterData, value);
        }

        public Uri PdfFileUri
        {
            get => _pdfFileUri;
            set => SetProperty(ref _pdfFileUri, value);
        }

        public string CharacterName
        {
            get => _characterName;
            set => SetProperty(ref _characterName, value);
        }

        public string ClassAndLevel
        {
            get => _classAndLevel;
            set => SetProperty(ref _classAndLevel, value);
        }

        public bool CanPrint
        {
            get => _canPrint;
            set
            {
                SetProperty(ref _canPrint, value);
                (PrintPdfCommand as Command)?.ChangeCanExecute();
            }
        }

        public bool IsDataLoaded
        {
            get => _isDataLoaded;
            set => SetProperty(ref _isDataLoaded, value);
        }

        #endregion

        #region Commands

        public ICommand GeneratePdfCommand { get; }
        public ICommand PrintPdfCommand { get; }
        public ICommand DeleteCommand => new Command(OnDeleteCommand);


        #endregion

        #region Methods

        public async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;
                BusyText = "Loading character data...";

                // Load character data
                CharacterData = await _characterService.GetCharacterDataAsync();

                // Set display properties
                UpdateCharacterDisplay();

                // Generate the initial PDF
                await GeneratePdfAsync();

                IsDataLoaded = true;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Failed to initialize: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GeneratePdfAsync()
        {
            if (CharacterData == null)
                return;

            try
            {
                IsBusy = true;
                BusyText = "Generating PDF...";

                // Generate PDF
                _pdfPath = await _pdfService.FillCharacterSheet(CharacterData);

                // Update file URI for the WebView

                using (FileStream pdfStream = new FileStream(_pdfPath,FileMode.Open))
                {
                    bool success = await _fileSaver.SaveFileAsync(pdfStream, "Document.pdf");
                    // Show appropriate success/failure message
                }
                //PdfFileUri = new Uri($"file://{_pdfPath}");

                // Now we can print
                CanPrint = true;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Failed to generate PDF: {ex.Message}");
                CanPrint = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task PrintPdfAsync()
        {
            if (string.IsNullOrEmpty(_pdfPath) || !File.Exists(_pdfPath))
            {
                await ShowErrorAsync("PDF file not found. Please generate the PDF first.");
                return;
            }

            try
            {
                IsBusy = true;
                BusyText = "Preparing to print...";

#if WINDOWS
                // Windows-specific printing logic
                await PrintWindowsPdf(_pdfPath);
#elif ANDROID || IOS
                // Mobile printing logic
                await SharePdf(_pdfPath);
#else
                await Shell.Current.DisplayAlert("Print", "Printing not supported on this platform", "OK");
#endif
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Failed to print PDF: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

#if WINDOWS
        private async Task PrintWindowsPdf(string pdfPath)
        {
            // On Windows, launch the PDF with the default viewer
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(pdfPath)
            });
        }
#endif

        private async Task SharePdf(string pdfPath)
        {
            // On mobile, use the share capability which often includes print options
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Share Character Sheet",
                File = new ShareFile(pdfPath)
            });
        }

        private void UpdateCharacterDisplay()
        {
            if (CharacterData?.character == null)
                return;

            // Set character name (sample - in a real app this would come from the JSON)
            CharacterName = "Multi-Class Character";

            // Format class and level
            string classAndLevel = "";
            for (int i = 0; i < CharacterData.character.Classes.Count; i++)
            {
                classAndLevel += $"{CharacterData.character.Classes[i]} {CharacterData.character.ClassLevels[i]}";
                if (i < CharacterData.character.Classes.Count - 1)
                    classAndLevel += ", ";
            }
            ClassAndLevel = classAndLevel;
        }

        private async Task ShowErrorAsync(string message)
        {
            await Shell.Current.DisplayAlert("Error", message, "OK");
        }

        async void OnDeleteCommand(object param)
        {
            await Orchidservice.DeleteCharacter(((App)Application.Current).CurrentCharacter);
            await Shell.Current.GoToAsync("..");
        }

        #endregion
    }
}
