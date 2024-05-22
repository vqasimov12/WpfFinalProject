using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Commands;

namespace WpfFinal.ViewModels.CommonViewModels;
public class DashBoardWindowBaseViewModel : BaseViewModel
{
    public ICommand ResizeCommand { get; set; }
    public ICommand MinimizeCommand { get; set; }
    public void ResizeCommandExecute(object? obj)
    {

        var window = obj as Window;
        if (window is not null)
            if(window.WindowState== WindowState.Maximized)
            window.WindowState = WindowState.Normal;
        else
            window.WindowState = WindowState.Maximized;
    }
    public DashBoardWindowBaseViewModel()
    {
        ResizeCommand = new RelayCommand(ResizeCommandExecute);
        MinimizeCommand = new RelayCommand(MinimizeCommandExecute);
    }

    public void MinimizeCommandExecute(object?obj)
    {
        var window = obj as Window;
        if (window is not null)
            window.WindowState = WindowState.Minimized;
    }
}
