namespace test;
public class MockGebruikerService : IGebruikerService{
    public IGebruikerContext MockgebruikerContext = new MockGebruikerContext();
    public Gebruiker Registreer(string email, string wachtwoord){
        MockgebruikerContext.NieuweGebruiker(email, wachtwoord);
        return MockgebruikerContext.GetGebruiker(MockgebruikerContext.AantalGebruikers() - 1);    
    }    public bool Verifieer(string email, string token2){
        foreach (Gebruiker gebruiker in MockgebruikerContext.AlleGebruikers()){
            if (gebruiker.Token != null && gebruiker.Email.Equals(email)){
                if (gebruiker.Token.token == token2 && gebruiker.Token.VerloopDatum >= DateTime.Today){
                    gebruiker.Token = null;
                    return true;
                }
            // }else if (gebruiker.Token == null){
            //     return true;
            }
        }
        return false;
    }



    public bool Login(string email, string wachtwoord){
        foreach (var gebruiker in MockgebruikerContext.AlleGebruikers()){
            if (gebruiker.Email == email && gebruiker.Wachtwoord == wachtwoord && gebruiker.Geverifieerd()){
                return true;
            }
        }
        return false;
    }
}