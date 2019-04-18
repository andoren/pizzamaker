
using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace pizzamaker.ViewModels
{
    public class ChooseDoughViewModel : Screen
    {
 
        public ChooseDoughViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Doughs = new BindableCollection<Food>() { new Dough(1,"Normal Dough","Regualr dough with our spicy spice",1.99), new Dough(2, "Wholegrain Dough", "Regualr dough with our spicy spice", 1.99), new Dough(3, "GlutenFree Dough", "Regualr dough with our spicy spice", 1.99), new Dough(4, "Crusty Dough", "Regualr dough with our spicy spice", 1.99) };
            SelectedDoughCommand = new RelayCommand(DoughSelected, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }
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
        private string _selectedDoughName = "Olasz Zászló";
        private BitmapImage _selectedDough = new BitmapImage(new Uri(@"E:\git2019\pizzamaker\pizzamaker\pizzamaker\imgs\italyflag.png", UriKind.Absolute));

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
        private string _selectedDoughDescription ="Ez egy szép olasz zászló (csak átmenetileg leszek itt amig nincs adatbázis kapcsolat)";

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
    }
}