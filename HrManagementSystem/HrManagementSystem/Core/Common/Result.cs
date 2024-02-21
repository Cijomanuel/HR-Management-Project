namespace HrManagementSystem.Core.Data.Entities.Common
{
	public abstract class Result
	{
		public Result()
		{
			Errors = new List<Error>();
		}
		public List<Error>? Errors { get; set; }

		public bool IsError => Errors != null && Errors.Any();
	}

	public class Result<T> : Result
	{
		public T? Response { get; set; }

		public string? WarningMessage { get; set; }
	}
}
