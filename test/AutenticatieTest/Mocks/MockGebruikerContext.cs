namespace test;
public class MockGebruikerContext : IGebruikerContext{
    //this new gebruiker can suck deez nuts
    public List<Gebruiker> Gebruikers = new List<Gebruiker>() {new Gebruiker("email", "wachtwoord")};

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