/*
    Alle mock klassen voor de testen,
    alleen worden ze niet gebruikt, en
    ik was te lui om ze in een aparte
    files te zetten.
*/

public class TestGebruikerContext : IGebruikerContext{
    public List<Gebruiker> Gebruikers = new List<Gebruiker>();

    public int AantalGebruikers(){
        return Gebruikers.Count;
    }
    public Gebruiker GetGebruiker(int i){
        return Gebruikers[i];
    }
    public void NieuweGebruiker (String email, String wachtwoord){
        Gebruikers.Add(new Gebruiker (email, wachtwoord));
    }
    public List<Gebruiker> AlleGebruikers(){
        return Gebruikers;
    }
}

public class TestEmailService : IEmailService{
    public bool Email (String wachtwoord, String email){
        return true;
    }    
}

public class TestGebruiker : IGebruiker{
    public string Wachtwoord {get; set;}
    public string Email {get; set;}
    public VerificatieToken? Token;

    public TestGebruiker (string email, string wachtwoord){
        Wachtwoord = wachtwoord;
        Email = email;
        this.NewToken();
    }
    public Boolean Geverifieerd(){
        if (Token == null){
            return true;
        }
        return false;
    }
    public VerificatieToken NewToken(){
        VerificatieToken token = new VerificatieToken();
        return token;
    }
    public String GetToken(Gebruiker gebruiker){
        return gebruiker.Token.token;
    }
}

public class TestGebruikerService : IGebruikerService{
    public IEmailService emailSender;
    public IGebruikerContext gebruikerContext;

    public TestGebruikerService(IEmailService emailSender, IGebruikerContext context){
        this.emailSender = emailSender;
        this.gebruikerContext = context;
    }
    public bool Login(string email, string wachtwoord){
        foreach (var gebruiker in gebruikerContext.AlleGebruikers()){
            if (gebruiker.Email == email && gebruiker.Wachtwoord == wachtwoord && gebruiker.Geverifieerd()){
                return true;
            }
        }
        return false;
    }
    public Gebruiker Registreer(string email, string wachtwoord){
        if (emailSender.Email("Nieuw account", email)){
            gebruikerContext!.NieuweGebruiker(email, wachtwoord);
            return gebruikerContext.GetGebruiker(gebruikerContext.AantalGebruikers() - 1);
        }else{
            return new Gebruiker("niet gelukt", "niet gelukt");
        } 
    }
    public bool Verifieer(string email, string token){
        foreach (Gebruiker gebruiker in gebruikerContext.AlleGebruikers()){
            if (gebruiker.Token != null && gebruiker.Email.Equals(email)){
                if (gebruiker.Token.token == token && gebruiker.Token.VerloopDatum >= DateTime.Today){
                    gebruiker.Token = null;
                    return true;
                }
            } 
        }
        return false;       
    }
}