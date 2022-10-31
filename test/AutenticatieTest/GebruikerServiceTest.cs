namespace test;
public class GebruikerServiceTest{

    //Mock tests
    public MockGebruikerService MockGebruikerService = new MockGebruikerService();

    [Fact]
    //Test of de gebruiker juist wordt geverifieerd
    public void VerificatieTest(){
        // Given
        Gebruiker gebruiker = new Gebruiker("email", "wachtwoord");

        // When
        bool resultaat = MockGebruikerService.Verifieer("email", gebruiker.Token.token);

        // Then
        Assert.True(resultaat);
    }

    [Theory]
    [InlineData("email", "wachtwoord")]
    //Test of de gebruiker juist wordt aangemaakt
    public void RegistreerTest(string email, string wachtwoord){
        // Given
        Gebruiker gebruiker = MockGebruikerService.Registreer(email, wachtwoord);

        // When
        string emailcheck = gebruiker.Email;
        string wachtwoordcheck = gebruiker.Wachtwoord;

        // Then
        Assert.Equal(email, emailcheck);
        Assert.Equal(wachtwoord, wachtwoordcheck);
    }

    [Fact]
    //Test of er kan worden ingelogd nadat de gebruiker is geverifieerd
    public void LoginMetVerificatie(){
        // Given
        Gebruiker gebruiker = MockGebruikerService.Registreer("EmailTest", "WachtwoordTest");

        // When
        bool Verifieer = MockGebruikerService.Verifieer("EmailTest", "token");
        bool Test = MockGebruikerService.Login("EmailTest", "WachtwoordTest");

        // Then
        Assert.True(Test);
    }

    [Fact]
    //Test of er kan worden ingelogd als de gebruiker niet is geverifieerd
    public void LoginZonderVerificatie(){
        // Given
        Gebruiker gebruiker = new Gebruiker("EmailTest2", "WachtwoordTest2");

        // When
        bool Test = MockGebruikerService.Login("EmailTest2", "WachtwoordTest2");

        // Then
        Assert.False(Test);
    }

    [Fact]
    //Test of de gebruiker kan inloggen met een verlopen token
    public void TokenDatumVerlopen(){
        // Given
        Gebruiker gebruiker = MockGebruikerService.Registreer("email", "wachtwoord");
        gebruiker.Token?.VerloopDatum.AddDays(-4);

        // When
        bool Test = MockGebruikerService.Login("EmailTest", "WachtwoordTest");

        // Then
        Assert.False(Test);
        Assert.True(!Test); //(other way of phrasing it)
    }

    //moq tests

    // [Theory]
    // [InlineData("email", "wachtwoord")]
    // public void MockLoginMetVerificatie(string email, string wachtwoord){
    //     // Given
    //     var moqGebruikerContext = new Mock<IGebruikerContext>();
    //     Gebruiker gebruiker = new Gebruiker(email, wachtwoord);

    //     gebruiker.Token = null;
    //     moqGebruikerContext.Setup(x => x.AlleGebruikers()).Returns(new List<Gebruiker> { gebruiker });

    //     GebruikerService service = new GebruikerService(moqEmailService.Object, moqGebruikerContext.Object);

    //     // When
    //     bool poging = service.Login(email, wachtwoord);

    //     // Then
    //     Assert.True(poging);
    // }

    // [Theory]
    // [InlineData("email", "wachtwoord")]
    // public void MockLoginZonderVerificatie(string email, string wachtwoord){
    //     // Given
    //     var moqEmailService = new Mock<IEmailService>();
    //     var moqGebruikerContext = new Mock<IGebruikerContext>();
    //     Gebruiker gebruiker = new Gebruiker(email, wachtwoord);
    //     moqGebruikerContext.Setup(u => u.AlleGebruikers()).Returns(new List<Gebruiker> {gebruiker});

    //     GebruikerService service = new GebruikerService(moqEmailService.Object, moqGebruikerContext.Object);

    //     // When
    //     bool poging = service.Login(email, wachtwoord);

    //     // Then
    //     Assert.False(poging);
    // }

    [Theory]
    [InlineData("email", "wachtwoord", "token")]
    public void MockTokenDatumVerlopen(string email, string wachtwoord, string token){
        // Given
        var moqGebruikerContext = new Mock<IGebruikerContext>();
        Gebruiker gebruiker = new Gebruiker(email, wachtwoord);

        if (gebruiker.Token != null){
            gebruiker.Token.token = token;
            gebruiker.Token?.VerloopDatum.AddDays(-4);
        }
        
        moqGebruikerContext.Setup(u => u.AlleGebruikers()).Returns(new List<Gebruiker> {gebruiker});
        MockGebruikerService MockService = new MockGebruikerService();

        // When
        bool poging = MockService.Verifieer(email, wachtwoord);

        // Then
        Assert.False(poging);
    }
}
