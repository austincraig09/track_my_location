using System;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;

namespace track_my_location
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetCurrentLocationButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Check and request runtime permission for location.
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    var request = new GeolocationRequest(
                        GeolocationAccuracy.Best,
                        TimeSpan.FromSeconds(10)
                    );
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        var mapPosition = new Location(location.Latitude, location.Longitude);

                        LocationLabel.Text =
                            $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";

                        MyMap.MoveToRegion(
                            MapSpan.FromCenterAndRadius(mapPosition, Distance.FromKilometers(1))
                        );

                        Console.WriteLine(
                            $"[DEBUG] Acquired: Lat={location.Latitude}, Lon={location.Longitude}"
                        );
                    }
                    else
                    {
                        LocationLabel.Text =
                            "Location is null. Ensure GPS or mock location is set.";
                    }
                }
                else
                {
                    LocationLabel.Text = "Permission denied. Cannot fetch location.";
                }
            }
            catch (Exception ex)
            {
                LocationLabel.Text = $"Error: {ex.Message}";
                Console.WriteLine($"[DEBUG] Exception: {ex}");
            }
        }
    }
}
