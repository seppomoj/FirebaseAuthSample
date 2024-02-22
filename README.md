**What is FirebaseAuthSample**

I was coding a separate cross-platform app and run into a need of authentication method for it. It was frustrating to see that there were very few options for .NET MAUI available but I decided to go with Firebase authentication as I have used before with Android development.
There is a limited number of Firebase Auth packages available in NuGet but I decided to use FirebaseAuthentication.net from Step Up Labs. https://github.com/step-up-labs/firebase-authentication-dotnet.
FirebaseAuthentication.net has provided some sample code in their github, but alas, there is no sample code for .NET MAUI.

**Applications etc. needed**

- Visual Studio 2022
- Firebaseauthentication.net by Step Up Labs, Inc.
- CommunityToolKit.Maui by Microsoft
 
**Step by step instructions for trying it out**

- Clone the repository https://github.com/seppomoj/FirebaseAuthSample/

- Install Firebaseauthentication.net package with Nuget Package manager. (Tools - Nuget Package manager - Manage NuGet Packages for Solution...)
  - Check box next to Project and click Install

- Install CommunityToolkit.Maui package with Nuget Package manager. (Tools - Nuget Package manager - Manage NuGet Packages for Solution...)
  - Check box next to Project and click Install

- Set up Firebase
  - Press *Get started* button on https://firebase.google.com/products-build?authuser=1
  - Press *Create a project*, give your project a name and select if you want to enable Google Analytics. (if you do, select Analytics location based on your location)
  - Press *Create project*
  - Edit project settings
  - Add a Web App and fill in the information
  - Click Authentication on left side of the screen
  - Click Get started
  - Click Email/Password and enable it
  - Once it is enabled you can find your API-key and Authdomain in Project setting screen

- Fill in your API-key and AuthDomain in LoginPage.xaml.cs
- Build and Deploy
  


