﻿using System;
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
    public sealed partial class EditPostPage : Page
    {
        public EditPostPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += EditPostPage_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Bind post VM to data context
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

        private void EditPostPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Go back if possible
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            AppViewModel.Instance.AddOrUpdatePost((PostViewModel)DataContext);
        }

        private void ButtonDiscard_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
