using Caliburn.Micro;
using pizzamaker.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace pizzamaker.Models.Abstracts
{
    public class FoodProxy : Food, INotifyPropertyChangedEx
    {
        public FoodProxy()
        {

            CreateBitMapImage();

        }
        volatile bool retrieving = false;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsNotifying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void NotifyOfPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
        public override BitmapImage Picture
        {
            get {
                lock (this) { 
                return _picture;
                }

            }
            set
            {
                lock (this){

        
                _picture = value;
                NotifyOfPropertyChange("Picture");
                }

            }
        }
        protected override void CreateBitMapImage()
        {
            var image = new BitmapImage();
            if (RawPicture == null)
            {
                if (!retrieving)
                {

                    retrieving = true;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(@"E:\git2019\pizzamaker\pizzamaker\pizzamaker\imgs\imgicon.png", UriKind.Relative);
                    image.EndInit();
                    image.Freeze();
                    Picture = image;
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(200);
                        var databasehelper = DatabaseHelper.getInstance();
                        this.RawPicture = databasehelper.GetRawPicture(Id);
                        CreateBitMapImage();


                    });

                }
            }
            else
            {
                using (var mem = new MemoryStream(RawPicture))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                    mem.Close();
                }
                image.Freeze();
                Picture = image;
            }
        }
    
        

    }
}
