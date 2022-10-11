namespace Pretpark;
public interface IGebruiker{
    public VerificatieToken NewToken();
    public Boolean Geverifieerd();
    public String GetToken(Gebruiker gebruiker);
}