using FluentAssertions;
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
        private Mock<IDatabaseSimulation> _databaseMock;
        private Mock<ITaskService> _taskServiceMock;
        private ProjectService _projectService;
        private List<Project> _projects;
        public ProjectServiceTests()
        {
            _projects = new List<Project>();
            _databaseMock = new Mock<IDatabaseSimulation>();
            _taskServiceMock = new Mock<ITaskService>();

            _databaseMock.Setup(db => db.Projects).Returns(_projects);
            _projectService = new ProjectService(_databaseMock.Object, _taskServiceMock.Object);
        }

        [Fact]
        public void Create_WhenProjectIsNotNull_ShouldReturnProject()
        {
            // Arrange
            var newProject = new Project { Name = "Test Project" };

            // Act
            var createdProject = _projectService.Create(newProject);

            // Assert
            Assert.NotNull(createdProject);
            newProject.Should().Be(createdProject);
            _projects.Should().Contain(newProject);
        }

        [Fact]
        public void CrateWhenProjectIsNull_ShouldReturnArgumentNullException()
        {
            // Act
            Action action = () => { _projectService.Create(null); };

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}

