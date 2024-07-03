using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System_do_zarz¹dzania_projektami.Entites;
using System_do_zarz¹dzania_projektami.Services;
using Xunit;

namespace ProjectManagmentSystem.Tests
{
    public class ProjectServiceTests
    {

        [Fact]
        public void Create_WhenProjectIsNotNull_ShouldReturnProject()
        {
            // Arrange
            var databaseMock = new Mock<DatabaseSimulation>();
            var taskServiceMock = new Mock<ITaskService>();
            var projectService = new ProjectService(databaseMock.Object, taskServiceMock.Object);
            var newProject = new Project { Name = "Test Project" };

            // Act
            var createdProject = projectService.Create(newProject);

            // Assert
            Assert.NotNull(createdProject);
            Assert.Equal(newProject, createdProject);
        }

        [Fact]
        public void CrateWhenProjectIsNull_ShouldReturnArgumentNullException()
        {
            // Arrange
            var databaseMock = new Mock<DatabaseSimulation>();
            var taskServiceMock = new Mock<ITaskService>();
            var projectService = new ProjectService(databaseMock.Object, taskServiceMock.Object);

            // Act
            Action action = () => { projectService.Create(null); };

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}

