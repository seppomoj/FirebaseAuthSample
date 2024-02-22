namespace FirebaseAuthSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (LoginPage.userLoggedIn == true)
            {
                statusLabel.Text = "User logged in";
            }
            else
            {
                statusLabel.Text = "User NOT logged in";
            }
        }
    }
    

}
