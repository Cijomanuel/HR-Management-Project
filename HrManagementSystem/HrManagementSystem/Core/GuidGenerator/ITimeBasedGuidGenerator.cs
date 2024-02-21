using System;

namespace HrManagementSystem.Core.GuidGenerator
{
	public interface ITimeBasedGuidGenerator
	{
		DateTime GetDateTimeFromGuid(Guid guid);
		Guid GenerateTimeBasedGuid(DateTime dateTime);
	}
}