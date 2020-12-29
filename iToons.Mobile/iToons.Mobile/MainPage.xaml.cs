using iToons.Mobile.ViewModels;
using Xamarin.Forms;

namespace iToons.Mobile
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm { get;set;}

        public MainPage()
        {
            this.InitializeComponent();
            BindingContext = vm = new MainPageViewModel();
        }
    }
}
