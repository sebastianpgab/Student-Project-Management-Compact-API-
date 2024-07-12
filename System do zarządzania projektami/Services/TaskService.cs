using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System_do_zarządzania_projektami.Entites;

namespace System_do_zarządzania_projektami.Services
{
    public interface ITaskService
    {
        TaskItem Create(int projectId, TaskItem task);
        TaskItem Get(int projectId, int taskId);
        void Update(int projectId, int taskId, TaskItem task);
        void Delete(int projectId, int taskId);

    }
    public class TaskService : ITaskService
    {
        private readonly IDatabaseSimulation _databaseSimulation;

        public TaskService(IDatabaseSimulation databaseSimulation)
        {
            _databaseSimulation = databaseSimulation;
        }

        public TaskItem Create(int projectId, TaskItem task)
        {
            var project = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project != null)
            {
                if (project.Tasks == null)
                {
                    project.Tasks = new List<TaskItem>();
                }
                // Zakładamy, że ID zadań są unikatowe tylko w ramach projektu.
                task.ProjectId = projectId;
                task.Id = project.Tasks.Any() ? project.Tasks.Max(t => t.Id) + 1 : 1;
                project.Tasks.Add(task);
                Console.WriteLine($"Task '{task.Title}' added to project '{project.Name}'.");
                return task;
            }
            else
            {
                Console.WriteLine("Nie znalezioni projektu");
                 throw new ArgumentNullException();
            }
        }

        public TaskItem Get(int projectId, int taskId)
        {
            var project = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                var task = project.Tasks?.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    return task;
                }
            }
            Console.WriteLine("Nie znaleziono zadania lub projektu");
            return null;
        }


        public void Update(int projectId, int taskId, TaskItem taskUpdate)
        {
            var project = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                var task = project.Tasks?.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.Title = taskUpdate.Title ?? task.Title;
                    task.Status = taskUpdate.Status;
                    Console.WriteLine($"Task '{task.Title}' updated in project '{project.Name}'.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono zadania");
                    throw new ArgumentNullException(nameof(task), "Task not found");
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono projektu");
                throw new ArgumentNullException(nameof(project), "Project not found");
            }
        }

        public void Delete(int projectId, int taskId)
        {
            if(projectId <= 0 || taskId <= 0)
            {
                throw new ArgumentNullException("ProjectId or TaskId is zero or negativ");
            }
            var project = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                var task = project.Tasks?.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    project.Tasks.Remove(task);
                    Console.WriteLine($"Task '{task.Title}' deleted from project '{project.Name}'.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono zadania");
                    throw new ArgumentNullException(nameof(task), "Task not found");
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono projektu");
                throw new ArgumentNullException(nameof(project), "Project not found");
            }
        }

    }
}
