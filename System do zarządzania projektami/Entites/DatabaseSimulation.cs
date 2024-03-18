using System_do_zarządzania_projektami.Services;

namespace System_do_zarządzania_projektami.Entites
{
    public class DatabaseSimulation
    {
        public List<Project> projects = new List<Project> {

            new Project() {
            Id = 1,
            Name = "Projekt Kino - Cinema 4FUN",
            Description = "Projekt kuno, który umozliwia zarządzanie kinem",
            Tasks = new List<TaskItem> {
                new TaskItem()
                {
                    Id = 1,
                    Title = "Dodanie funkcji - Dodaj klienta",
                    Status = Status.Not_Started,
                    ProjectId = 1
                },
                new TaskItem()
                {
                    Id = 2,
                    Title = "Dodanie funkcji - Dodaj seans",
                    Status = Status.Not_Started,
                    ProjectId = 1
                },
                new TaskItem()
                {
                    Id = 3,
                    Title = "Dodanie funkcji - Sprawdź dostępnośc biletów",
                    Status = Status.Not_Started,
                    ProjectId = 1
                }
                }
            },

            new Project()
            {
                Id = 2,
                Name = "Sklep internetowy - Bażant",
                Description = "Sklep internetowy Bażant dla miłośników łowictwa i myślistwa",
                Tasks =  new List<TaskItem> {
                    new TaskItem
                    {
                        Id = 1,
                        Title = "Dodanie funckji - dodaj do koszyka",
                        Status = Status.In_Progress,
                    },
                    new TaskItem
                    {
                        Id = 2,
                        Title = "Dodanie funckji - usuń z koszyka",
                        Status = Status.Cancelled,
                    },
                    new TaskItem
                    {
                        Id = 3,
                        Title = "Usunięcie widoku - promocje Śwąteczne",
                        Status = Status.Cancelled,
                    }
                }

            }

        };
    }
}
