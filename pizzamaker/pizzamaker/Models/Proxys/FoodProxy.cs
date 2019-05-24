using Caliburn.Micro;
using pizzamaker.Models.Singletons;
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
    public class FoodProxy : Food 
    {
        private Food food;
        public FoodProxy()
        {

            CreateBitMapImage();

        }
        //We need this for t
        volatile bool retrieving = false;

        public override BitmapImage Picture
        {
            get {
                lock (this) { 
                return _picture;
                }

            }
            set
            {
                lock (this)
                {
                    _picture = value;
                    
                }
                NotifyOfPropertyChange(() => Picture);


            }
        }

        /// <summary>
        /// First gives back a placeholder picture for enhance the speed of the view load. When its downloaded the picture notifys the view to changed the placeholder.
        /// </summary>
        protected override void CreateBitMapImage()
        {
            try {
                var image = new BitmapImage();
                lock (lockobj) {
                    if (food == null)
                    {
                        if (!retrieving)
                        {
                            retrieving = true;
                            image.BeginInit();
                            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\imgs\imgicon.png", UriKind.Relative);
                            image.EndInit();
                            image.Freeze();
                            Picture = image;
                            Task.Factory.StartNew(() =>
                            {
                                food = new Food();
                                var databasehelper = DatabaseHelperProxy.getInstance();
                                food.RawPicture = databasehelper.GetRawPicture(Id);
                                CreateBitMapImage();
                                
                            });

                        }
                    }
                    else
                    {
                        using (var mem = new MemoryStream(food.RawPicture))
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
                NotifyOfPropertyChange(() => Picture);
            }
            catch (Exception e)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.DbLog, this.GetType().ToString(), "CreateBitMapImage", e.Message);
            }
        }

    }
}
