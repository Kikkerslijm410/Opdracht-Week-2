namespace Pretpark;

public interface IVerificatieToken{
    String? token {get; set;}
    DateTime VerloopDatum {get; set;}
}