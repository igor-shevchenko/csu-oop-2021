using System;
using System.Reflection.Emit;

namespace Payroll
{
    class Program
    {
        static void Main(string[] args)
        {
            var ivan = new ManagerEmployee("Ivan", 100);
            var petr = new TechnicianEmployee("Petr", 50, Category.A);
            var vasily = new DriverEmployee("Vasily", 1, Category.C);

            var x = ivan.Name;

            Console.WriteLine(ivan.GetPayroll(new MonthlyReport
            {
                Bonus = 10
            }));
            Console.WriteLine(petr.GetPayroll(new MonthlyReport
            {
                Bonus = 10
            }));
            Console.WriteLine(vasily.GetPayroll(new MonthlyReport
            {
                Hours = 160,
                Bonus = 10
            }));
        }
    }

    enum Category
    {
        A,
        B,
        C
    }

    class MonthlyReport
    {
        public int? Hours { get; set; }
        public decimal Bonus { get; set; }
    }

    interface IEmployee
    {
        string Name { get; }

        decimal GetPayroll(MonthlyReport report);
    }

    abstract class Employee : IEmployee
    {
        protected Employee(string name, IPayrollCalculator calculator)
        {
            Name = name;
            this.calculator = calculator;
        }

        protected readonly IPayrollCalculator calculator;

        public string Name { get; }

        public decimal GetPayroll(MonthlyReport report)
        {
            return calculator.GetPayroll(report);
        }
    }

    interface IPayrollCalculator
    {
        decimal GetPayroll(MonthlyReport report);
    }

    class SalaryCalculator : IPayrollCalculator
    {
        public SalaryCalculator(decimal salary)
        {
            Salary = salary;
        }

        public decimal Salary { get; }

        public decimal GetPayroll(MonthlyReport report)
        {
            return Salary;
        }
    }

    class HourlyCalculator : IPayrollCalculator
    {
        public HourlyCalculator(decimal hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public decimal HourlyRate { get; }

        public decimal GetPayroll(MonthlyReport report)
        {
            if (report.Hours == null)
            {
                throw new Exception("Hours can't be null for");
            }
            return HourlyRate * (decimal)report.Hours;
        }
    }

    class CategoryCalculator : IPayrollCalculator
    {
        private readonly IPayrollCalculator _innerCalculator;

        public CategoryCalculator(Category category, IPayrollCalculator innerCalculator)
        {
            _innerCalculator = innerCalculator;
            Category = category;
        }

        public Category Category { get; }

        public decimal GetPayroll(MonthlyReport report)
        {
            var payroll = _innerCalculator.GetPayroll(report);
            switch (Category)
            {
                case Category.A:
                    return payroll * 1.25m;
                case Category.B:
                    return payroll * 1.15m;
                case Category.C:
                    return payroll;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    class BonusCalculator : IPayrollCalculator
    {
        private readonly IPayrollCalculator _innerCalculator;

        public BonusCalculator(IPayrollCalculator innerCalculator)
        {
            _innerCalculator = innerCalculator;
        }

        public decimal GetPayroll(MonthlyReport report)
        {
            return _innerCalculator.GetPayroll(report) + report.Bonus;
        }
    }

    class ManagerEmployee : Employee
    {
        public ManagerEmployee(string name, decimal salary) 
            : base(name, new BonusCalculator(new SalaryCalculator(salary)))
        {
        }
    }

    class TechnicianEmployee : Employee
    {
        public TechnicianEmployee(string name, decimal salary, Category category) 
            : base(name, new BonusCalculator(new CategoryCalculator(category, new SalaryCalculator(salary))))
        {
        }
    }

    class DriverEmployee : Employee
    {
        public DriverEmployee(string name, decimal hourlyRate, Category category) 
            : base(name, new BonusCalculator(new CategoryCalculator(category, new HourlyCalculator(hourlyRate))))
        {
        }
    }
    class ContactorDriverEmployee : Employee
    {
        public ContactorDriverEmployee(string name, decimal hourlyRate, Category category) 
            : base(name, new CategoryCalculator(category, new HourlyCalculator(hourlyRate)))
        {
        }
    }
}
