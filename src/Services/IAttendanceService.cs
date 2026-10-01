using Attendance_Sample.Models;
using Attendance_Sample.Organizations;

namespace Attendance_Sample.Services;

public interface IAttendanceService
{
    Task UpdateAsync(IAttendanceTarget target, int employeeId, int year, int month);
    Task<List<Attendance>> GetAttendancesAsync(IAttendanceTarget target);
    Task<Attendance?> GetAttendanceAsync(IAttendanceTarget target, int employeeId, int year, int month);
    void EntryInit(IAttendanceTarget target, IAttendanceData attendanceData);
}

public abstract class AttendanceTargetServiceBase
{
    protected readonly IAttendanceTarget _target;
    protected readonly IAttendanceService _attendanceService;

    protected AttendanceTargetServiceBase(IAttendanceTarget target, IAttendanceService attendanceService)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(attendanceService);

        _target = target;
        _attendanceService = attendanceService;
    }

    public Task UpdateAsync(int employeeId, int year, int month)
    {
        return _attendanceService.UpdateAsync(_target, employeeId, year, month);
    }

    public Task<List<Attendance>> GetAttendancesAsync()
    {
        return _attendanceService.GetAttendancesAsync(_target);
    }

    public Task<Attendance?> GetAttendanceAsync(int employeeId, int year, int month)
    {
        return _attendanceService.GetAttendanceAsync(_target, employeeId, year, month);
    }

    public void EntryInit(IAttendanceData attendanceData)
    {
        ArgumentNullException.ThrowIfNull(attendanceData);
        _attendanceService.EntryInit(_target, attendanceData);
    }
}

