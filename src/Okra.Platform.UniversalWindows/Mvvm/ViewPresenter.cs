using Okra.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Okra.Mvvm
{
    public class ViewPresenter : Frame
    {
        public static readonly DependencyProperty CurrentViewProperty =
            DependencyProperty.Register("CurrentView", typeof(ViewInfo), typeof(ViewPresenter), new PropertyMetadata(null, CurrentViewChanged));
        
        public ViewPresenter()
        {
        }

        public ViewInfo CurrentView
        {
            get { return (ViewInfo)GetValue(CurrentViewProperty); }
            set { SetValue(CurrentViewProperty, value); }
        }

        private static void CurrentViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ViewPresenter)d).CurrentViewChanged((ViewInfo)e.OldValue, (ViewInfo)e.NewValue);
        }

        public void CurrentViewChanged(ViewInfo oldViewInfo, ViewInfo newViewInfo)
        {
            this.Navigate(typeof(ViewHost), newViewInfo.View);
        }
    }
}
