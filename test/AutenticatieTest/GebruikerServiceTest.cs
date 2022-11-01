namespace test;
public class GebruikerServiceTest{

    //Zonder moq tests
    public static MockGebruikerContext MockGebruikerContext = new MockGebruikerContext();

    [Theory]
    [InlineData("email", "wachtwoord", "email", "wachtwoord", true)]
    [InlineData("niet gelukt", "niet gelukt", "email", "wachtwoord", false)]
    //Test of de gebruiker wordt aangemaakt als de email wel verzonden kan worden
    public void RegistreerTest(string expectedEmail, string expectedWachtwoord, string actualEmail, string actualWachtwoord, bool emailVerzonden){
        // Given
        GebruikerService gebruikerService = new GebruikerService(new MockEmailService(emailVerzonden), new MockGebruikerContext());

        // When
        Gebruiker resultaat = gebruikerService.Registreer(actualEmail, actualWachtwoord);

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
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(true), new MockGebruikerContext());
        GebruikerService.Registreer(email, wachtwoord);

        // When
        Boolean resultaat = GebruikerService.Verifieer(email2, token);

        // Then
        Assert.Equal(expected, resultaat);
    }    

    //[Fact]
    //Test of de gebruiker kan verifiëren met een verlopen token
    public void VerifieerTestVerloopDatum(){
        // Given
        MockGebruikerContext mockGebruikerContext = new MockGebruikerContext();
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(true), mockGebruikerContext);
        Gebruiker gebruiker = new Gebruiker("email", "wachtwoord", new VerificatieToken("token", DateTime.Today.AddDays(-1)));

        // When
        Boolean resultaat = GebruikerService.Verifieer("email", "token");

        // Then
        Assert.False(resultaat);
    }

    [Fact]
    // Test of de gebruiker kan inloggen zonder dat deze geverifieerd is
    public void LoginTestZonderVerificatie(){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(true), new MockGebruikerContext());
        GebruikerService.Registreer("email", "wachtwoord");

        // When
        bool resultaat = GebruikerService.Login("email", "wachtwoord");

        // Then
        Assert.False(resultaat);
    }

    [Fact]
    // Test of de gebruiker kan inloggen nadat deze geverifieerd is
    public void LoginTestMetVerificatie(){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(true), new MockGebruikerContext());
        GebruikerService.Registreer("email", "wachtwoord");

        // When
        GebruikerService.Verifieer("email", "token");
        bool resultaat = GebruikerService.Login("email", "wachtwoord");

        // Then
        Assert.True(resultaat);
    }

    [Theory]
    [InlineData("email", "wachtwoord", "token", true)]
    [InlineData("email", "wachtwoord", "token", false)]
    // Combinatie van bovenstaande tests
    public void LoginTestCombined(string email, string wachtwoord, string token, bool verifieer){
        // Given
        GebruikerService GebruikerService = new GebruikerService(new MockEmailService(true), new MockGebruikerContext());
        GebruikerService.Registreer(email, wachtwoord);
        if(verifieer){
            GebruikerService.Verifieer(email, token);
        }

        // When
        bool resultaat = GebruikerService.Login(email, wachtwoord);

        // Then
        Assert.Equal(verifieer, resultaat);
    }

    //Met Mock test
    [Theory]
    [InlineData("email", "wachtwoord", "email", "wachtwoord", true, 1)]
    [InlineData("niet gelukt", "niet gelukt", "email", "wachtwoord", false, 0)]
    public void MoqRegistreerTest(string expectedEmail, string expectedWachtwoord, string actualEmail, string actualWachtwoord, bool emailVerzonden, int x){
        // Given
        var MockMail = new Mock<IEmailService>();
        MockMail.Setup(x => x.Email(It.IsAny<string>(), It.IsAny<string>())).Returns(emailVerzonden);
        //MockMail.Setup((x) => x.Email("x", "y")).Returns(true);

        var MockContext = new Mock<IGebruikerContext>();
        MockContext.Setup(x => x.AantalGebruikers()).Returns(1);
        MockContext.Setup(x => x.GetGebruiker(It.IsAny<int>())).Returns(new Gebruiker(actualEmail, actualWachtwoord));

        // When
        var gebruikerService = new GebruikerService(MockMail.Object, MockContext.Object);
        var gebruiker = gebruikerService.Registreer(actualEmail, actualWachtwoord);

        // Then
        Assert.Equal(expectedEmail, gebruiker.Email);
        Assert.Equal(expectedWachtwoord, gebruiker.Wachtwoord);

        MockMail.Verify(x => x.Email(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        MockContext.Verify(x => x.AantalGebruikers(), Times.Exactly(x));
    }

    [Theory]
    [InlineData("email", "email", "token", "wachtwoord", true)]
    [InlineData("email", "email", null, "wachtwoord", false)]
    [InlineData("email", "email", "token2", "wachtwoord", false)]
    [InlineData("fout", "email", "token2", "wachtwoord", false)]
    [InlineData("email", "fout", "token2", "wachtwoord", false)]
    public void MoqVerifieerTest(string email2, string email, string token, string wachtwoord, Boolean expected){
        // Given
        var MockMail = new Mock<IEmailService>();
        var MockContext = new Mock<IGebruikerContext>();
        MockContext.Setup(x => x.AlleGebruikers()).Returns(new List<Gebruiker>(){new Gebruiker("emailtest", "wachtwoordtest"), new Gebruiker(email, wachtwoord)});

        // When
        var gebruikerService = new GebruikerService(MockMail.Object, MockContext.Object);
        var resultaat = gebruikerService.Verifieer(email2, token);

        // Then
        Assert.Equal(expected, resultaat);

        MockContext.Verify(x => x.AlleGebruikers(), Times.Exactly(1));
    }

    [Theory]
    [InlineData("email", "wachtwoord", "token", true, 1)]
    [InlineData("email", "wachtwoord", "token", false, 2)]
    public void MoqLoginTest(string email, string wachtwoord, string token, bool verifieer, int x){
        // Given
        var MockMail = new Mock<IEmailService>();
        var MockContext = new Mock<IGebruikerContext>();
        MockContext.Setup(x => x.AlleGebruikers()).Returns(new List<Gebruiker>(){new Gebruiker("emailtest", "wachtwoordtest"), new Gebruiker(email, wachtwoord)});

        // When
        var gebruikerService = new GebruikerService(MockMail.Object, MockContext.Object);
        if(verifieer){
            gebruikerService.Verifieer(email, token);
        }
        var resultaat = gebruikerService.Login(email, wachtwoord);

        // Then
        Assert.Equal(verifieer, resultaat);

        MockContext.Verify(x => x.AlleGebruikers(), Times.AtLeastOnce);
    }
}