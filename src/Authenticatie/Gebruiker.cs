namespace Pretpark;
public class Gebruiker : IGebruiker {
    public string Wachtwoord {get; set;}
    public string Email {get; set;}
    public VerificatieToken? Token {get; set;}

    public Boolean Geverifieerd(){
        if (Token == null){
            return true;
        }
        return false;
    }

    public Gebruiker (string email, string wachtwoord){
        Wachtwoord = wachtwoord;
        Email = email;
        Token = this.NewToken();
    }

    public Gebruiker (string email, string wachtwoord, VerificatieToken token){
        Wachtwoord = wachtwoord;
        Email = email;
        Token = token;
    }

    public VerificatieToken NewToken(){
        VerificatieToken token = new VerificatieToken();
        return token;
    }

    // public String GetToken(Gebruiker gebruiker){
    //     return gebruiker.Token.token;
    // }

    public string GetToken(){
        return "token";
    }
}