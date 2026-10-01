using Attendance_Sample.Models;
using Attendance_Sample.Organizations;
using Attendance_Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var company = new Company
{
    Id = 1,
    Name = "Fujitsu"
};

var school = new School
{
    Id = 2,
    Name = "Tokyo Academy"
};

var gym = new Gym
{
    Id = 3,
    Name = "PowerFit"
};

var companyEmployee = new Employee
{
    Id = 1001,
    Name = "Alice",
    OrganizationId = company.Id
};

var schoolEmployee = new Employee
{
    Id = 2001,
    Name = "Bob",
    OrganizationId = school.Id
};

var gymEmployee = new Employee
{
    Id = 3001,
    Name = "Charlie",
    OrganizationId = gym.Id
};

company.Employees.Add(companyEmployee);
school.Employees.Add(schoolEmployee);
gym.Employees.Add(gymEmployee);

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(company);
builder.Services.AddSingleton(school);
builder.Services.AddSingleton(gym);
builder.Services.AddSingleton<IAttendanceService, AttendanceService>();
builder.Services.AddSingleton<CompanyService>();
builder.Services.AddSingleton<SchoolService>();
builder.Services.AddSingleton<GymService>();

using var app = builder.Build();
var companyService = app.Services.GetRequiredService<CompanyService>();
var schoolService = app.Services.GetRequiredService<SchoolService>();
var gymService = app.Services.GetRequiredService<GymService>();

var companyAttendanceData = new AttendanceData
{
    EmployeeId = companyEmployee.Id,
    Year = 2026,
    Month = 9
};

var schoolAttendanceData = new AttendanceData
{
    EmployeeId = schoolEmployee.Id,
    Year = 2026,
    Month = 9
};

var gymAttendanceData = new AttendanceData
{
    EmployeeId = gymEmployee.Id,
    Year = 2026,
    Month = 9
};

companyService.EntryInit(companyAttendanceData);
schoolService.EntryInit(schoolAttendanceData);
gymService.EntryInit(gymAttendanceData);

for (var i = 0; i < 2; i++)
{
    await companyService.UpdateAsync(companyAttendanceData.EmployeeId, companyAttendanceData.Year, companyAttendanceData.Month);
    await schoolService.UpdateAsync(schoolAttendanceData.EmployeeId, schoolAttendanceData.Year, schoolAttendanceData.Month);
    await gymService.UpdateAsync(gymAttendanceData.EmployeeId, gymAttendanceData.Year, gymAttendanceData.Month);
}

var attendanceService = app.Services.GetRequiredService<IAttendanceService>();
var companyAttendance = await companyService.GetAttendanceAsync(companyAttendanceData.EmployeeId, companyAttendanceData.Year, companyAttendanceData.Month);
var schoolAttendance = await schoolService.GetAttendanceAsync(schoolAttendanceData.EmployeeId, schoolAttendanceData.Year, schoolAttendanceData.Month);
var gymAttendance = await gymService.GetAttendanceAsync(gymAttendanceData.EmployeeId, gymAttendanceData.Year, gymAttendanceData.Month);

var allRecords = await attendanceService.GetAttendancesAsync(company);
allRecords.AddRange(await attendanceService.GetAttendancesAsync(school));
allRecords.AddRange(await attendanceService.GetAttendancesAsync(gym));

Console.WriteLine($"Company employee: {companyEmployee.Name} ({company.Name}) -> Attendance exists: {companyAttendance is not null}");
Console.WriteLine($"School employee: {schoolEmployee.Name} ({school.Name}) -> Attendance exists: {schoolAttendance is not null}");
Console.WriteLine($"Gym employee: {gymEmployee.Name} ({gym.Name}) -> Attendance exists: {gymAttendance is not null}");
Console.WriteLine("Attendance records:");

foreach (var item in allRecords.OrderBy(x => x.EmployeeId))
{
    Console.WriteLine($"- EmployeeId={item.EmployeeId}, Year={item.Year}, Month={item.Month}, WorkDays={item.WorkDays}, PaidHolidays={item.PaidHolidays}");
}
