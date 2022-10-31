namespace test;

public class MockVerificatieToken : IVerificatieToken{
    public string? token { get; set; }
    public DateTime VerloopDatum { get; set; }

    public MockVerificatieToken(string? token, DateTime verloopDatum){
        this.token = token;
        VerloopDatum = verloopDatum;
    }
}