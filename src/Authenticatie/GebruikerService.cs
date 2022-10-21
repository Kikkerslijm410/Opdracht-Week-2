namespace Pretpark;
public class GebruikerService {
    public EmailService emailSender = new EmailService();
    public IGebruikerContext gebruikerContext = new GebruikerContext();

    public Gebruiker Registreer (String email, String wachtwoord){
        if (emailSender.Email("Nieuw account", email)){
            gebruikerContext.NieuweGebruiker(email, wachtwoord);
            return gebruikerContext.GetGebruiker(gebruikerContext.AantalGebruikers() - 1);
        }else{
            return new Gebruiker("niet gelukt", "niet gelukt");
        }
    }

    public Boolean Verifieer (String email, String token2){
        foreach (Gebruiker gebruiker in gebruikerContext.AlleGebruikers()){
            if (gebruiker.Token != null && gebruiker.Email.Equals(email)){
                if (gebruiker.Token.token == token2 && gebruiker.Token.VerloopDatum >= DateTime.Today){
                    gebruiker.Token = null;
                    return true;
                }
            }else if (gebruiker.Token == null){
                return true;
            }
        }
        return false;
    }

    public Boolean Login (String email, String wachtwoord){
        foreach (var gebruiker in gebruikerContext.AlleGebruikers()){
            if (gebruiker.Email == email && gebruiker.Wachtwoord == wachtwoord && gebruiker.Geverifieerd()){
                return true;
            }
        }
        return false;
    }
}