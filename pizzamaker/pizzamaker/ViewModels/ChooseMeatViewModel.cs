using Caliburn.Micro;
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
using System.Windows.Threading;

namespace pizzamaker.ViewModels
{
    class ChooseMeatViewModel:Screen
    {
       

        public ChooseMeatViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();
        }

        #region Initialize data
        void Initialize()
        {
            Meats = new BindableCollection<Food>() { new Meat(), new Meat(1, "Spicy Meat", "This is our Spicy Meat", 4.99) };
            SelectedMeatCommand = new RelayCommand(MeatSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }
        private void LoadOrderData()
        {
            Order = Order.getInstance();
            if (Order.Meat != null)
            {
                SelectedMeat = Order.Meat;
                order.Remove(SelectedMeat);

            }
        }
        #endregion
        #region View properties and methods like current Meat
        //
        public BindableCollection<Food> Meats { get; set; }
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
        }

        private StartUpViewModel mainWindow;
        public void MeatSelected(object obj)
        {
            if (obj is Meat)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedMeat = obj as Meat;
            }
        }
        private Meat _selectedMeat;
        public Meat SelectedMeat
        {
            get { return _selectedMeat; }
            set
            {

                _selectedMeat = value;
                order.AddAt(SelectedMeat, 2);
                NotifyOfPropertyChange(() => SelectedMeat);
            }
        }
        public void LoadNextView()
        {
            if (SelectedMeat == null)
            {
                MessageBox.Show("You must selected a Meat first!");
                return;
            }
            order.Remove(SelectedMeat);
            order.Meat = SelectedMeat;
            order.AddAt(SelectedMeat, 1);
            mainWindow.LoadNextView();
        }
        public void LoadPrevView()
        {
            mainWindow.LoadPrevView();
        }
        #endregion
        #region Scrollers properties and methods
        //This timer moves the scroller in the view.
        DispatcherTimer scrollTimer;

        private bool canExecute = true;
        //Command for the scroller image button
        private ICommand selectedMeatCommand;
        public ICommand SelectedMeatCommand
        {
            get
            {
                return selectedMeatCommand;
            }
            set
            {
                selectedMeatCommand = value;
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
