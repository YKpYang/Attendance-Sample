using Attendance_Sample.Models;

namespace Attendance_Sample.Organizations;

public interface IAttendanceTarget
{
    int Id { get; }
    string Name { get; }
    IReadOnlyCollection<Employee> Employees { get; }
    bool ContainsEmployee(int employeeId);
}
