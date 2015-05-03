namespace WindowsTasksPlanning.Features.Main
{
	public interface IPagingInformation
	{
		int NumberOfPages { get; }

		int CurrentPage { get; }
	}
}