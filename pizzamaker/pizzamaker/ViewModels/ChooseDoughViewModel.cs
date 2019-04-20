﻿
using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Threading;
using System.Reflection;

namespace pizzamaker.ViewModels
{
    public class ChooseDoughViewModel : Screen
    {

        public ChooseDoughViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();
        }
        #region Initialize data
        private void Initialize()
        {
            Doughs = new BindableCollection<Food>() { new Dough(1, "Normal Dough", "Regualr dough with our spicy spice", 1.99), new Dough(1, "Normal Dough", "Regualr dough with our spicy spice", 1.99), new Dough(1, "Normal Dough", "Regualr dough with our spicy spice", 1.99), new Dough(2, "Wholegrain Dough", "Regualr dough with our spicy spice", 1.99), new Dough(3, "GlutenFree Dough", "Regualr dough with our spicy spice", 1.99), new Dough(4, "Crusty Dough", "Regualr dough with our spicy spice", 1.99) };
            SelectedDoughCommand = new RelayCommand(DoughSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);


        }
        private void LoadOrderData()
        {
            var order = Order.getInstance();
            if (order.Dough != null) SelectedDough = order.Dough;
        }
        #endregion

        #region View properties and Methods like page changing or current dough
        private StartUpViewModel mainWindow;
        public BindableCollection<Food> Doughs { get; set; }
        private Dough _selectedDough;
        public Dough SelectedDough
        {
            get { return _selectedDough; }
            set {
                var order = Order.getInstance();
                order.Remove(_selectedDough);
                _selectedDough = value;
                order.Dough = value;
                order.Add(_selectedDough);
                NotifyOfPropertyChange(() => SelectedDough);
            }
        }

        public void DoughSelected(object obj) {
            if (obj is Dough) {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedDough = obj as Dough;
            }
        }
        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        public void LoadNextView()
        {
            var order = Order.getInstance();
            if (order.Dough == null) {
                MessageBox.Show("You must selected a dough first!");
                return;
            }
            mainWindow.LoadNextView();
        }
        public void LoadPrevView()
        {
            mainWindow.LoadPrevView();
        }
        #endregion
        #region Scrollers properties and methods
        DispatcherTimer scrollTimer;
        private ICommand selectedDoughCommand;
        public ICommand SelectedDoughCommand
        {
            get
            {
                return selectedDoughCommand;
            }
            set
            {
                selectedDoughCommand = value;
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
        #endregion
    }
}