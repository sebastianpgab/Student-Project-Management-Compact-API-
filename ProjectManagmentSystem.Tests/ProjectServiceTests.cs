using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
        public void Create_WhenProjectIsNull_ShouldThrowsArgumentNullException()
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

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Delete_WhenIdIsNegativeOrZero_ThrowsArgumentOutOfRangeException(int number)
        {
            // Act
            Action deleteWithNegativeId = () => _projectService.Delete(number);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(deleteWithNegativeId);
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

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]

        public void Update_WhenIdIsNegativeOrZero_ThrowssArgumentOutOfRangeException(int number)
        {
            //Arrange
            var project = new Project()
            {
                Name = "New Project Name",
                Description = "New Project Desc"
            };
            var projects = new List<Project> { new Project { Id = 2, Name = "Project Name", Description = "Project Desc"} };
            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //act
            Action action = () => _projectService.Update(project, number);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void Update_WhenProjectDoesNotExist_ThrowsArgumentNullException()
        {
            //Arrange
            Project project = null;
            var id = 1;
            var projects = new List<Project> { new Project { Id = 2, Name = "Project Name", Description = "Project Desc" } };
            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //Act
            Action action = () => _projectService.Update(project, id);

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Update_WhenProjectExists_UpdatesProject()
        {
            //Arrange
            Project project = new Project { Id = 2, Name = "Project Name Updated", Description = "Project Desc Updated" };

            var projects = new List<Project> { new Project { Id = 2, Name = "Project Name", Description = "Project Desc" } };
            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //Act
            _projectService.Update(project, 2);

            //Assert
            var updatedProject = projects.FirstOrDefault(p => p.Id == 2);
            updatedProject.Should().NotBeNull();
            updatedProject.Description.Should().Be("Project Desc Updated");
            updatedProject.Name.Should().Be("Project Name Updated");
        }
    }
}

