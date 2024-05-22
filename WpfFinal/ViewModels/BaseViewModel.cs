using System.Windows.Input;
using System.Windows;
using WpfFinal.Services;
using WpfFinal.Commands;
using WpfFinal.Views.Windows;

namespace WpfFinal.ViewModels;
public class BaseViewModel : NotifyPropertyChangedService
{
    public BaseViewModel()
    {
        CloseCommand = new RelayCommand(Close);
    }
    
    #region CloseCommand
    public ICommand CloseCommand { get; set; }
    public void Close(object? obj)
    {
        if (obj is FrameworkElement element)
        {
            Window window = Window.GetWindow(element);
            window?.Close();
        }
    }
    #endregion

}
