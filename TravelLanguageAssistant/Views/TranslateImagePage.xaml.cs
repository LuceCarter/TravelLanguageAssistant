using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TravelLanguageAssistant.ViewModels;

namespace TravelLanguageAssistant.Views
{
	public partial class TranslateImagePage : ContentPage
	{
		public TranslateImagePage()
		{
			InitializeComponent();
			BindingContext = new TranslateImageViewModel();
		}
	}
}
