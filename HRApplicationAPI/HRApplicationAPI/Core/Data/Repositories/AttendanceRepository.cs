using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HrDbContext context;

        public AttendanceRepository(HrDbContext context)
        {
            this.context = context;
        }
        public Attendance GetPresentAttendance(int Id)
        {
            try
            {
                DateTime now = DateTime.Now;
                var tmp = context.Attendances.Where(x => x.EmployeeId == Id).Where(x => DateTime.Compare(x.Date, now) == 0).ToList();
                if (tmp.Count > 0)
                {
                    Attendance attendance = new Attendance()
                    {
                        Date = tmp[0].Date,
                        AttendenceId = tmp[0].AttendenceId,
                        EmployeeId = tmp[0].EmployeeId,
                        Session1TimeIn = tmp[0].Session1TimeIn,
                        Session1TimeOut = tmp[0].Session1TimeOut,
                        Session2TimeIn = tmp[0].Session2TimeIn,
                        Session2TimeOut = tmp[0].Session2TimeOut,
                        Session3TimeIn = tmp[0].Session3TimeIn,
                        Session3TimeOut = tmp[0].Session3TimeOut,
                        TotalTime = tmp[0].TotalTime
                    };
                    return attendance;
                }
                Attendance attendance1 = new Attendance();
                return attendance1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
