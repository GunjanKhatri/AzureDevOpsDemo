using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using AzureDevOpsDemo.Models;

namespace AzureDevOpsDemo.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConcurrentDictionary<Guid, Employee> _store = new();

        public Task<Employee> AddAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _store[employee.Id] = employee;
            return Task.FromResult(employee);
        }

        public Task<Employee?> GetAsync(Guid id)
        {
            _store.TryGetValue(id, out var employee);
            return Task.FromResult(employee);
        }

        public Task<Employee?> UpdateAsync(Employee employee)
        {
            if (!_store.ContainsKey(employee.Id))
                return Task.FromResult<Employee?>(null);

            _store[employee.Id] = employee;
            return Task.FromResult<Employee?>(employee);
        }
    }
}