namespace test;
public class MockGebruikerContext : IGebruikerContext{
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

    public void NieuweGebruikerZonderVerifactie(){
        Gebruiker dummy = new Gebruiker("email", "wachtwoord");
        Gebruikers.Add(dummy);
    }
}