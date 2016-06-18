using Okra.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Okra.Mvvm
{
    public class WindowAppHost
    {
        private INavigationManager _navigationManager;
        private SystemNavigationManager _systemNavigationManager;

        // *** Constructors ***

        public WindowAppHost(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;

            _navigationManager.PropertyChanged += NavigationManager_PropertyChanged;
        }

        // *** Methods ***

        public void SetShell(object shell)
        {
            InitializeWindow();

            Window.Current.Content = shell as UIElement;
            Window.Current.Activate();
        }

        private void InitializeWindow()
        {
            _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            _systemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;

            UpdateCanGoBack();
        }

        private void UpdateCanGoBack()
        {
            if (_systemNavigationManager != null)
                _systemNavigationManager.AppViewBackButtonVisibility = _navigationManager.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void NavigationManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(INavigationManager.CanGoBack))
                UpdateCanGoBack();
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_navigationManager.CanGoBack)
            {
                _navigationManager.GoBack();
                e.Handled = true;
            }
        }
    }
}
