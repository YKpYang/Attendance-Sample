using Attendance_Sample.Organizations;

namespace Attendance_Sample.Services;

public sealed class CompanyService : AttendanceTargetServiceBase
{
    public CompanyService(Company company, IAttendanceService attendanceService)
        : base(company, attendanceService)
    {
    }
}
