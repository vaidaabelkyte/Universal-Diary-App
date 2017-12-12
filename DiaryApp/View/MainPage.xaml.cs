using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DiaryApp.ViewModel;

namespace DiaryApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ScrollViewer(object sender, RoutedEventArgs e)
        {
            /*
                UIElement.UpdateLayout
                    Ensures that all positions of child objects of
                    a UIElement are properly updated for layout.
            */

            ScrollViewer1.UpdateLayout();

            /*
                ScrollViewer.ChangeView
                    Causes the ScrollViewer to load a new view into
                    the viewport using the specified offsets and zoom factor.
            */

            // Programmatically scroll to bottom
            ScrollViewer1.ChangeView(
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
            // Cant go back from the main page
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
        }

        private void MenuButton_OnClick(object sender, RoutedEventArgs e)
        {
            AppViewModel.Instance.IsMenuOpened = !AppViewModel.Instance.IsMenuOpened;
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AppViewModel.Instance.IsMenuOpened = false;
            this.Frame.Navigate(typeof(AboutPage));
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                this.Frame.Navigate(typeof(PostPage), ((ListView)sender).SelectedItem);
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            AppViewModel.Instance.IsMenuOpened = false;
            this.Frame.Navigate(typeof(EditPostPage), new PostViewModel(null));
        }
    }
}
