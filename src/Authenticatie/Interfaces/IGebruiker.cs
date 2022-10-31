namespace Pretpark;

public interface IGebruiker{
    string Wachtwoord { get; set; }
    string Email { get; set; }
    VerificatieToken? Token { get; set; }
    Boolean Geverifieerd();
    VerificatieToken NewToken();
    string GetToken(){return "token";}
}
