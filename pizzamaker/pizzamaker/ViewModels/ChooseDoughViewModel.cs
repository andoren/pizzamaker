
using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Threading;

namespace pizzamaker.ViewModels
{
    public class ChooseDoughViewModel : Screen
    {

        public ChooseDoughViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Doughs = new BindableCollection<Food>() { new Dough(1, "Normal Dough", "Regualr dough with our spicy spice", 1.99), new Dough(1, "Normal Dough", "Regualr dough with our spicy spice", 1.99), new Dough(1, "Normal Dough", "Regualr dough with our spicy spice", 1.99), new Dough(2, "Wholegrain Dough", "Regualr dough with our spicy spice", 1.99), new Dough(3, "GlutenFree Dough", "Regualr dough with our spicy spice", 1.99), new Dough(4, "Crusty Dough", "Regualr dough with our spicy spice", 1.99) };
            SelectedDoughCommand = new RelayCommand(DoughSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
            
        }

        DispatcherTimer scrollTimer;
        private StartUpViewModel mainWindow;
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

        public BindableCollection<Food> Doughs { get; set; }
        private string _selectedDoughName = "Dough name";
        private BitmapImage _selectedDough;

        public BitmapImage SelectedDough
        {
            get { return _selectedDough; }
            set {
                _selectedDough = value;
                NotifyOfPropertyChange(() => SelectedDough);
            }
        }

        public string SelectedDoughName
        {
            get { return _selectedDoughName; }
            set
            {
                _selectedDoughName = value;
                NotifyOfPropertyChange(() => SelectedDoughName);
            }
        }
        private string _selectedDoughDescription = "Ez egy szép olasz zászló (csak átmenetileg leszek itt amig nincs adatbázis kapcsolat)";

        public string SelectedDoughDescription
        {
            get { return _selectedDoughDescription; }
            set { _selectedDoughDescription = value; }
        }
        public void DoughSelected(object obj) {
            if (obj is Dough) {
                Dough dough = obj as Dough;
                SelectedDoughName = dough.Name;
                SelectedDough = dough.Picture;
            }
        }
        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        public void LoadNextView()
        {
            mainWindow.LoadNextView();
        }
        public void LoadPrevView()
        {
            mainWindow.LoadPrevView();
        }

        public void ScrollerToRight(object obj) {
            if (obj is ScrollViewer)
            {
                ScrollViewer scrollViewer = obj as ScrollViewer;
                scrollTimer = new DispatcherTimer();

                scrollTimer.Start();
                double curroffset = scrollViewer.HorizontalOffset;
                scrollTimer.Interval = TimeSpan.FromMilliseconds(15);

                scrollTimer.Tick += (s, e) =>
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 5);

                    if (curroffset + 140 == scrollViewer.HorizontalOffset)
                    {
                        scrollTimer.Stop();
                    }
                };
                
            }
        }
        public void ScrollerToLeft(object obj)
        {
            ScrollViewer scrollViewer = obj as ScrollViewer;
            scrollTimer = new DispatcherTimer();

            scrollTimer.Start();
            double curroffset = scrollViewer.HorizontalOffset;
            scrollTimer.Interval = TimeSpan.FromMilliseconds(15);

            scrollTimer.Tick += (s, e) =>
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 5);

                if (curroffset - 140 == scrollViewer.HorizontalOffset)
                {
                    scrollTimer.Stop();
                }
            };
        }
    }
}