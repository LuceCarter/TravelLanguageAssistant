using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TravelLanguageAssistant.ViewModels;

namespace TravelLanguageAssistant.Views
{
	public partial class TranslateSpeechPage : ContentPage
	{
		public TranslateSpeechPage()
		{
			InitializeComponent();
			BindingContext = new TranslateSpeechViewModel();
		}
	}
}
