namespace Attendance_Sample.Services;

public class AttendanceData : IAttendanceData
{
    public int EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
}
