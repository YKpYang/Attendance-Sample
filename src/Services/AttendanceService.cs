using Attendance_Sample.Models;
using Attendance_Sample.Organizations;

namespace Attendance_Sample.Services;

public class AttendanceService : IAttendanceService
{
    private const int MaxWorkDaysPerMonth = 20;
    private const int InitialWorkDays = 0;
    private const int InitialPaidHolidays = 1;
    private readonly Dictionary<int, List<Attendance>> _attendancesByTarget = new();
    private readonly object _syncRoot = new();

    public Task UpdateAsync(IAttendanceTarget target, int employeeId, int year, int month)
    {
        ArgumentNullException.ThrowIfNull(target);

        ValidateEmployeeAndPeriod(employeeId, year, month);
        ValidateEmployeeBelongsToTarget(target, employeeId);

        int currentWorkDays;
        int paidHolidays;

        lock (_syncRoot)
        {
            var attendances = GetOrCreateAttendanceList(target.Id);
            var attendance = attendances.FirstOrDefault(x => x.EmployeeId == employeeId && x.Year == year && x.Month == month);

            if (attendance is null)
            {
                attendance = CreateAttendance(employeeId, year, month);
                attendances.Add(attendance);
            }

            if (attendance.WorkDays < MaxWorkDaysPerMonth)
            {
                attendance.WorkDays += 1;
            }

            currentWorkDays = attendance.WorkDays;
            paidHolidays = attendance.PaidHolidays;
        }

        Console.WriteLine($"TargetId={target.Id}, EmployeeId={employeeId}, Year={year}, Month={month}, WorkDays={currentWorkDays}, PaidHolidays={paidHolidays}");
        return Task.CompletedTask;
    }

    public Task<List<Attendance>> GetAttendancesAsync(IAttendanceTarget target)
    {
        ArgumentNullException.ThrowIfNull(target);

        lock (_syncRoot)
        {
            var attendances = GetAttendanceList(target.Id);
            return Task.FromResult(attendances
                .OrderBy(x => x.EmployeeId)
                .ThenBy(x => x.Year)
                .ThenBy(x => x.Month)
                .Select(CloneAttendance)
                .ToList());
        }
    }

    public Task<Attendance?> GetAttendanceAsync(IAttendanceTarget target, int employeeId, int year, int month)
    {
        ArgumentNullException.ThrowIfNull(target);

        ValidateEmployeeAndPeriod(employeeId, year, month);
        ValidateEmployeeBelongsToTarget(target, employeeId);

        lock (_syncRoot)
        {
            var attendance = GetAttendanceList(target.Id)
                .FirstOrDefault(x => x.EmployeeId == employeeId && x.Year == year && x.Month == month);

            return Task.FromResult(attendance is null ? null : CloneAttendance(attendance));
        }
    }

    public void EntryInit(IAttendanceTarget target, IAttendanceData attendanceData)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(attendanceData);

        ValidateEmployeeAndPeriod(attendanceData.EmployeeId, attendanceData.Year, attendanceData.Month);
        ValidateEmployeeBelongsToTarget(target, attendanceData.EmployeeId);

        lock (_syncRoot)
        {
            var attendances = GetOrCreateAttendanceList(target.Id);
            var attendance = attendances.FirstOrDefault(x => x.EmployeeId == attendanceData.EmployeeId && x.Year == attendanceData.Year && x.Month == attendanceData.Month);

            if (attendance is null)
            {
                attendances.Add(CreateAttendance(attendanceData.EmployeeId, attendanceData.Year, attendanceData.Month));
            }
        }
    }

    private static void ValidateEmployeeAndPeriod(int employeeId, int year, int month)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(employeeId));
        }

        if (year < 2000 || year > 2100)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (month is < 1 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }
    }

    private static void ValidateEmployeeBelongsToTarget(IAttendanceTarget target, int employeeId)
    {
        if (!target.ContainsEmployee(employeeId))
        {
            throw new InvalidOperationException($"EmployeeId={employeeId} does not belong to {target.GetType().Name}Id={target.Id}.");
        }
    }

    private List<Attendance> GetOrCreateAttendanceList(int targetId)
    {
        if (!_attendancesByTarget.TryGetValue(targetId, out var attendances))
        {
            attendances = new List<Attendance>();
            _attendancesByTarget[targetId] = attendances;
        }

        return attendances;
    }

    private List<Attendance> GetAttendanceList(int targetId)
    {
        return _attendancesByTarget.TryGetValue(targetId, out var attendances)
            ? attendances
            : new List<Attendance>();
    }

    private static Attendance CreateAttendance(int employeeId, int year, int month)
    {
        return new Attendance
        {
            EmployeeId = employeeId,
            Year = year,
            Month = month,
            WorkDays = InitialWorkDays,
            PaidHolidays = InitialPaidHolidays
        };
    }

    private static Attendance CloneAttendance(Attendance attendance)
    {
        return new Attendance
        {
            EmployeeId = attendance.EmployeeId,
            Year = attendance.Year,
            Month = attendance.Month,
            WorkDays = attendance.WorkDays,
            PaidHolidays = attendance.PaidHolidays
        };
    }
}
