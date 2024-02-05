

namespace MauiApp7
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            TakeScreenshotAsync();
        }

        public async Task<ImageSource> TakeScreenshotAsync()
        {
            if (Screenshot.Default.IsCaptureSupported)
            {
                IScreenshotResult screen = await Screenshot.Default.CaptureAsync();

                Stream stream = await screen.OpenReadAsync();

                var imageSource = ImageSource.FromStream(() => stream);
                //Imafe.Source = imageSource;
                string appDirectory="";


                List<string> list = new List<string>();
                DateTime dateTime = DateTime.Now;

                string DATE = "";
                var date = dateTime.ToString();
                var dates = date.Replace('.', '_');

                var DA = dates.Replace(':', '_');
                for (int i = 0; i < DA.Length; i++)
                {
                    if (DA[i].ToString() == "13")
                    {
                        DATE += "_";
                    }
                    else
                    {
                        list.Add(DA[i].ToString());



                    }
                }
                DATE = "";
                list.RemoveAt(10);
                list.Insert(10, "_");
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i] == "")
                    {

                    }
                    else
                    {


                        DATE += list[i].ToString();

                    }
                }


                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
#if ANDROID
                    appDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(type: Android.OS.Environment.DirectoryDownloads).AbsolutePath;
#endif
                }
                else if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    appDirectory = System.IO.Path.GetTempPath();
                    string path2 = System.IO.Path.Combine(appDirectory, $"{Guid.NewGuid().ToString()}дата_{DATE}.jpg");
                }

                System.Diagnostics.Debug.WriteLine("Download Path: " + appDirectory);
                appDirectory = Path.Combine(appDirectory, "Examiner");
                if (!Directory.Exists(appDirectory))
                {
                    Directory.CreateDirectory(appDirectory);
                    Console.WriteLine("Папка Downloads успешно создана.");
                }
                string downloadsPath = Path.Combine(appDirectory, $"{Guid.NewGuid().ToString()}дата_{DATE}.jpg");

                type.Text = downloadsPath;
                // Проверяем, существует ли папка, и если нет, создаем ее


                if (File.Exists(downloadsPath))
                {
                    File.Delete(downloadsPath);
                    Console.WriteLine("Файл успешно удален.");
                }
                else
                {
                    Console.WriteLine("Файл не существует.");
                }
                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                File.WriteAllBytes(downloadsPath, memoryStream.ToArray());
                Images.Source = downloadsPath;
                return imageSource;
            }

            return null;
        }
    }

}                //var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
                 ////var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
                 //string downloadsPath = Path.Combine(docsDirectory.AbsoluteFile.Path, $"{Guid.NewGuid().ToString()}дата_{DATE}.jpg");

//using (FileStream fileStream = new FileStream(downloadsPath, FileMode.OpenOrCreate))
//{
//    // Копирование данных из MemoryStream в FileStream
//    stream.CopyTo(fileStream);
//}