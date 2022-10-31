namespace Pretpark;
public class VerificatieToken{
    public String? token {get; set;}
    public DateTime VerloopDatum {get; set;}

    public VerificatieToken(){
        token = "token";
        VerloopDatum = DateTime.Today.AddDays(3);
    }

    public VerificatieToken(string x, DateTime y){
        token = x;
        VerloopDatum = y;
    }
}