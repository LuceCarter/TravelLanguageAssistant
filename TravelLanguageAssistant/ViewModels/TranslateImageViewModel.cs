using System;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;

namespace TravelLanguageAssistant.ViewModels
{
	public class TranslateImageViewModel : BaseViewModel
	{

		public TranslateImageViewModel()
		{

		}

		public ICommand TakePhotoTranslateCommand
		{
			get
			{
				return new Command(() =>
				{
					// TODO: Call a photo taking method
				});
			}
		}
	}
}
