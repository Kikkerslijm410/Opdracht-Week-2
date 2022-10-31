namespace Pretpark;
public class VerificatieToken : IVerificatieToken{
    public String? token {get; set;}
    public DateTime VerloopDatum {get; set;}

    public VerificatieToken(){
        token = "token";
        VerloopDatum = DateTime.Today.AddDays(3);
    }
}