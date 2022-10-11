namespace Pretpark;
public class Program{
    public static GebruikerContext gebruikerContext = new GebruikerContext();

    static void Main(string[] args){
        Console.WriteLine("Geef uw naam");
        String? gn = Console.ReadLine();
        Console.WriteLine("Geef uw wachtwoord");
        String? ww = Console.ReadLine();
        gebruikerContext.NieuweGebruiker(gn!, ww!);
    }
}