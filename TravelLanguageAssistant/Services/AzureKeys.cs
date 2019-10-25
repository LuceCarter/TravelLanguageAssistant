using System;
namespace TravelLanguageAssistant.Services
{
	public static class AzureKeys
	{

        // Start by registering for an account at https://customvision.ai
        // Then create a new project.
        // From the settings tab, find:
        // Prediction Key
        // Project Id
        // and update the values below
        public const string AzureComputerVisionEndpoint = "https://westeurope.api.cognitive.microsoft.com/";
        public const string AzureComputerVisonAPIKey = "76018f68ebb9416fa03a6b502344ceac";
        public const string AzureSpeechAPIKey = "866d3ba4fdee4de68f25762499c6cd3c";
    }
}
