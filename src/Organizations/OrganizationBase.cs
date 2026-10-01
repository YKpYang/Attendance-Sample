using Attendance_Sample.Models;

namespace Attendance_Sample.Organizations;

public abstract class OrganizationBase : IAttendanceTarget
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Employee> Employees { get; } = new();

    IReadOnlyCollection<Employee> IAttendanceTarget.Employees => Employees;

    public bool ContainsEmployee(int employeeId)
    {
        return Employees.Any(employee => employee.Id == employeeId && employee.OrganizationId == Id);
    }
}
