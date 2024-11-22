using Microsoft.Win32.SafeHandles;
using Project_managment;
using System.Threading.Tasks;
using Task = Project_managment.Task;
using TaskStatus = Project_managment.TaskStatus;

public static class Program
{
    public static Dictionary<Project, List<Task>> projects = new Dictionary<Project, List<Task>>()
    {
        {new Project("Upravljanje financija", "Drugi domaći rad za dump", new DateTime(2024,11,15)), new List<Task>{new Task("Bug-fixing","Popravi greške u programu", new DateTime(2024,11,27), new TimeSpan(0, 15, 0)), new Task("Objava", "Objavi projekt", new DateTime(2025,12,1), new TimeSpan(0,10,0))} },
    };
    public static void Main(string[] args)
    {
        bool actionIsConfirmed=false;
        int pickedOperation=0;
        while (pickedOperation != 8)
        {
            Console.Clear();
            Console.WriteLine("Upišite broj operacije koje želite obaviti:\n1 - Ispis projekta\n2 - Dodavanje projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u sljedećih 7 dana\n5 - Prikaz projekta po statusu\n6 - Upravljanje projektima\n7 - Upravljanje zadatcima\n8 - Izlaz iz aplikacije");
            do
            {
                int.TryParse(Console.ReadLine(), out pickedOperation);
                if(pickedOperation != 1 && pickedOperation != 2 && pickedOperation != 3 && pickedOperation != 4 && pickedOperation != 5 && pickedOperation != 6 && pickedOperation != 7 && pickedOperation != 8)
                {
                    Console.WriteLine("Unos nije validan. Pokušajte ponovo");
                }
            }while (pickedOperation != 1 && pickedOperation != 2 && pickedOperation != 3 && pickedOperation != 4 && pickedOperation != 5 && pickedOperation != 6 && pickedOperation != 7 && pickedOperation != 8);
            switch (pickedOperation)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Ovo su vaši projekti i zadatci:");
                    foreach (var project in projects)
                    {
                        Console.WriteLine($"\n{project.Key.projectName} ({project.Key.projectStatus}):\nSvi zadatci za ovaj projekt:");
                        foreach (var task in project.Value) Console.WriteLine($"- {task.taskName} ({task.taskStatus})");
                    }
                    LeaveOperation();
                    break;
                case 2:
                    Console.Clear();
                    string newProjectName;
                    bool projectNameIsRepeating;
                    Console.Write("Upišite ime projekta: ");
                    do
                    {
                        newProjectName = Console.ReadLine();
                        projectNameIsRepeating = false;
                        if (newProjectName == "") Console.WriteLine("Ime projekta nije validno. pokušajte ponovo");
                        foreach (var project in projects)
                        {
                            if (newProjectName == project.Key.projectName)
                            {
                                Console.WriteLine("Ime projekta već postoji. Pokušajte ponovo");
                                projectNameIsRepeating = true;
                            }
                        }
                    } while (newProjectName == "" || projectNameIsRepeating);
                    Console.Write("Upišite opis projekta: ");
                    string newProjectDescription = Console.ReadLine();
                    Console.WriteLine($"Želite li dodati ovaj projekt?(Da,Ne): ");
                    actionIsConfirmed = IsActionConfirmed();
                    if (!actionIsConfirmed) Console.WriteLine("Dodavanje je uspiješno otkazano");
                    else
                    {
                        projects.Add(new Project(newProjectName, newProjectDescription, DateTime.Now.Date), new List<Task>());
                        Console.WriteLine("Projekt uspiješno dodan");
                    }
                    LeaveOperation();
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Upišite ime projekta kojeg želite izbrisati: ");
                    bool nameIsFound = false;
                    while(!nameIsFound)
                    {
                        var nameToDelete = Console.ReadLine();
                        foreach (var project in projects)
                        {
                            if (project.Key.projectName == nameToDelete)
                            {
                                Console.WriteLine("Želite li izbrisati ovaj projekt?(Da, Ne): ");
                                actionIsConfirmed = IsActionConfirmed();
                                if (!actionIsConfirmed) Console.WriteLine("Brisanje uspiješno otkazano");
                                else
                                {
                                    projects.Remove(project.Key);
                                    Console.WriteLine("Brisanje uspiješno odrađeno");
                                }
                                nameIsFound = true;
                                break;
                            }
                        }
                        if (!nameIsFound) Console.WriteLine("Projekt kojeg tražite ne postoji. Pokušajte ponovo");
                    }
                    LeaveOperation();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Zadatci kojima je rok za 7 dana i manje:");
                    foreach(var project in projects)
                    {
                        foreach(var task in project.Value)
                        {
                            if ((task.taskDeadlineDate - DateTime.Now.Date).TotalDays <= 7)
                            {
                                Console.WriteLine($"{task.taskName} iz projekta {project.Key.projectName}");
                            }
                        }
                    }
                    LeaveOperation();
                    break;
                case 5:
                    Console.Clear();
                    Console.Write("Upišite status projekata kojih želite vidjeti(Active, Standby, Finished): ");
                    string projectStatusToView = StatusValidation();
                    Console.WriteLine($"Svi projekti sa statusom {projectStatusToView}:");
                    foreach(var project in projects)
                    {
                        if (project.Key.projectStatus.ToString() ==  projectStatusToView)
                        {
                            Console.WriteLine(project.Key.projectName);
                        }
                    }
                    LeaveOperation();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Upišite ime projekta kojeg želite uređivati: ");
                    Project projectToEdit = FindProjectByName();
                    Console.WriteLine("1 - Ispis svih zadataka\n2 - Prikaz detalja\n3 - Uređivanje statusa\n4 - Dodavanje zadataka\n5 - Brisanje zadataka\n6 - Očekivano vrijeme za sve zadatke\n7 - Izlaz iz izbornika");
                    int pickedOperationForProject = 0;
                    do
                    {
                        int.TryParse(Console.ReadLine(), out pickedOperationForProject);
                        if (pickedOperationForProject != 1 && pickedOperationForProject != 2 && pickedOperationForProject != 3 && pickedOperationForProject != 4 && pickedOperationForProject != 5 && pickedOperationForProject != 6 && pickedOperationForProject != 7)
                        {
                            Console.WriteLine("Unos nije validan. Pokušajte ponovo");
                        }
                    } while (pickedOperationForProject != 1 && pickedOperationForProject != 2 && pickedOperationForProject != 3 && pickedOperationForProject != 4 && pickedOperationForProject != 5 && pickedOperationForProject != 6 && pickedOperationForProject != 7);
                    switch (pickedOperationForProject)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Zadatci u ovom projektu:");
                            foreach (var task in projects[projectToEdit]) Console.WriteLine($"- {task.taskName} ({task.taskStatus})");
                            LeaveOperation();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine($"- Opis: {projectToEdit.projectDescription}\n- Početak projekta: {projectToEdit.projectStartDate} \n- Kraj projekta: {projectToEdit.projectEndDate}\n- Status projekta{projectToEdit.projectStatus}");
                            LeaveOperation();
                            break;
                        case 3:
                            if(projectToEdit.projectStatus.ToString()=="Finished")
                            {
                                Console.WriteLine("Projekt koji želite promijeniti je završen te se ne može uređivati");
                                LeaveOperation();
                                break;
                            }
                            Console.Clear();
                            Console.Write($"Trenutni status projekta je {projectToEdit.projectStatus}. Unesite novi status projekta (Active, Standby, Finished): ");
                            string newStatusForProject = StatusValidation();
                            if (newStatusForProject == "Active") projectToEdit.projectStatus = ProjectStatus.Active;
                            else if(newStatusForProject == "Standby") projectToEdit.projectStatus = ProjectStatus.Standby;
                            else projectToEdit.projectStatus = ProjectStatus.Finished;
                            Console.WriteLine("Promjena statusa je uspješna.");
                            LeaveOperation();
                            break;
                        case 4:
                            if (projectToEdit.projectStatus.ToString() == "Finished")
                            {
                                Console.WriteLine("Projekt koji želite promijeniti je završen te se ne može uređivati");
                                LeaveOperation();
                                break;
                            }
                            Console.Clear();
                            Console.Write("Upišite ime novog zadatka: ");
                            string newTaskName;
                            bool taskNameIsRepeating;
                            do
                            {
                                taskNameIsRepeating = false;
                                newTaskName = Console.ReadLine();
                                if (newTaskName == "") Console.WriteLine("Unešeno ime nije validno. Pokušajte ponovo");
                                foreach (var task in projects[projectToEdit])
                                {
                                    if (newTaskName == task.taskName)
                                    {
                                        Console.WriteLine("Ime zadatka već postoji. Pokušajte ponovo");
                                        taskNameIsRepeating = true;
                                    }
                                }
                            }while (taskNameIsRepeating || newTaskName=="");
                            Console.Write("Upišite opis zadatka: ");
                            string newTaskDescription = Console.ReadLine();
                            Console.Write("Upišite rok za izvršavanje zadatka: ");
                            DateTime newTaskDeadline;
                            do
                            {
                                DateTime.TryParse(Console.ReadLine(), out newTaskDeadline);
                                if(newTaskDeadline.ToString() == "1/1/0001" || (newTaskDeadline - DateTime.Now.Date).TotalDays <= 0)
                                {
                                    Console.WriteLine("Unešeni datum nije validan. Pokušajte ponovo");
                                }
                            } while (newTaskDeadline.ToString() == "1/1/0001" || (newTaskDeadline - DateTime.Now.Date).TotalDays <= 0);
                            Console.WriteLine();
                            Console.WriteLine("Upišite očekivano vrijeme za završavanje ovog zadatka (min): ");
                            int expectedTimeInMinutes = 0;
                            do
                            {
                                int.TryParse(Console.ReadLine(), out expectedTimeInMinutes);
                                if(expectedTimeInMinutes == 0)
                                {
                                    Console.WriteLine("Unos nije validan. Pokušajte ponovo");
                                }
                            } while (expectedTimeInMinutes == 0);
                            TimeSpan newTaskExpectedTime = new TimeSpan(0, expectedTimeInMinutes, 0);
                            Console.Write("Želite li dodati ovaj zadatak?(Da,Ne): ");
                            actionIsConfirmed = IsActionConfirmed();
                            if(!actionIsConfirmed)
                            {
                                Console.WriteLine("Dodavanje zadatka uspiješno otkazano");
                                LeaveOperation();
                            }
                            else
                            {
                                projects[projectToEdit].Add(new Task(newTaskName, newTaskDescription, newTaskDeadline, newTaskExpectedTime));
                                Console.WriteLine("Dodavanje zadatka uspiješno odrađeno");
                                LeaveOperation();
                            }
                            break;
                        case 5:
                            if (projectToEdit.projectStatus.ToString() == "Finished")
                            {
                                Console.WriteLine("Projekt koji želite promijeniti je završen te se ne može uređivati");
                                LeaveOperation();
                                break;
                            }
                            Console.Clear();
                            Console.Write("Upišite ime zadatka kojeg želite izbrisati: ");
                            string taskNameToDelete;
                            bool taskNameFound = false;
                            Task taskToDelete = null;
                            do
                            {
                                taskNameToDelete = Console.ReadLine();
                                foreach(var task in projects[projectToEdit])
                                {
                                    if (task.taskName == taskNameToDelete)
                                    {
                                        taskNameFound = true;
                                        taskToDelete = task;  
                                    }
                                    else Console.Write("Ime zadatka nije pronađeno. Pokušajte ponovo");
                                }
                            } while (!taskNameFound);
                            Console.Write("Želite li izbrisati ovaj zadatak?(Da, Ne): ");
                            actionIsConfirmed = IsActionConfirmed();
                            if (!actionIsConfirmed)
                            {
                                Console.WriteLine("Brisanje uspiješno otkazano");
                            }
                            else
                            {
                                projects[projectToEdit].Remove(taskToDelete);
                                Console.WriteLine("Brisanje uspiješno provedeno");
                            }
                            LeaveOperation();
                            break;
                        case 6:
                            TimeSpan sumOfExpectedTaskTimes = new TimeSpan(0,0,0);
                            foreach(var task in projects[projectToEdit])
                            {
                                if(task.taskStatus != TaskStatus.Finished)
                                {
                                    sumOfExpectedTaskTimes += task.taskExpectedTime;
                                }
                            }
                            if (sumOfExpectedTaskTimes.Minutes == 0) Console.WriteLine("Nemate ne završenih zadataka");
                            else
                            {
                                Console.WriteLine($"Očekivano vrijeme za riješavanje svih zadataka je {sumOfExpectedTaskTimes.Minutes} minuta");
                            }
                            LeaveOperation();
                            break;
                        case 7:
                            break;
                    }
                    break;
            }
        }
    }
    public static void LeaveOperation()
    {
        Console.WriteLine("\nPritisnite ENTER kako biste se vratili na početni izbornik");
        string leaveOperationInput = Console.ReadLine();
    }
    public static bool IsActionConfirmed()
    {
        string affirmationCheck;
        bool isActionConfirmed = false;
        do
        {
            affirmationCheck = Console.ReadLine().ToLower();
            if (affirmationCheck == "da") isActionConfirmed = true;
            else if (affirmationCheck == "ne") isActionConfirmed = false;
            else Console.Write("Odgovor nije validan. Pokušajte ponovo");
        } while (affirmationCheck != "da" && affirmationCheck != "ne");
        return isActionConfirmed;
    }
    public static Project FindProjectByName()
    {
        bool projectNameFound = false;
        Project foundProject = null;
        do
        {
            string nameToFind = Console.ReadLine();
            foreach (var project in projects)
            {
                if (project.Key.projectName == nameToFind)
                {
                    projectNameFound = true;
                    foundProject = project.Key;
                }
                else Console.WriteLine("Projekt ne postoji. Pokušajte ponovo");
            }
        } while (!projectNameFound);
        return foundProject;
    }
    public static string StatusValidation()
    {
        string statusToView = "";
        do
        {
            statusToView = Console.ReadLine();
            if (statusToView != "Active" && statusToView != "Standby" && statusToView != "Finished") Console.WriteLine("Unos nije validan. Pokušajte ponovo");
        } while (statusToView != "Active" && statusToView != "Standby" && statusToView != "Finished");
        return statusToView;
    }
}