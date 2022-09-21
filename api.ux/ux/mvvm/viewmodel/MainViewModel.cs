using application.api.ux.core;

namespace application.api.ux.mvvm.viewmodel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeViewModel { get; set; }

        private object? currentView;

        public object? CurrentView
        {
            get { return currentView; }
            set 
            { 
                currentView = value;
                this.OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            this.HomeViewModel = new HomeViewModel();
            this.CurrentView = this.HomeViewModel;
        }
    }
}
