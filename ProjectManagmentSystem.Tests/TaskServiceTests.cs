﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System_do_zarządzania_projektami.Entites;
using System_do_zarządzania_projektami.Services;
using Xunit;
using FluentAssertions;
using System.Xml.Linq;
using System.Runtime.Serialization;

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

        public static IEnumerable<object[]> GetNegativAndZeroData()
        {
            yield return new object[] { -1, 1 };
            yield return new object[] { 1, -1 };
            yield return new object[] { 1, -1 };
            yield return new object[] { 1,  0 };
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

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Update_WhenProjectIdIsNegativeOrZero_ThrowArgumentNullException(int projectId)
        {
            //Arrange
            var taskItem = new TaskItem();

            //Act
            
            Action action = () => _taskService.Create(projectId, taskItem);

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Update_WhenTaskItemIsNull_ThrowArgumentNullException()
        {
            //Arrange
            TaskItem taskItem = null;
            var projects = new List<Project>() {
                new Project 
                { 
                    Id = 1, Description = "Project example", Name = "Project example"
                } 
            };
            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //
            Action action = () => _taskService.Update(1, 1, taskItem);

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Update_WhenTaskItemIsNotNull_UpdateTaskItem()
        {
            // Arrange
            List<TaskItem> tasks = new List<TaskItem>();
            TaskItem taskItemToUpdate = new TaskItem()
            {
                Id = 1,
                ProjectId = 1,
                Status = Status.Cancelled,
                Title = "Title updated"
            };

            TaskItem taskItem = new TaskItem()
            {
                Id = 1,
                ProjectId = 1,
                Status = Status.In_Progress,
                Title = "Title example"
            };
            tasks.Add(taskItem);

            var projects = new List<Project>()
            {
                new Project
                {
                    Id = 1,
                    Description = "Project example",
                    Name = "Project example",
                    Tasks = tasks
                }
            };

            _databaseMock.Setup(p => p.Projects).Returns(projects);

            // Act
            _taskService.Update(1, 1, taskItemToUpdate);

            //Assert
            var updatedTask = tasks.FirstOrDefault(t => t.ProjectId == 1 && t.Id == 1);
            updatedTask.Status.Should().Be(Status.Cancelled);
            updatedTask.Title.Should().Be("Title updated");
            updatedTask.Id.Should().Be(1);
            updatedTask.ProjectId.Should().Be(1);
        }

        [Theory]
        [MemberData(nameof(GetNegativAndZeroData))]
        public void Delete_WhenProjectIdAndTaskIdIsNegativeOrZero_ThrowArgumentNullException(int projectId, int taskId)
        {
            // Act
            Action action = () => _taskService.Delete(projectId, taskId);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Delete_WhenProjectAndTaskIsNotNull_DeletesTaskFromProject()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Example Project",
                Description = "Example Project",
                Tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                ProjectId = 1,
                Status = Status.Completed,
                Title = "Example"
            }
        }
            };
            var projects = new List<Project> { project };

            _databaseMock.Setup(db => db.Projects).Returns(projects);

            // Act
            _taskService.Delete(1, 1);

            // Assert
            var updatedProject = projects.FirstOrDefault(p => p.Id == 1);
            updatedProject.Should().NotBeNull();
            updatedProject.Tasks.Should().BeEmpty();

            _databaseMock.Verify(db => db.Projects, Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetNegativAndZeroData))]
        public void Get_WhenProjectIdOrTaskIdIsNegativeOrZero_ThrowsKeyNotFoundException(int taskId, int projectId)
        {
            //Arrange
            _databaseMock.Setup(p => p.Projects).Returns(new List<Project>());

            //Act
            Action action = () => _taskService.Get(taskId, projectId);

            //Assert
            Assert.Throws<KeyNotFoundException>(action);
        }

        [Fact]
        public void Get_WhenProjectAndTaskExistInDatabase_ReturnTaskItem()
        {
            //Arrange
            var taskItem = new TaskItem
            {
                Id = 1,
                ProjectId = 1,
                Status = Status.Completed
            };
            var taskItems = new List<TaskItem>();
            taskItems.Add(taskItem);

            var project = new Project()
            {
                Id = 1,
                Name = "Name Example",
                Description = "Description Example",
                Tasks = taskItems
            };
            var projects = new List<Project>();
            projects.Add(project);

            _databaseMock.Setup(p => p.Projects).Returns(projects);

            //Act
            var result = _taskService.Get(1, 1);

            //Assert
            Assert.Equal(taskItem, result);
            _databaseMock.Verify(p => p.Projects, Times.Once());
        }
    }
}
