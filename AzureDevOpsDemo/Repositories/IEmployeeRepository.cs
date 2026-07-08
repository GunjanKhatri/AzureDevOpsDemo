using System;
using System.Threading.Tasks;
using AzureDevOpsDemo.Models;

namespace AzureDevOpsDemo.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<Employee?> GetAsync(Guid id);
        Task<Employee?> UpdateAsync(Employee employee);
    }
}