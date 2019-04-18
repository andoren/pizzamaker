using Caliburn.Micro;
using pizzamaker.Models;

namespace pizzamaker.ViewModels
{
    public class ChooseDoughViewModel : Screen
    {
 
        public ChooseDoughViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        private StartUpViewModel mainWindow;
        private string _selectedDoughName = "Olasz Zászló";

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