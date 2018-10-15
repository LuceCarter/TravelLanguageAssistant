using System;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;
using Plugin.Media;
using Xamarin.Essentials;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using TravelLanguageAssistant.Services;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using System.Dynamic;
using System.Text;


namespace TravelLanguageAssistant.ViewModels
{
	public class TranslateImageViewModel : BaseViewModel
	{

		public TranslateImageViewModel()
		{
			TakePhotoTranslateCommand = new Command(async () => await TakePhotoAsync());
		}

		public Image image;

		MediaFile photo;
		StreamImageSource imageSource;

		public StreamImageSource ImageForAnalysis
		{
			get => imageSource;
			set => SetProperty(ref imageSource, value);
		}

		public ICommand TakePhotoTranslateCommand { get; }
		private async Task TakePhotoAsync()
		{
			try
			{
				if (CrossMedia.Current.IsCameraAvailable)
				{
					photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
					{
						PhotoSize = PhotoSize.Small
					});

					ImageForAnalysis = (StreamImageSource)ImageSource.FromStream(() => photo.GetStream());

					ComputerVisionClient computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(AzureKeys.AzureComputerVisonAPIKey));
					computerVisionClient.Endpoint = AzureKeys.AzureComputerVisionEndpoint;

					var stuff = await computerVisionClient.RecognizePrintedTextInStreamAsync(true, photo.GetStream(), OcrLanguages.Unk);
					var language = stuff.Language;

					var sb = new StringBuilder();
					foreach (var region in stuff.Regions)
					{
						foreach (var line in region.Lines)
						{
							foreach (var word in line.Words)
							{
								sb.Append(" " + word.Text);
							}
						}
					}
					AnalysedTextResult = sb.ToString();
				}
			}

			catch (Exception e)
			{
				await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
			}
		}

		string textResult;
		public String AnalysedTextResult
		{
			get => textResult;
			set => SetProperty(ref textResult, value);
		}



	}
}