using Microsoft.Win32.SafeHandles;
using Project_managment;
using System.Threading.Tasks;
using Task = Project_managment.Task;

public static class Program
{
    public static Dictionary<Project, List<Task>> projects = new Dictionary<Project, List<Task>>()
    {
        {new Project("Upravljanje financija", "Drugi domaći rad za dump", new DateTime(2024-11-8)), new List<Task>{new Task("Bug-fixing","Popravi greške u programu", new DateTime(2024-11-15)), new Task("Objava", "Objavi projekt", new DateTime(2024/11/16))} },
    };
    public static void Main(string[] args)
    {
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
                        foreach (var task in project.Value) Console.WriteLine(task.taskName);
                    }
                    leaveOperation();
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
                        if (newProjectName == "")
                        {
                            Console.WriteLine("Ime projekta nije validno. pokušajte ponovo");
                        }
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
                    string projectApproval;
                    do
                    {
                        projectApproval = Console.ReadLine();
                        if (projectApproval == "YES")
                        {
                            projects.Add(new Project(newProjectName, newProjectDescription, DateTime.Now.Date), new List<Task> { });
                            Console.WriteLine("Projekt uspješno dodan.");
                        }
                        else if (projectApproval == "NO")
                        {
                            Console.WriteLine("Dodavanje projekta otkazano");
                        }
                        else Console.WriteLine("Odgovor nije validan. Pokušajte ponovo ");
                    } while (projectApproval != "YES" && projectApproval != "NO");
                    leaveOperation();
                    break;
            }
        }
    }
    public static void leaveOperation()
    {
        Console.WriteLine("Pritisnite ENTER kako biste se vratili na početni izbornik");
        string leaveOperationInput = Console.ReadLine();
    }
}