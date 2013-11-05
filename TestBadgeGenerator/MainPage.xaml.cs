using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestBadgeGenerator.Resources;
using BadgeServicePCLClient;
using Model;
using System.Windows.Media.Imaging;
using System.IO;

namespace TestBadgeGenerator
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            BadgeServiceClient cl = new BadgeServiceClient("http://powerdev:800/badges");

            BadgeGenData data = new BadgeGenData();

            data.BadgeName = "test";

            data.BadgeData.Add(new BadgeTextContent(){ElementName = "text3003", Value=this.txtBadgeparam.Text});

            var imageData = await cl.CreateBadgeImage(data);

            if(imageData != null)
            {
                if(imageData.Length > 0)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                                        
                    try
                    {

                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                           
                            bitmapImage.SetSource(ms);

                            this.imgBadge.Source = bitmapImage;
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        
                    }
                }
            }
            
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}