using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System_do_zarządzania_projektami.Entites;
using System_do_zarządzania_projektami.Services;
using Xunit;
using FluentAssertions;

namespace ProjectManagmentSystem.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<IDatabaseSimulation> _databaseMock;
        private readonly TaskService _taskService;


        public TaskServiceTests()
        {
            _databaseMock = new Mock<IDatabaseSimulation>();
            _taskService = new TaskService(_databaseMock.Object);
        }
        [Fact]
        public void Create_WhenTaskItemIsNotNull_ReturnsTaskItem()
        {
            // Arrange
            var project = new Project { Id = 1 };
            var projects = new List<Project> { project };

            _databaseMock.Setup(p => p.Projects).Returns(projects);
            var idProject = 1;
            var taskItem = new TaskItem();

            // Act
            var task = _taskService.Create(idProject, taskItem);

            // Assert
            task.Should().NotBeNull();
            task.Should().BeOfType<TaskItem>();
            task.Should().Be(taskItem);
        }
    }
}
