using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using System.Windows.Threading;
using System.ComponentModel;
using System.Globalization;
using pizzamaker.Models.Singletons;

namespace pizzamaker.Models
{
    public abstract class Food
    {
        
        CultureInfo us = CultureInfo.GetCultureInfo("en-US");
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        protected double _price;

        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private byte[] _rawpicture;

        public  byte[] RawPicture
        {
            get {
                lock (this) {
                return _rawpicture;
                }
            }
            set {
                lock (this) { 
                _rawpicture = value;
                }
            }
        }
        protected volatile BitmapImage _picture;

        
        public virtual BitmapImage Picture
        {
            get { return _picture; }
            set {
                _picture = value;
                

            }
        }
        public virtual string GetInformation {
            get {
                
                return string.Format(us,"Name: {0}"+Environment.NewLine+"Description: {1}"+Environment.NewLine+"Price: {2}",Name,Description,Price.ToString());
            }
        }

        public string GetPriceInCurrency
        {
            get { 
            return string.Format(us,"{0:C2}",Price);
            }
        }

        protected virtual void CreateBitMapImage()
        {
            try
            {
                var image = new BitmapImage();
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
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.DbLog, this.GetType().ToString(), "CreateBitMapImage", e.Message);
            }
        }


    }
}
