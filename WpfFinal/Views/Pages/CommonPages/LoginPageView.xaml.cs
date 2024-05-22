using System.Windows.Controls;
using WpfFinal.ViewModels.CommonViewModels;

namespace WpfFinal.Views.Pages.CommonPages;
public partial class LoginPageView : Page
{
    public LoginPageView()
    {
        InitializeComponent();
        DataContext = App.Container.GetInstance<LoginPageViewModel>();
    }
}
