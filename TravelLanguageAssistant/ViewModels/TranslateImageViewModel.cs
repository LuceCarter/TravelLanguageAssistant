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
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace TravelLanguageAssistant.ViewModels
{
	public class TranslateImageViewModel : BaseViewModel
	{

		public TranslateImageViewModel()
		{
			TakePhotoTranslateCommand = new Command(async () => await TakePhotoAsync());
		}

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

					var ocrResult = await computerVisionClient.RecognizePrintedTextInStreamAsync(true, photo.GetStream(), OcrLanguages.Unk);
					var language = ocrResult.Language;

					var sb = new StringBuilder();
					foreach (var region in ocrResult.Regions)
					{
						foreach (var line in region.Lines)
						{
							foreach (var word in line.Words)
							{
								sb.Append(word.Text + " ");
							}
						}
					}

					await TranslateText(sb.ToString().TrimEnd());
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

		private async Task TranslateText(string textToTranslate)
		{
			string host = "https://api.cognitive.microsofttranslator.com";
			string path = "/translate?api-version=3.0";

			// Translate to English
			string params_ = "&to=en";

			string uri = host + path + params_;

			System.Object[] body = new System.Object[] { new { Text = textToTranslate } };
			var requestBody = JsonConvert.SerializeObject(body);

			using (var client = new HttpClient())
			using (var request = new HttpRequestMessage())
			{
				request.Method = HttpMethod.Post;
				request.RequestUri = new Uri(uri);
				request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
				request.Headers.Add("Ocp-Apim-Subscription-Key", AzureKeys.AzureSpeechAPIKey);

				var response = await client.SendAsync(request);
				var responseBody = await response.Content.ReadAsStringAsync();

				JArray translatedText = JArray.Parse(responseBody);
				dynamic jo = JObject.Parse(translatedText[0].ToString());
				var translations = jo["translations"];
				var t = translations[0];
				var text = translations[0]["text"];

				AnalysedTextResult = text;
			}
		}

	}
}