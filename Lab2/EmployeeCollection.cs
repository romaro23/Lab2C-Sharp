using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    delegate TKey KeySelector<TKey>(Employee emp);
    delegate void EmployeesChangedHandler<TKey>(object source, EmployeesChangedEventArgs<TKey> args);
    internal class EmployeeCollection<TKey>
    {
        public string CollectionName { get; set; }
        public int MinSalary
        {
            get
            { 
                if(employees.Any())
                {
                    return employees.Min(e => e.Value.Salary);
                }
                return 1;
            }
        }
        public IEnumerable<IGrouping<TimeWork,KeyValuePair<TKey,Employee>>> Grouping
        {
            get
            {
                return employees.GroupBy(e => e.Value.WorkTime);
            }
        }
        private Dictionary<TKey, Employee>? employees = new Dictionary<TKey, Employee>();
        public Employee this[TKey key]
        {
            get
            {
                return employees[key]; 
            }
            set
            {
                employees[key] = value;
            }
        }
        private KeySelector<TKey> keySelector;
        public event EmployeesChangedHandler<TKey> EmployeesChanged;
        public EmployeeCollection(KeySelector<TKey> keySelector) 
        {
            this.keySelector = keySelector;
        }
        public void SetPosition(TKey key, string position)
        {
            employees[key].Position = position;
        }
        public void SetSalary(TKey key, int salary)
        {
            employees[key].Salary = salary;
        }
        public void AddDefaults()
        {
            Employee employee = new Employee();
            TKey key = keySelector(employee);
            employees.Add(key, employee);
            SubscribeToEmployeePropertyChanged(employee);
            EmployeesChanged?.Invoke(this, new EmployeesChangedEventArgs<TKey>(CollectionName, Update.Add, "", key));
        }
        public void AddEmployee(params Employee[] employees)
        {
            for(int i = 0; i < employees.Length; i++)
            {
                Employee employee = employees[i];
                TKey key = keySelector(employee);
                this.employees.Add(key, employee);
                SubscribeToEmployeePropertyChanged(employee);
                EmployeesChanged?.Invoke(this, new EmployeesChangedEventArgs<TKey>(CollectionName, Update.Add, "", key));
            }
        }
        public bool Replace(Employee emOld, Employee emNew)
        {
            if(employees.ContainsValue(emOld))
            {
                TKey keyOld = keySelector(emOld);
                UnsubscribeFromEmployeePropertyChanged(employees[keyOld]);
                employees[keyOld] = emNew;
                SubscribeToEmployeePropertyChanged(employees[keyOld]);
                EmployeesChanged?.Invoke(this, new EmployeesChangedEventArgs<TKey>(CollectionName, Update.Replace, "", keyOld));
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            var keyValuePairs = employees.Select(kv => $"{kv.Key}: {kv.Value}");
            return string.Join(", ", keyValuePairs);
        }
        public string ToShortString()
        {
            var keyValuePairs = employees.Select(kv => $"{kv.Key}: {kv.Value.ToShortString()}");
            return string.Join (", ", keyValuePairs);
        }
        public IEnumerable<KeyValuePair<TKey, Employee>> TimeWorkForm(TimeWork value)
        {
            return employees.Where(e => e.Value.WorkTime == value);
        }
        public int InSalaryRange(int minValue, int maxValue)
        {
            return employees.Count(e => e.Value.Salary >= minValue && e.Value.Salary <= maxValue);
        }
        private void SubscribeToEmployeePropertyChanged(Employee employee)
        {
            employee.PropertyChanged += HandleEmployeePropertyChanged;
        }

        private void UnsubscribeFromEmployeePropertyChanged(Employee employee)
        {
            employee.PropertyChanged -= HandleEmployeePropertyChanged;
        }

        private void HandleEmployeePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Employee employee)
            {
                TKey key = keySelector(employee);
                EmployeesChanged?.Invoke(this, new EmployeesChangedEventArgs<TKey>(CollectionName, Update.Property, e.PropertyName, key));
            }
        }
    }
}
