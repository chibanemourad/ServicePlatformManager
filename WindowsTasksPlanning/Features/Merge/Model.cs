using WindowsTasksPlanning.Infrastructure.Impl;
using WindowsTasksPlanning.Model;

namespace WindowsTasksPlanning.Features.Merge
{
    public class Model
	{
		public ServicePlatform DatabaseVersion { get; set;}
        public ServicePlatform UserVersion { get; set; }
		public Observable<bool> AllowEditing { get; set; }
	}
}