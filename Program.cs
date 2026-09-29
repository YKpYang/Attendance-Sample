using Attendance_Sample.Models;
using Attendance_Sample.Services;

var company = new Company
{
    Id = 1,
    Name = "Contoso"
};

var employee = new Employee
{
    Id = 1001,
    Name = "Alice",
    CompanyId = company.Id
};

company.Employees.Add(employee);

var service = new AttendanceService();

await service.UpdateAsync(employee.Id, 2026, 9);
await service.UpdateAsync(employee.Id, 2026, 9);

var attendance = await service.GetAttendanceAsync(employee.Id, 2026, 9);
var records = await service.GetAttendancesAsync();

Console.WriteLine($"Employee: {employee.Name} ({company.Name})");
Console.WriteLine($"Attendance exists: {attendance is not null}");
Console.WriteLine("Attendance records:");

foreach (var item in records)
{
    Console.WriteLine($"- EmployeeId={item.EmployeeId}, Year={item.Year}, Month={item.Month}, WorkDays={item.WorkDays}, PaidHolidays={item.PaidHolidays}");
}
