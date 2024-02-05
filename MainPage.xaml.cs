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
                string appDirectory = System.IO.Path.GetTempPath();

                Stream stream = await screen.OpenReadAsync();

                var imageSource = ImageSource.FromStream(() => stream);
                //Imafe.Source = imageSource;
                string path2 = System.IO.Path.Combine(appDirectory, "image1.jpg");

                // Удаление файла, если он существует
                if (System.IO.File.Exists(path2))
                {
                    System.IO.File.Delete(path2);
                }
                var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads);
                //var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);

                //File.WriteAllText($"{docsDirectory.AbsoluteFile.Path}/atextfile.txt", "contents are here");
                string downloadsPath = Path.Combine(docsDirectory.AbsoluteFile.Path, "image1.jpg");

                type.Text = downloadsPath;
                // Проверяем, существует ли папка, и если нет, создаем ее
                //if (!Directory.Exists(downloadsPath))
                //{
                //    Directory.CreateDirectory(downloadsPath);
                //    Console.WriteLine("Папка Downloads успешно создана.");
                //}

                if (System.IO.File.Exists(downloadsPath))
                {
                    System.IO.File.Delete(downloadsPath);
                }

                using (FileStream fileStream = new FileStream(downloadsPath, FileMode.OpenOrCreate))
                {
                    // Копирование данных из MemoryStream в FileStream
                    stream.CopyTo(fileStream);
                }


                Images.Source = downloadsPath;
                return imageSource;
            }

            return null;
        }
    }

}
