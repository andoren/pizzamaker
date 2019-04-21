﻿using Caliburn.Micro;
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
            try {
                var image = new BitmapImage();
                lock (this) {
                    if (RawPicture == null)
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
            catch (Exception e)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.DbLog, this.GetType().ToString(), "CreateBitMapImage", e.Message);
            }
        }

    }
}
