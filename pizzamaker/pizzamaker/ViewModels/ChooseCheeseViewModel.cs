﻿using Caliburn.Micro;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
using pizzamaker.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using pizzamaker.Models.Singletons;

namespace pizzamaker.ViewModels
{
    public class ChooseCheeseViewModel : Screen
    {


        public ChooseCheeseViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();
        }
        #region Initialize data
        private void Initialize()
        {
            var databasehelper = DatabaseHelperProxy.getInstance();
            Cheeses = databasehelper.GetFoodsByType("cheese");
            SelectedCheeseCommand = new RelayCommand(CheeseSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }
        private void LoadOrderData()
        {
            Order = Order.getInstance();
            if (Order.Cheese != null)
            {
                SelectedCheese = Order.Cheese;
                Order.Remove(SelectedCheese);
            }
        }
        #endregion

        #region View properties and Methods like page changing or current Cheese
        private StartUpViewModel mainWindow;
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
        }

        public BindableCollection<Food> Cheeses { get; set; }
        private Cheese _selectedCheese;
        public Cheese SelectedCheese
        {
            get { return _selectedCheese; }
            set
            {


                _selectedCheese = value;
                order.AddAt(SelectedCheese, 4);
                NotifyOfPropertyChange(() => SelectedCheese);
            }
        }

        public void CheeseSelected(object obj)
        {
            if (obj is Cheese)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedCheese = obj as Cheese;
                order.AddAt(SelectedCheese, 4);
                NotifyOfPropertyChange(() => SelectedCheese);
                
            }
        }
        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        public void LoadNextView()
        {
            try
            {
                if (SelectedCheese == null)
                {
                    MessageBox.Show("You must selected a Cheese first!");
                    return;
                }
                Order.Remove(_selectedCheese);
                Order.Cheese = SelectedCheese;
                Order.AddAt(SelectedCheese, 4);
                mainWindow.LoadNextView();
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadNextView", e.Message);
            }
        }
        public void LoadPrevView()
        {
            try
            {
                mainWindow.LoadPrevView();
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadPrevView", e.Message);
            }
        }
        #endregion
        #region Scrollers properties and methods
        DispatcherTimer scrollTimer;
        private ICommand selectedCheeseCommand;
        public ICommand SelectedCheeseCommand
        {
            get
            {
                return selectedCheeseCommand;
            }
            set
            {
                selectedCheeseCommand = value;
            }
        }
        private ICommand scrollerToLeft;
        public ICommand ScrollerToLeftCommand
        {
            get
            {
                return scrollerToLeft;
            }
            set
            {
                scrollerToLeft = value;
            }
        }
        private ICommand scrollerToRight;
        public ICommand ScrollerToRightCommand
        {
            get
            {
                return scrollerToRight;
            }
            set
            {
                scrollerToRight = value;
            }
        }
        private ICommand toggleExecuteCommand { get; set; }
        public ICommand ToggleExecuteCommand
        {
            get
            {
                return toggleExecuteCommand;
            }
            set
            {
                toggleExecuteCommand = value;
            }
        }
        private bool canExecute = true;
        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }
        public void ScrollerToRight(object obj)
        {
            if (obj is ScrollViewer)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                ScrollViewer scrollViewer = obj as ScrollViewer;
                scrollTimer = new DispatcherTimer();

                scrollTimer.Start();
                double curroffset = scrollViewer.HorizontalOffset;


                scrollTimer.Interval = TimeSpan.FromTicks(5000);

                scrollTimer.Tick += (s, e) =>
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 0.2);

                    if (curroffset + 170 <= scrollViewer.HorizontalOffset)
                    {
                        scrollTimer.Stop();
                    }
                };

            }
        }
        public void ScrollerToLeft(object obj)
        {
            if (obj is ScrollViewer)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                ScrollViewer scrollViewer = obj as ScrollViewer;
                scrollTimer = new DispatcherTimer();

                scrollTimer.Start();
                double curroffset = scrollViewer.HorizontalOffset;
                scrollTimer.Interval = TimeSpan.FromTicks(5000);

                scrollTimer.Tick += (s, e) =>
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 0.2);

                    if (curroffset - 170 >= scrollViewer.HorizontalOffset)
                    {
                        scrollTimer.Stop();
                    }
                };
            }
        }
        private void RemoveHandlers(DispatcherTimer dispatchTimer)
        {
            try
            {
                var eventField = dispatchTimer.GetType().GetField("Tick",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                var eventDelegate = (Delegate)eventField.GetValue(dispatchTimer);
                if (eventDelegate != null)
                {
                    var invocatationList = eventDelegate.GetInvocationList();

                    foreach (var handler in invocatationList)
                        dispatchTimer.Tick -= ((EventHandler)handler);
                }
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "RemoveHandlers", e.Message);
            }

        }
        #endregion
    }
}
