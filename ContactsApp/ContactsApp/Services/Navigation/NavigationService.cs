using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.ViewModels;
using ContactsApp.ViewModels.Base;
using ContactsApp.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ContactsApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<Page> _navigationPageStack = new Stack<Page>();

        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainPageViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        public async Task NavigateModalAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await NavigateModalAsync<TViewModel>(null);
        }

        public async Task NavigateModalAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            PopupPage page = (PopupPage)CreatePage(typeof(TViewModel), parameter);
            //NavigationPage.SetHasNavigationBar(page, false);
            var navigationPage = Application.Current.MainPage as CustomNavigationView;

            //await navigationPage.Navigation.PushModalAsync(page);
            await PopupNavigation.Instance.PushAsync(page);

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);

            _navigationPageStack.Push(page);
        }

        public async Task GoBack()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (_navigationPageStack.Count > 1)
            {
               await  mainPage.Navigation.PopModalAsync();
                _navigationPageStack.Pop();
                return;
            }

            if(mainPage.Navigation.NavigationStack.Count > 1)
            {
                await mainPage.PopAsync();
                return;
            }
        }
    }
}
