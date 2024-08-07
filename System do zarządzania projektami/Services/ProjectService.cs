﻿using System_do_zarządzania_projektami.Entites;

namespace System_do_zarządzania_projektami.Services
{
    public enum Status
    {
        Not_Started,
        In_Progress,
        On_Hold,
        Cancelled,
        Completed
    }
    public interface IProjectService
    {
        public Project Create(Project project);
        public void Delete(int id);
        public void Update(Project project, int id);
        public Project Get(int id);
    }
    public class ProjectService : IProjectService
    {
        private readonly IDatabaseSimulation _databaseSimulation;
        private readonly ITaskService _taskService;
        public ProjectService(IDatabaseSimulation databaseSimulation, ITaskService taskService)
        {
            _databaseSimulation = databaseSimulation;

            _taskService = taskService;

        }

        public Project Create(Project project)
        {
            if(project != null)
            {
                _databaseSimulation.Projects.Add(project);
                Console.WriteLine("Poprawnie dodano projekt " +  project.Name);
                return project;
            }
            else
            {
                Console.WriteLine("Nie udało się dodać projektu");
                throw new ArgumentNullException(nameof(project));
            }
        }

        public void Delete(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id musi być większe niż zero");
            }

            var project = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == id);

            if (project != null)
            {
                _databaseSimulation.Projects.Remove(project);
                Console.WriteLine("Poprawnie usunięto projekt" + project.Name);
            }
            else
            {
                throw new ArgumentNullException(nameof(project), "Nie znaleziono projektu o podanym id.");
            }

        }

        public void Update(Project project, int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id musi być większe niż zero");
            }

            var result = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == id);
            if (result != null)
            {
                result.Name = result.Name is not null ? project.Name : result.Name;
                result.Description = result.Description is not null ? project.Description : result.Description;
                Console.WriteLine("Poprawnie edytowano projekt" + result.Name);
            }
            else
            {
                throw new ArgumentNullException(nameof(project), "Nie znaleziono projektu o podanym id.");
            }
        }

        public Project Get(int id)
        {
            if (id > 0)
            {
                var project = _databaseSimulation.Projects.FirstOrDefault(p => p.Id == id);
                return project;
            }
            Console.WriteLine("Nie znaleziono projektu");
            return null;
        }

    }
}
