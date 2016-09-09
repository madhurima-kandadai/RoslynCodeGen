using System;
using System.Generic;
using EntityProjectName;

namespace Employee
{
    public class Employee : IEmployeeRepository
    {
        public Employee(IDataContext context, IUnitofWOrk unitofWork)
        {
        }
    }
}
