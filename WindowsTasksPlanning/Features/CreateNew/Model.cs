using WindowsTasksPlanning.Infrastructure.Impl;
using WindowsTasksPlanning.Model;

namespace WindowsTasksPlanning.Features.CreateNew
{

	public class Model
	{
        public ServicePlatform Action { get; set; }

		public Observable<bool> AllowEditing { get; set; }
	}
}