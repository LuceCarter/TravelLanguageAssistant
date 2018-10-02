using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TravelLanguageAssistant.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TravelLanguageAssistant
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			AppCenter.Start("ios=c78f47f8-6b6c-4db7-b9fa-ad22ad36e759;" +
				  "android=c7b2b827-47ea-495a-b3a8-f015bf21cd5c;",
							typeof(Analytics), typeof(Crashes));
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
