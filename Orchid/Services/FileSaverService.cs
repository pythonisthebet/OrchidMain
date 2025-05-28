using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orchid.Services
{
    public interface IFileSaver
    {
        Task<bool> SaveFileAsync(Stream fileStream, string fileName);
    }
    public class FileSaverImplementation : IFileSaver
    {

        //save a file in an android phone
        public async Task<bool> SaveFileAsync(Stream fileStream, string fileName)
        {

            try
            {
#if ANDROID
                // Request permissions if needed
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    return false;
                }

                // Get the platform-specific MainActivity instance
                var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
                string packageName = activity.PackageName;

                // Use Android-specific APIs through the platform assemblies
                var downloadsPath = global::Android.OS.Environment.GetExternalStoragePublicDirectory(global::Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var filePath = Path.Combine(downloadsPath, fileName);

                using (var destinationStream = File.Create(filePath))
                {
                    await fileStream.CopyToAsync(destinationStream);
                }

                // Option 1: Use FileProvider (if properly configured)
                try
                {
                    var file = new Java.IO.File(filePath);
                    var fileUri = global::AndroidX.Core.Content.FileProvider.GetUriForFile(
                        activity,
                        $"{packageName}.fileprovider",
                        file);

                    var openIntent = new global::Android.Content.Intent(global::Android.Content.Intent.ActionView);
                    openIntent.SetDataAndType(fileUri, "application/pdf");
                    openIntent.AddFlags(global::Android.Content.ActivityFlags.GrantReadUriPermission);
                    openIntent.AddFlags(global::Android.Content.ActivityFlags.NewTask);

                    activity.StartActivity(global::Android.Content.Intent.CreateChooser(openIntent, "Open PDF with..."));
                }
                catch (Java.Lang.IllegalArgumentException)
                {
                    // Option 2: Fallback to direct file URI (this works on older Android versions but not newer ones)
                    try
                    {
                        var file = new Java.IO.File(filePath);
                        var fileUri = global::Android.Net.Uri.FromFile(file);

                        var openIntent = new global::Android.Content.Intent(global::Android.Content.Intent.ActionView);
                        openIntent.SetDataAndType(fileUri, "application/pdf");
                        openIntent.AddFlags(global::Android.Content.ActivityFlags.NewTask);

                        activity.StartActivity(openIntent);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to open file directly: {ex.Message}");
                        // At least the file was saved, so return true
                    }
                }

                return true;
#endif
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                return false;
            }

        }

    }
}

//// iOS Implementation
//#if IOS
//namespace Orchid.Platforms.iOS
//{
//    public class FileSaverImplementation : IFileSaver
//    {
//        public async Task<bool> SaveFileAsync(Stream fileStream, string fileName)
//        {
//            try
//            {
//                var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
//                var filePath = Path.Combine(docFolder, fileName);
                
//                using (var destinationStream = File.Create(filePath))
//                {
//                    await fileStream.CopyToAsync(destinationStream);
//                }
                
//                // Share the file with UIDocumentInteractionController
//                UIKit.UIViewController currentController = GetCurrentUIViewController();
                
//                UIKit.UIDocumentInteractionController documentInteractionController = 
//                    UIKit.UIDocumentInteractionController.FromUrl(Foundation.NSUrl.FromFilename(filePath));
                
//                documentInteractionController.PresentOpenInMenu(new CoreGraphics.CGRect(0, 0, 0, 0), 
//                    currentController.View, true);
                
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error saving file: {ex.Message}");
//                return false;
//            }
//        }
        
//        // Helper to get current view controller
//        private UIKit.UIViewController GetCurrentUIViewController()
//        {
//            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
//            var rootController = window.RootViewController;
            
//            if (rootController.PresentedViewController != null)
//                return rootController.PresentedViewController;
            
//            return rootController;
//        }
//    }
//}
//#endif

//// Windows Implementation
//#if WINDOWS
//namespace Orchid.Platforms.Windows
//{
//    public class FileSaverImplementation : IFileSaver
//    {
//        public async Task<bool> SaveFileAsync(Stream fileStream, string fileName)
//        {
//            try
//            {
//                var fileSavePicker = new Windows.Storage.Pickers.FileSavePicker();
                
//                // Get the current window handle
//                var hwnd = ((MauiWinUIWindow)App.Current.Windows[0].Handler.PlatformView).WindowHandle;
                
//                // Initialize the file picker with the window handle
//                WinRT.Interop.InitializeWithWindow.Initialize(fileSavePicker, hwnd);
                
//                // Set properties
//                fileSavePicker.SuggestedFileName = fileName;
//                fileSavePicker.FileTypeChoices.Add("PDF Document", new List<string>() { ".pdf" });
                
//                // Open the picker for the user to select where to save
//                var file = await fileSavePicker.PickSaveFileAsync();
                
//                if (file != null)
//                {
//                    using (var destinationStream = await file.OpenStreamForWriteAsync())
//                    {
//                        await fileStream.CopyToAsync(destinationStream);
//                    }
//                    return true;
//                }
                
//                return false;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error saving file: {ex.Message}");
//                return false;
//            }
//        }
//    }
//}
//#endif

//// MacCatalyst Implementation
//#if MACCATALYST
//namespace Orchid.Platforms.MacCatalyst
//{
//    public class FileSaverImplementation : IFileSaver
//    {
//        public async Task<bool> SaveFileAsync(Stream fileStream, string fileName)
//        {
//            try
//            {
//                var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
//                var filePath = Path.Combine(docFolder, fileName);
                
//                using (var destinationStream = File.Create(filePath))
//                {
//                    await fileStream.CopyToAsync(destinationStream);
//                }
                
//                // Share the file with UIDocumentInteractionController (similar to iOS)
//                UIKit.UIViewController currentController = GetCurrentUIViewController();
                
//                UIKit.UIDocumentInteractionController documentInteractionController = 
//                    UIKit.UIDocumentInteractionController.FromUrl(Foundation.NSUrl.FromFilename(filePath));
                
//                documentInteractionController.PresentOpenInMenu(new CoreGraphics.CGRect(0, 0, 0, 0), 
//                    currentController.View, true);
                
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error saving file: {ex.Message}");
//                return false;
//            }
//        }
        
//        // Helper to get current view controller
//        private UIKit.UIViewController GetCurrentUIViewController()
//        {
//            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
//            var rootController = window.RootViewController;
            
//            if (rootController.PresentedViewController != null)
//                return rootController.PresentedViewController;
            
//            return rootController;
//        }
//    }
//}
//#endif