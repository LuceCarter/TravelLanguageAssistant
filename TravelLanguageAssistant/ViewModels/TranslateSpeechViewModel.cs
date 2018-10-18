using System;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;

namespace TravelLanguageAssistant.ViewModels
{
	public class TranslateSpeechViewModel
	{
		string
		public Command RecordConversationCommand
		{
			get
			{
				return new Command(() =>
				{
					Application.Current.MainPage.DisplayAlert("Record Clicked!", "", "OK");
					Analytics.TrackEvent("Record Speech Clicked!");
				});
			}
		}
	}
}