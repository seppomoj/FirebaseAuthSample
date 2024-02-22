using CommunityToolkit.Maui.Core.Platform;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace FirebaseAuthSample;

public partial class LoginPage : ContentPage
{
    
    public readonly FirebaseAuthClient authClient;
    public static bool userLoggedIn;
    public string? errorMessage;

    public LoginPage()
    {
        InitializeComponent();
        
        Console.WriteLine("Login screen initialized.");


        var config = new FirebaseAuthConfig
        {
            ApiKey = "API key here",
            AuthDomain = "Auth domain here",
            Providers = new FirebaseAuthProvider[]
            {
                new GoogleProvider().AddScopes("email"),
                new EmailProvider()
            },
            
        };
        authClient = new FirebaseAuthClient(config);

    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {

        if (!userLoggedIn)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            await passwordEntry.HideKeyboardAsync(CancellationToken.None);
            await emailEntry.HideKeyboardAsync(CancellationToken.None);
            if (isEmailEmpty || isPasswordEmpty)
            {
                userLoggedIn = false;
            }
            else
            {
                await Task.Run(() => LoginUserAsync(emailEntry.Text, passwordEntry.Text));
                if (userLoggedIn)
                {
                    signUpButton.IsEnabled = false;
                    loginButton.Text = "Log out";
                    passwordEntry.IsEnabled = false;
                    emailEntry.IsEnabled = false;
                    // You may navigate to MainPage of the app after successful login

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
                    errorMessage = "";
                }

            }
        }
        else
        {
            LogoutUser();
            userLoggedIn = false;
            signUpButton.IsEnabled = true;
            loginButton.Text = "Login";
            passwordEntry.IsEnabled = true;
            emailEntry.IsEnabled = true;
        }

    }
    //TODO: avoid async void
    //make private void methods that call private async Tasks
    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {

        bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
        bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
        await passwordEntry.HideKeyboardAsync(CancellationToken.None);
        await emailEntry.HideKeyboardAsync(CancellationToken.None);
        if (isEmailEmpty || isPasswordEmpty)
        {
            userLoggedIn = false;
        }
        else
        {
            await Task.Run(() => SignUpAsync(emailEntry.Text, passwordEntry.Text));
            //check if operation was successful
            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
                errorMessage = "";
            }
        }

    }
    
        private void PasswordResetButton_Clicked(object sender, EventArgs e)
    {
        //resetting password is not yet implemented
    }

    protected async Task LoginUserAsync(string email, string password)
    {
        if (authClient is null)
        {
            Console.WriteLine("authclient is null");
        }
        else
        {
            try
            {
                await authClient.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("Sign in encountered an error: " + task.Exception.Message);
                        errorMessage = task.Exception.Message.ToString();
                        userLoggedIn = false;
                    }
                    if (task.IsCanceled && task is not null)
                    {
                        Console.WriteLine("Sign in was canceled.");
                        userLoggedIn = false;
                    }
                    Console.WriteLine("User:" + authClient.User.Info.Email + " logged in.");
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        return;
    }
    protected void LogoutUser()
    {
        if (authClient is null)
        {
            Console.WriteLine("authclient is null");
        }
        else
        {
            authClient.SignOut();
            Console.WriteLine("sign out:" + authClient);
            userLoggedIn = false;
        }
    }
    protected async Task SignUpAsync(string email, string password)
    {
        if (authClient is null)
        {
            Console.WriteLine("authclient is null");
        }
        else
        {
            Console.WriteLine("User signing up");

            await authClient.CreateUserWithEmailAndPasswordAsync(email, password, "Display Name").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception.Message);
                    errorMessage = task.Exception.Message.ToString();
                    userLoggedIn = false;
                }
                if (task.IsCompletedSuccessfully)
                {
                    Console.WriteLine("User added succesfully");
                }
                return;
            });

        }
    }

}