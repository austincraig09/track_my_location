namespace track_my_location;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage(); // Ensure this points to MainPage
    }
}
