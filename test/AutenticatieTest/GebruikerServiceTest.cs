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
    
    [Fact]
    public void VerifieerTest(){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(false), new MockGebruikerContext());

        var expectedEmail = "email";
        var expectedWachtwoord = "wachtwoord";

        // When
        Gebruiker resultaat = GebruikerService.Registreer(expectedEmail, expectedWachtwoord);
        

        // Then
    }    

    [Fact]
    public void VerifieerTest2(){
        // Given


        // When


        // Then
    }  
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    [Fact]
    //Test of de gebruiker kan inloggen met verificatie
    public void LoginTest(){
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
    public void VerifieerTest(){
        // Given
        Gebruiker m = GebruikerService.Registreer("email", "wachtwoord");

        // When
        bool resultaat = GebruikerService.Verifieer("email", "token");

        // Then
        Assert.True(resultaat);
    }
}