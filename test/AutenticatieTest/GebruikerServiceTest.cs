namespace test;
public class GebruikerServiceTest{

    //Zonder moq tests
    public static MockGebruikerContext MockGebruikerContext = new MockGebruikerContext();
    public static MockEmailService MockEmailService = new MockEmailService(true);
    public GebruikerService GebruikerService = new GebruikerService(MockEmailService, MockGebruikerContext);

    [Fact]
    //Test of de gebruiker wordt aangemaakt als de email wel verzonden kan worden
    public void RegistreerTest(){
        // Given
        GebruikerService gebruikerService = new GebruikerService(new MockEmailService(true), new MockGebruikerContext());

        var expectedEmail = "email";
        var expectedWachtwoord = "wachtwoord";

        // When
        Gebruiker resultaat = GebruikerService.Registreer(expectedEmail, expectedWachtwoord);

        // Then
        Assert.Equal(expectedEmail, resultaat.Email);
        Assert.Equal(expectedWachtwoord, resultaat.Wachtwoord);
    }

    [Fact]
    //Test of de gebruiker wordt aangemaakt als de email niet verzonden kan worden
    public void RegistreerTest2(){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(false), new MockGebruikerContext());

        var expectedEmail = "email";
        var expectedWachtwoord = "wachtwoord";

        // When
        Gebruiker resultaat = GebruikerService.Registreer(expectedEmail, expectedWachtwoord);

        // Then
        Assert.Equal("niet gelukt", resultaat.Email);
        Assert.Equal("niet gelukt", resultaat.Wachtwoord);
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
        Gebruiker gebruiker = new Gebruiker("email", "wachtwoord");
        gebruiker.Token.VerloopDatum = gebruiker.Token.VerloopDatum.AddDays(-1);

        // When
        Boolean resultaat = GebruikerService.Verifieer("email", "token");

        // Then
        Assert.False(resultaat);
    } 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    [Fact]
    //Test of de gebruiker kan inloggen met verificatie
    public void LoginTest7(){
        // Given
        Gebruiker m = GebruikerService.Registreer("email", "wachtwoord");
        GebruikerService.Verifieer("email", "token");

        // When
        bool resultaat = GebruikerService.Login("email", "wachtwoord");

        // Then
        Assert.True(resultaat);
    }

    [Fact]
    //Test of de gebruiker juist wordt geregistreerd
    public void VerifieerTest5(){
        // Given
        Gebruiker m = GebruikerService.Registreer("email", "wachtwoord");

        // When
        bool resultaat = GebruikerService.Verifieer("email", "token");

        // Then
        Assert.True(resultaat);
    }
}