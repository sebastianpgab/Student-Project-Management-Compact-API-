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
        public void Create_WhenProjectIsNull_ShouldReturnArgumentNullException()
        {
            // Act
            Action action = () => { _projectService.Create(null); };

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Delete_WhenIdExistInDatabase_RemovesProject()
        {
            //Arrange
            var projects = new List<Project> { new Project { Id = 2, Name = "Test Project" } };
            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //Act
            _projectService.Delete(2);

            //Assert
            projects.Should().BeEmpty();
        }

        [Fact]
        public void Delete_WhenIdIsNegativeOrZero_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var negativeId = -1;
            var zeroId = 0;

            // Act
            Action deleteWithNegativeId = () => _projectService.Delete(negativeId);
            Action deleteWithZeroId = () => _projectService.Delete(zeroId);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(deleteWithNegativeId);
            Assert.Throws<ArgumentOutOfRangeException>(deleteWithZeroId);
        }

        [Fact]
        public void Delete_WhenProjectIsNotFound_ThrowsArgumentNullException()
        {
            //Arrange
            List<Project> projects = new List<Project>() { new Project { Id = 1, Name = "Test" } };
            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //Act 
            Action action = () => { _projectService.Delete(2); };

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}

