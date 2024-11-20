using Microsoft.Win32.SafeHandles;
using Project_managment;
using Task = Project_managment.Task;

public static class Program
{
    public static Dictionary<Project, List<Task>> projects = new Dictionary<Project, List<Task>>()
    {
        {new Project("Upravljanje financija", "Drugi domaći rad za dump", new DateTime(2024-11-8)), new List<Task>{new Task("Bug-fixing","Popravi greške u programu", new DateTime(2024-11-15))} },
        {new Project("Upravljanje financija_", "Drugi domaći rad za dump", new DateTime(2024-11-8)), new List<Task>{new Task("Bug-fixing_","Popravi greške u programu", new DateTime(2024-11-15))} }
    };
    public static void Main(string[] args)
    {
        Console.WriteLine("Upišite broj operacije koje želite obaviti:\n1 - Ispis projekta\n2 - Dodavanje projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u sljedećih 7 dana\n5 - Prikaz projekta po statusu\n6 - Upravljanje projektima\n7 - Upravljanje zadatcima\n8 - Izlaz iz aplikacije");
        int.TryParse(Console.ReadLine(), out var pickedOperation);
        switch(pickedOperation)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Ovo su vaši projekti i zadatci:");
                foreach(var project in projects)
                {
                    Console.WriteLine(project.Key.projectName + ":");
                    WriteTasks(project.Value);
                }
                break;
        }
    }
    public static void WriteTasks(List<Task> tasks)
    {
        foreach (var task in tasks) Console.WriteLine(task.taskName);
    }
}