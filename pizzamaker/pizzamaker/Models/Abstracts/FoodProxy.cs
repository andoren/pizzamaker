using Caliburn.Micro;
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
    public class FoodProxy: Food, INotifyPropertyChangedEx
    {
        public FoodProxy()
        {
            
            CreateBitMapImage();

        }
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
            get { return _picture; }
            set
            {
                _picture = value;
                NotifyOfPropertyChange("Picture");

            }
        }
        protected override void CreateBitMapImage()
        {
            var image = new BitmapImage();
            if (RawPicture == null || RawPicture.Length == 0)
            {


                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(@"E:\git2019\pizzamaker\pizzamaker\pizzamaker\imgs\imgicon.png", UriKind.Relative);
                image.EndInit();
                image.Freeze();
                Picture = image;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(200);
                    Dispatcher.CurrentDispatcher.Invoke(() => {
                        this.RawPicture = Convert.FromBase64String(temp);
                        CreateBitMapImage();
                    });

                }
                 );
                t.Start();
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
