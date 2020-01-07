using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using Supperxin.Linq;

namespace Supperxin.Library.Tests.Linq
{
    public class ExpressionBuilderTest
    {
        class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        [Fact]
        public void TestAnd()
        {
            //Given
            var employees = InitialTestEmployees();

            //When
            Expression<Func<Employee, bool>> nameFilter = e => e.Name.Contains("s");
            Expression<Func<Employee, bool>> ageFilter = e => e.Age > 25;
            Expression<Func<Employee, bool>> nameAgeFilter = nameFilter.And<Employee>(ageFilter);

            //Then
            Assert.Equal(2, employees.Where(nameFilter.Compile()).Count());
            Assert.Equal(2, employees.Where(ageFilter.Compile()).Count());
            Assert.Equal(1, employees.Where(nameAgeFilter.Compile()).Count());
        }

        private List<Employee> InitialTestEmployees()
        {
            return new List<Employee>() {
                new Employee { Name = "Jason", Age = 25 },
            new Employee { Name = "Andrew", Age = 30 },
            new Employee { Name = "Chris", Age = 35 } };
        }

        [Fact]
        public void TestOr()
        {
            //Given
            var employees = InitialTestEmployees();

            //When
            Expression<Func<Employee, bool>> nameFilter = e => e.Name.Contains("s");
            Expression<Func<Employee, bool>> ageFilter = e => e.Age < 35;
            Expression<Func<Employee, bool>> nameAgeFilter = nameFilter.Or<Employee>(ageFilter);

            //Then
            Assert.Equal(2, employees.Where(nameFilter.Compile()).Count());
            Assert.Equal(2, employees.Where(ageFilter.Compile()).Count());
            Assert.Equal(3, employees.Where(nameAgeFilter.Compile()).Count());
        }
    }
}
