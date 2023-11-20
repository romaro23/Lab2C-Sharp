using Lab2;
//1
EmployeeCollection<string> employeesOne = new EmployeeCollection<string>(emp => emp.ToString());
employeesOne.CollectionName = "Employees1";
EmployeeCollection<string> employeesTwo = new EmployeeCollection<string>(emp => emp.ToString());
employeesTwo.CollectionName = "Employees2";
//2
Listener<string> listener = new Listener<string>(); 
employeesOne.EmployeesChanged += listener.EmployeesChanged;
employeesTwo.EmployeesChanged += listener.EmployeesChanged;
//3.1
employeesOne.AddDefaults();
Employee firstEmployee = new Employee(new Person("Petro", "Petrov", DateTime.Parse("01.01.2001")), "Intern", TimeWork.FullTime, 431);
Employee secondEmployee = new Employee(new Person("Maksim", "Maksimov", DateTime.Parse("01.01.2001")), "Senior", TimeWork.Free, 700);
employeesOne.AddEmployee(firstEmployee, secondEmployee);
Employee thirdEmployee = new Employee(new Person("Ivan", "Ivanov", DateTime.Parse("01.01.2001")), "Middle", TimeWork.PartTime, 999);
employeesTwo.AddEmployee(thirdEmployee);
//3.2
employeesOne.SetSalary(firstEmployee.ToString(), 31);
employeesTwo.SetPosition(thirdEmployee.ToString(), "Junior");
//3.3
Employee newEmployees = new Employee(new Person("!!!!!", "?????", DateTime.Parse("01.01.2001")), "Senior", TimeWork.FullTime, 430);
Employee employeeOld = employeesOne[new Employee().ToString()];
employeesOne.Replace(employeesOne[new Employee().ToString()], newEmployees);
//3.4
employeeOld.Salary = 20;
//4
Console.WriteLine("================================================");
Console.WriteLine(listener);
Console.WriteLine("================================================");
//5.1
Console.WriteLine(employeesOne.MinSalary);
Console.WriteLine("================================================");
//5.2
var filteredEmloyees = employeesOne.TimeWorkForm(TimeWork.Free);
foreach(var employee in filteredEmloyees)
{
    Console.WriteLine(employee);
}
Console.WriteLine("================================================");
//5.3
var groupedEmployees = employeesOne.Grouping;
foreach(var group in groupedEmployees)
{
    foreach(var employee in group)
    {
        Console.WriteLine(employee);
    }
}
Console.WriteLine("================================================");
//5.4
Console.WriteLine(employeesOne.InSalaryRange(400, 600));
Console.WriteLine("================================================");