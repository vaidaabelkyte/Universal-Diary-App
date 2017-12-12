using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DiaryApp.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DiaryApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostPage : Page
    {
        public PostPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += PostPage_BackRequested;
        }

        private void ScrollViewer(object sender, RoutedEventArgs e)
        {
            /*
                UIElement.UpdateLayout
                    Ensures that all positions of child objects of
                    a UIElement are properly updated for layout.
            */

            ScrollViewer2.UpdateLayout();

            /*
                ScrollViewer.ChangeView
                    Causes the ScrollViewer to load a new view into
                    the viewport using the specified offsets and zoom factor.
            */

            // Programmatically scroll to bottom
            ScrollViewer2.ChangeView(
                0.0f, // horizontalOffset
                double.MaxValue, // verticalOffset
                1.0f // zoomFactor
                );

            // Another way to programmatically scroll to bottom
            // But above way is better
            //ScrollViewer1.ScrollToVerticalOffset(ScrollViewer1.ScrollableHeight);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                DataContext = e.Parameter;
            }
            // Check if can go back
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                // Show UI in title bar
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void PostPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Go back if possible
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            AppViewModel.Instance.RemovePost((PostViewModel)DataContext);
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditPostPage), new PostViewModel((PostViewModel)DataContext));
        }
    }
}
