﻿using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.RepositoryInterfaces
{
    public interface IAttendanceRepository
    {
        Attendance GetPresentAttendance(int Id);
    }
}