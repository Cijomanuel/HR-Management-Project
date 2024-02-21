namespace HrManagementSystem.Core.Startup
{
	public class TimeBasedKeyForRequestReplay
	{
		public bool Enabled { get; set; } = true;
		public int KeyValiditySeconds { get; set; } = 120;
	}
}