namespace Attendance_Sample.Services;

public interface IAttendanceData
{
    int EmployeeId { get; set; }
    int Year { get; set; }
    int Month { get; set; }
}
