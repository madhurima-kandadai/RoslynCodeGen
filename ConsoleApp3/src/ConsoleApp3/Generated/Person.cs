using System;
using System.Generic;
using EntityProjectName;

namespace Person
{
    public class Person : IPersonRepository
    {
        public Person(IDataContext context, IUnitofWOrk unitofWork)
        {
        }
    }
}
