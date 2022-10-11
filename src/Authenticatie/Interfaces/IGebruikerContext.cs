namespace Pretpark;
public interface IGebruikerContext{
    public int AantalGebruikers();
    public Gebruiker GetGebruiker(int i);
    public void NieuweGebruiker (String email, String wachtwoord);
    public List<Gebruiker> AlleGebruikers();
}