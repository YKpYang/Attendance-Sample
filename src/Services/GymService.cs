using Attendance_Sample.Organizations;

namespace Attendance_Sample.Services;

public sealed class GymService : AttendanceTargetServiceBase
{
    public GymService(Gym gym, IAttendanceService attendanceService)
        : base(gym, attendanceService)
    {
    }
}
