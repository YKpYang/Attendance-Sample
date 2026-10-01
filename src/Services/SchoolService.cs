using Attendance_Sample.Organizations;

namespace Attendance_Sample.Services;

public sealed class SchoolService : AttendanceTargetServiceBase
{
    public SchoolService(School school, IAttendanceService attendanceService)
        : base(school, attendanceService)
    {
    }
}
