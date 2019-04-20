﻿using Caliburn.Micro;
using pizzamaker.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace pizzamaker.Models.Foods
{
    public class Toppings:FoodProxy
    {
        public Toppings()
        {
            AllToppings = new List<Topping>();
        }
        CultureInfo us = CultureInfo.GetCultureInfo("en-US");
        private List<Topping> toppings;

        public List<Topping> AllToppings
        {
            get { return toppings; }
            set { toppings = value; }
        }

        public override double Price {
            get {
                double prices = 0;
                foreach (Food topping in AllToppings)
                {
                    prices += topping.Price;
                }
                return prices;
            }
        }
        public void AddTopping(Topping topping)
        {
            AllToppings.Add(topping);
            NotifyOfPropertyChange("AllToppings");
            NotifyOfPropertyChange("Price");
        }
        public void RemoveTopping(Topping topping)
        {
            AllToppings.Remove(topping);
            NotifyOfPropertyChange("AllToppings");
            NotifyOfPropertyChange("Price");
        }

        protected override void CreateBitMapImage()
        {
            var image = new BitmapImage();
            
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(@"E:\git2019\pizzamaker\pizzamaker\pizzamaker\imgs\toppingspicture.jpg", UriKind.Relative);
            image.EndInit();
            image.Freeze();
            Picture = image;
        }
        public override string GetInformation {
            get {
                string toppinginformations = string.Empty;
                foreach (var item in AllToppings)
                {
                    toppinginformations += string.Format(us,"Name: {0}  Description: {1}  Price: {2:C2}"+Environment.NewLine, item.Name, item.Description, item.Price);
                }
                return toppinginformations;
            }
        }

    }
}
