﻿using Caliburn.Micro;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using pizzamaker.Models.Singletons;

namespace pizzamaker.ViewModels
{
    public class ChooseYourSauceViewModel:Screen
    {
        public ChooseYourSauceViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();

        }
        #region Initialize data
        /// <summary>
        /// We initialize here the necessary data what is needed to the view. The Buttons commands, the foods .
        /// </summary>
        void Initialize()
        {
            var databasehelper = DatabaseHelperProxy.getInstance();
            Sauces = databasehelper.GetFoodsByType("sauce");
            SelectedSauceCommand = new RelayCommand(SauceSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }
        /// <summary>
        /// If we came back to this page then load the previously selected data
        /// </summary>
        private void LoadOrderData()
        {
            Order = Order.getInstance();
            if (Order.Sauce != null)
            {
                SelectedSauce = Order.Sauce;
                order.Remove(SelectedSauce);
                
            }
        }
        #endregion
        #region View properties and methods like current Sauce
        //We store here the visualizeable foods
        public BindableCollection<Food> Sauces { get; set; }
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
        }

        private StartUpViewModel mainWindow;

        private Sauce _selectedSauce;
        //Add the food to our order, and changes the selected food picture from the view
        public Sauce SelectedSauce
        {
            get { return _selectedSauce; }
            set
            {
                
                _selectedSauce = value;
                order.AddAt(SelectedSauce, 1);
                NotifyOfPropertyChange(() => SelectedSauce);
            }
        }
        public void LoadNextView()
        {
            try
            {
                if (SelectedSauce == null)
                {
                    MessageBox.Show("You must selected a sauce first!");
                    return;
                }
                order.Remove(SelectedSauce);
                order.Sauce = SelectedSauce;
                order.AddAt(SelectedSauce, 1);
                mainWindow.LoadNextView();
            }
            catch (Exception e)
            {
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
            catch(Exception e)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadPrevView", e.Message);
            }
        }
        #endregion
        #region Scrollers properties and methods
        //This timer moves the scroller in the view.
        DispatcherTimer scrollTimer;

        private bool canExecute = true;
        //Command for the scroller image button
        private ICommand selectedSauceCommand;
        public ICommand SelectedSauceCommand
        {
            get
            {
                return selectedSauceCommand;
            }
            set
            {
                selectedSauceCommand = value;
            }
        }
        //The actual command of the SelectedSauceCommand
        public void SauceSelected(object obj)
        {
            if (obj is Sauce)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedSauce = obj as Sauce;
            }
        }
        //Command for the scroller button
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
        //Command for the scroller button
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

        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        /// <summary>
        /// Gets the object(Scroller) as command parameter from the view and moving the scroller right with DispatcherTimer
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Gets the object(Scroller) as command parameter from the view and moving the scroller left with DispatcherTimer
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Removes the given DispatcherTimer tick events if has any.
        /// </summary>
        /// <param name="dispatchTimer"></param>
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
