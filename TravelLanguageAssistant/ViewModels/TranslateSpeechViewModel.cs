using System;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;
using System.Windows.Input;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using MvvmHelpers;

namespace TravelLanguageAssistant.ViewModels
{
	public class TranslateSpeechViewModel : BaseViewModel
	{
		private string _translateButtonText = "Record Conversation to Translate";
		public string RecordForTranslateButton
		{
			get => _translateButtonText;
			set => SetProperty(ref _translateButtonText, value);
		}

		public TranslateSpeechViewModel()
		{
			RecordConversationCommand = new Command(async () => await RecordAudio());
		}
		public ICommand RecordConversationCommand { get; }

		private async Task RecordAudio()
		{
			RecordForTranslateButton = "Recording in Progress";
			var recorder = new AudioRecorderService
			{
				StopRecordingOnSilence = true,
				StopRecordingAfterTimeout = true,
				TotalAudioTimeout = TimeSpan.FromSeconds(15)
			};

			try
			{
				if (!recorder.IsRecording)
				{
					var recordTask = await recorder.StartRecording();
					var audioFile = await recordTask;
					if (audioFile != null)
					{
						Application.Current.MainPage.DisplayAlert("Woo", audioFile, "OK");
					}
				}
				else
				{
					await recorder.StopRecording();
					RecordForTranslateButton = "Record Conversation to Translate";
				}
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}