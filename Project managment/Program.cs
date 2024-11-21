using Microsoft.Win32.SafeHandles;
using Project_managment;
using System.Threading.Tasks;
using Task = Project_managment.Task;

public static class Program
{
    public static Dictionary<Project, List<Task>> projects = new Dictionary<Project, List<Task>>()
    {
        {new Project("Upravljanje financija", "Drugi domaći rad za dump", new DateTime(2024,11,15)), new List<Task>{new Task("Bug-fixing","Popravi greške u programu", new DateTime(2024,11,27)), new Task("Objava", "Objavi projekt", new DateTime(2025,12,1))} },
    };
    public static void Main(string[] args)
    {
        bool actionIsConfirmed=false;
        int pickedOperation=0;
        while (pickedOperation != 8)
        {
            Console.Clear();
            Console.WriteLine("Upišite broj operacije koje želite obaviti:\n1 - Ispis projekta\n2 - Dodavanje projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u sljedećih 7 dana\n5 - Prikaz projekta po statusu\n6 - Upravljanje projektima\n7 - Upravljanje zadatcima\n8 - Izlaz iz aplikacije");
            int.TryParse(Console.ReadLine(), out pickedOperation);
            switch (pickedOperation)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Ovo su vaši projekti i zadatci:");
                    foreach (var project in projects)
                    {
                        Console.WriteLine($"{project.Key.projectName} ({project.Key.projectStatus}):");
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
                    Console.WriteLine($"Želite li dodati ovaj projekt?");
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
                    string projectStatusToView;
                    do
                    {
                        projectStatusToView = Console.ReadLine();
                        if (projectStatusToView != "Active" && projectStatusToView != "Standby" && projectStatusToView != "Finished")
                        {
                            Console.WriteLine("Unos nije validan. Pokušajte ponovo");
                        }
                    } while (projectStatusToView != "Active" && projectStatusToView != "Standby" && projectStatusToView != "Finished");
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
            }
        }
    }
    public static void LeaveOperation()
    {
        Console.WriteLine("");
        Console.WriteLine("Pritisnite ENTER kako biste se vratili na početni izbornik");
        string leaveOperationInput = Console.ReadLine();
    }
    public static bool IsActionConfirmed()
    {
        string affirmationCheck;
        bool isActionConfirmed = false;
        do
        {
            affirmationCheck = Console.ReadLine();
            if (affirmationCheck == "YES") isActionConfirmed = true;
            else if (affirmationCheck == "NO") isActionConfirmed = false;
            else Console.Write("Odgovor nije validan. Pokušajte ponovo");
        } while (affirmationCheck != "YES" && affirmationCheck != "NO");
        return isActionConfirmed;
    }
}