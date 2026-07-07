using System;
using System.Threading.Tasks;
using AzureDevOpsDemo.Controllers;
using AzureDevOpsDemo.Models;
using AzureDevOpsDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AzureDevOpsDemo.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        [Fact]
        public async Task AddEmployee_ReturnsCreated()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                    .ReturnsAsync((Employee e) => { e.Id = Guid.NewGuid(); return e; });

            var controller = new EmployeesController(mockRepo.Object);

            var dto = new EmployeeCreateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com"
            };

            var result = await controller.AddEmployee(dto);
            var created = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(EmployeesController.GetEmployee), created.ActionName);
            var returned = Assert.IsType<Employee>(created.Value);
            Assert.Equal(returned.Id, (Guid)created.RouteValues["id"]);
        }

        [Fact]
        public async Task GetEmployee_ReturnsOk_WhenFound()
        {
            var id = Guid.NewGuid();
            var employee = new Employee { Id = id, FirstName = "Jane", LastName = "Doe" };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetAsync(id)).ReturnsAsync(employee);

            var controller = new EmployeesController(mockRepo.Object);

            var actionResult = await controller.GetEmployee(id);
            var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returned = Assert.IsType<Employee>(ok.Value);
            Assert.Equal(id, returned.Id);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsNoContent_WhenUpdated()
        {
            var id = Guid.NewGuid();
            var existing = new Employee { Id = id, FirstName = "A", LastName = "B" };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetAsync(id)).ReturnsAsync(existing);
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync((Employee e) => e);

            var controller = new EmployeesController(mockRepo.Object);

            var dto = new EmployeeUpdateDto { FirstName = "Updated", LastName = "Name" };

            var result = await controller.UpdateEmployee(id, dto);
            Assert.IsType<NoContentResult>(result);
        }
    }
}