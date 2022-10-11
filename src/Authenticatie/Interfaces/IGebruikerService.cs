namespace Pretpark;
public interface IGebruikerService{
    public Gebruiker Registreer (String email, String wachtwoord);
    public Boolean Verifieer (String email, String token);
    public Boolean Login (String email, String wachtwoord);
}