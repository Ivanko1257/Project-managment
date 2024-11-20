using Project_managment;
using Task = Project_managment.Task;

public static class Program
{
    public static Dictionary<Project, List<Task>> projects = new Dictionary<Project, List<Task>>();
    public static void Main(string[] args)
    {
        Console.WriteLine("Upišite broj operacije koje želite obaviti:\n1 - Ispis projekta\n2 - Dodavanje projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u sljedećih 7 dana\n5 - Prikaz projekta po statusu\n6 - Upravljanje projektima\n7 - Upravljanje zadatcima");
    }
}