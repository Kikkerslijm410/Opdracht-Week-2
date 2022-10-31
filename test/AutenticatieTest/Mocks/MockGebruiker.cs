namespace test;

public class MockGebruiker : IGebruiker{
    public string Wachtwoord { get; set; }
    public string Email { get; set; }
    public VerificatieToken? Token { get; set; }

    public Boolean Geverifieerd(){
        if (Token == null){
            return true;
        }
        return false;
    }

    public MockGebruiker(string email, string wachtwoord){
        Wachtwoord = wachtwoord;
        Email = email;
        Token = this.NewToken();
    }

    public VerificatieToken NewToken(){
        VerificatieToken token = new VerificatieToken();
        return token;
    }

    public String GetToken(){
        return Token.token;
    }
}