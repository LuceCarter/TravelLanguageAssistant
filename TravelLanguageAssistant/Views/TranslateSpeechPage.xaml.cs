using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TravelLanguageAssistant.ViewModels;
using Plugin.AudioRecorder;
using System.Threading.Tasks;

namespace TravelLanguageAssistant.Views
{
	public partial class TranslateSpeechPage : ContentPage
	{
		AudioRecorderService recorder;

		public TranslateSpeechPage()
		{
			InitializeComponent();

			recorder = new AudioRecorderService
			{
				StopRecordingOnSilence = true,
				StopRecordingAfterTimeout = true,
				TotalAudioTimeout = TimeSpan.FromSeconds(15),
				AudioSilenceTimeout = TimeSpan.FromSeconds(2)
			};
		}

		async void Record_Clicked(object sender, EventArgs e)
		{
			await RecordAudio();
		}

		async Task RecordAudio()
		{
			try
			{
				if (!recorder.IsRecording)
				{
					RecordButton.IsEnabled = false;

					var audioRecordTask = await recorder.StartRecording();
					RecordButton.Text = "Stop Recording";
					RecordButton.IsEnabled = true;

					var recordResult = await audioRecordTask;

					RecordButton.Text = "Record";
				}
				else
				{
					RecordButton.IsEnabled = false;
					await recorder.StopRecording();
					RecordButton.IsEnabled = true;
				}
			}
			catch (Exception e)
			{
				Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
			}
		}
	}
}
