using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MediaPluginIssue
{
    public class PhotoListViewModel: BaseViewModel
    {
        ObservableCollection<Photos> imageSources;

        public ObservableCollection<Photos> ImageSources
        {
            get => imageSources;
            set
            {
                imageSources = value;
                OnPropertyChanged();
            }
        }

        public ICommand TakePhotoCommand { get; set; }

        public PhotoListViewModel()
        {
            ImageSources=new ObservableCollection<Photos>();
            //App.ImageSources=ImageSources;
            TakePhotoCommand = new Command(TakePhoto);
        }

        async void TakePhoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if(CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {


                    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Test",
                        SaveToAlbum = true,
                        CompressionQuality = 75,
                        CustomPhotoSize = 50,
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000,
                        DefaultCamera = CameraDevice.Front
                    });

                    if(file == null)
                        return;

                    var imgsource = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });

                    ImageSources.Add(new Photos(imgsource,file.Path));
                    //App.ImageSources=ImageSources;
                    Console.WriteLine("CapturedImageCount : " + ImageSources.Count);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }

    public class Photos
    {
        public ImageSource ImageSource { get; set; }
        public string ImageURL { get; set; }
        public DateTime CaptureTime { get; set; }
        public string Name { get; set; }

        public Photos(ImageSource source,string url)
        {
            ImageSource=source;
            CaptureTime=DateTime.Now;
            Name=DateTime.Now.ToString("yyyyMMddHHmmss")+".jpg";
            ImageURL=url;
        }
    }
}
