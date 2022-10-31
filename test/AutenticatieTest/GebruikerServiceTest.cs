namespace test;
public class GebruikerServiceTest{

    //Zonder moq tests
    public static MockGebruikerContext MockGebruikerContext = new MockGebruikerContext();
    public static MockEmailService MockEmailService = new MockEmailService(true);
    public GebruikerService GebruikerService = new GebruikerService(MockEmailService, MockGebruikerContext);

    [Theory]
    [InlineData("email", "wachtwoord")]
    [InlineData("niet gelukt", "niet gelukt")]
    //Test of de gebruiker wordt aangemaakt als de email wel verzonden kan worden
    public void RegistreerTest(string expectedEmail, string expectedWachtwoord){
        // Given
        GebruikerService gebruikerService = new GebruikerService(new MockEmailService(true), new MockGebruikerContext());

        // When
        Gebruiker resultaat = GebruikerService.Registreer(expectedEmail, expectedWachtwoord);

        // Then
        Assert.Equal(expectedEmail, resultaat.Email);
        Assert.Equal(expectedWachtwoord, resultaat.Wachtwoord);
    }
    
    [Theory]
    [InlineData("email", "email", "token", "wachtwoord", true)]
    [InlineData("email", "email", null, "wachtwoord", false)]
    [InlineData("email", "email", "token2", "wachtwoord", false)]
    [InlineData("fout", "email", "token2", "wachtwoord", false)]
    [InlineData("email", "fout", "token2", "wachtwoord", false)]
    //Test of de gebruiker wordt geverifieerd
    public void VerifieerTest(String email2, String email, String token, String wachtwoord, Boolean expected){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(false), new MockGebruikerContext());
        GebruikerService.Registreer(email, wachtwoord);

        // When
        Boolean resultaat = GebruikerService.Verifieer(email2, token);

        // Then
        Assert.Equal(expected, resultaat);
    }    

    [Fact]
    public void VerifieerTestVerloopDatum(){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(false), new MockGebruikerContext());
        MockGebruiker Mgebruiker = new MockGebruiker("email", "wachtwoord", new VerificatieToken("token", DateTime.));

        // When
        Boolean resultaat = GebruikerService.Verifieer("email", "token");

        // Then
        Assert.False(resultaat);
    }

     //login moet hier
}