namespace Pretpark;
public class Program{
    public static GebruikerContext gebruikerContext = new GebruikerContext();
    public static EmailService emailService = new EmailService();

    static void Main(string[] args){
        bool x = true;
        while (x){
            Console.WriteLine("Welkom bij het pretpark");
            Console.WriteLine("1. Registreren");
            Console.WriteLine("2. Inloggen");
            Console.WriteLine("3. Verlaten");
            Console.Write("Maak een keuze: ");
            string? keuze = Console.ReadLine();
            switch (keuze){
                case "1":
                    Console.Write("Voer uw email in: ");
                    string email = Console.ReadLine();
                    Console.Write("Voer uw wachtwoord in: ");
                    string wachtwoord = Console.ReadLine();
                    GebruikerService service = new GebruikerService(emailService, gebruikerContext);
                    Gebruiker gebruiker = service.Registreer(email, wachtwoord);
                    gebruikerContext.Gebruikers.Add(gebruiker);
                    break;
                case "2":
                    Console.Write("Voer uw email in: ");
                    string email2 = Console.ReadLine();
                    Console.Write("Voer uw wachtwoord in: ");
                    string wachtwoord2 = Console.ReadLine();
                    GebruikerService service2 = new GebruikerService (emailService, gebruikerContext);
                    bool poging = service2.Login(email2, wachtwoord2);
                    if (poging){
                        Console.WriteLine("U bent ingelogd");
                    }
                    else{
                        Console.WriteLine("Uw email of wachtwoord is onjuist");
                    }
                    break;
                case "3":
                    x = false;
                    break;
                default:
                    Console.WriteLine("U heeft een ongeldige keuze gemaakt");
                    break;
            }
        }
    }
}