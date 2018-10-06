using System;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;
using Plugin.Media;
using Xamarin.Essentials;

namespace TravelLanguageAssistant.ViewModels
{
	public class TranslateImageViewModel : BaseViewModel
	{

		public TranslateImageViewModel()
		{

		}

		public Image image;

		public ICommand TakePhotoTranslateCommand
		{
			get => new Command(async () =>
			{
				CrossMedia.Current.Initialize();

				if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
				{
					var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
					{
						Directory = "Travel Language Assistant",
						Name = "ImageForTranslation.jpg"
					});

					if (file == null) return;

				}
				else
				{
					Application.Current.MainPage.DisplayAlert("No Camera", "No Camera Available.", "OK");
					return;
				}

			});
		}

	}


}

