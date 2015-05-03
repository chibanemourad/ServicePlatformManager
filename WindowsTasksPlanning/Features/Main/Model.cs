using System.Collections.ObjectModel;
using WindowsTasksPlanning.Model;

namespace WindowsTasksPlanning.Features.Main
{
	public class Model : IPagingInformation
	{
		public ObservableCollection<ServicePlatform> Actions { get; set; }

		public int NumberOfPages { get; set; }

		public int CurrentPage { get; set; }
	}
}