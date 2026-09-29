namespace Attendance_Sample.Models;

public class Attendance
{
    public int EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int WorkDays { get; set; }
    public int PaidHolidays { get; set; }
}
