using Attendance_Sample.Models;

namespace Attendance_Sample.Services;

public interface IAttendanceService
{
    Task UpdateAsync(int employeeId, int year, int month);
    Task<List<Attendance>> GetAttendancesAsync();
    Task<Attendance?> GetAttendanceAsync(int employeeId, int year, int month);
}
