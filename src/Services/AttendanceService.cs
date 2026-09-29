using Attendance_Sample.Models;

namespace Attendance_Sample.Services;

public class AttendanceService : IAttendanceService
{
    private readonly List<Attendance> _attendances = new();

    public Task UpdateAsync(int employeeId, int year, int month)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        if (year < 2000 || year > 2100)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (month < 1 || month > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        var attendance = _attendances.FirstOrDefault(x => x.EmployeeId == employeeId && x.Year == year && x.Month == month);

        if (attendance is null)
        {
            attendance = new Attendance
            {
                EmployeeId = employeeId,
                Year = year,
                Month = month,
                WorkDays = 20,
                PaidHolidays = 1
            };

            _attendances.Add(attendance);
        }
        else
        {
            attendance.WorkDays += 1;
        }

        Console.WriteLine($"Attendance updated: EmployeeId={employeeId}, Year={year}, Month={month}, WorkDays={attendance.WorkDays}, PaidHolidays={attendance.PaidHolidays}");

        return Task.CompletedTask;
    }

    public Task<List<Attendance>> GetAttendancesAsync()
    {
        var attendances = _attendances
            .OrderBy(x => x.EmployeeId)
            .ThenBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();

        return Task.FromResult(attendances);
    }

    public Task<Attendance?> GetAttendanceAsync(int employeeId, int year, int month)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        if (year < 2000 || year > 2100)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (month < 1 || month > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        var attendance = _attendances.FirstOrDefault(x => x.EmployeeId == employeeId && x.Year == year && x.Month == month);

        return Task.FromResult(attendance);
    }
}
