using System;
namespace TravelLanguageAssistant.Services
{
	public static class AzureKeys
	{
#error You need to set up your API keys.
		// Start by registering for an account at https://customvision.ai
		// Then create a new project.
		// From the settings tab, find:
		// Prediction Key
		// Project Id
		// and update the values below
		public const string AzureComputerVisonAPIKey = "<API KEY GOES HERE>";
		public const string AzureSpeechAPIKey = "<API KEY GOES HERE>";
	}
}
