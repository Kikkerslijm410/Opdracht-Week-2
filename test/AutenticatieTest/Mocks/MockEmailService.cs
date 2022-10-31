namespace test;

public class MockEmailService : IEmailService{
    private bool email;
    public bool Email (String wachtwoord, String Email){
        return email;
    }

    public MockEmailService(bool expected){
        email = expected;
    }
}