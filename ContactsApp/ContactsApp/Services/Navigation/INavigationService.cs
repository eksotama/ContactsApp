using ContactsApp.ViewModels.Base;
using System.Threading.Tasks;

namespace ContactsApp.Services
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task NavigateModalAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateModalAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
        Task GoBack();
    }
}
