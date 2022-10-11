public class GebruikerServiceTest{

    //Normale tests
    public GebruikerContext gebruikerContext = new GebruikerContext();
    public GebruikerService gebruikerService = new GebruikerService(new EmailService(), new GebruikerContext());
    public EmailService emailSender = new EmailService();

    [Fact]
    public void LoginMetVerificatie(){
        // Given
        Gebruiker gebruiker = gebruikerService.Registreer("EmailTest", "WachtwoordTest");

        // When
        bool Verifieer = gebruikerService.Verifieer("EmailTest", "token");
        bool Test = gebruikerService.Login("EmailTest", "WachtwoordTest");

        // Then
        Assert.True(Test);
    }

    [Fact]
    public void LoginZonderVerificatie(){
        // Given
        Gebruiker gebruiker = new Gebruiker("EmailTest2", "WachtwoordTest2");

        // When
        bool Test = gebruikerService.Login("EmailTest2", "WachtwoordTest2");

        // Then
        Assert.False(Test);
    }

    [Fact]
    public void TokenDatumVerlopen(){
        // Given
        Gebruiker gebruiker2 = new Gebruiker("EmailTest", "WachtwoordTest");
        gebruiker2.Token?.VerloopDatum.AddDays(-4);

        // When
        bool Test = gebruikerService.Login("EmailTest", "WachtwoordTest");

        // Then
        Assert.False(Test);
        Assert.True(!Test); //(other way of phrasing it)
    }

    //mock tests
    [Theory]
    [InlineData("email", "wachtwoord")]
    public void MockLoginMetVerificatie(string email, string wachtwoord){
        // Given
        var moqEmailService = new Mock<IEmailService>();
        var moqGebruikerContext = new Mock<IGebruikerContext>();
        Gebruiker gebruiker = new Gebruiker(email, wachtwoord);

        gebruiker.Token = null;
        moqGebruikerContext.Setup(x => x.AlleGebruikers()).Returns(new List<Gebruiker> { gebruiker });

        GebruikerService service = new GebruikerService(moqEmailService.Object, moqGebruikerContext.Object);

        // When
        bool poging = service.Login(email, wachtwoord);

        // Then
        Assert.True(poging);
    }

    [Theory]
    [InlineData("email", "wachtwoord")]
    public void MockLoginZonderVerificatie(string email, string wachtwoord){
        // Given
        var moqEmailService = new Mock<IEmailService>();
        var moqGebruikerContext = new Mock<IGebruikerContext>();
        Gebruiker gebruiker = new Gebruiker(email, wachtwoord);
        moqGebruikerContext.Setup(u => u.AlleGebruikers()).Returns(new List<Gebruiker> {gebruiker});

        GebruikerService service = new GebruikerService(moqEmailService.Object, moqGebruikerContext.Object);

        // When
        bool poging = service.Login(email, wachtwoord);

        // Then
        Assert.False(poging);
    }

    [Theory]
    [InlineData("email", "wachtwoord", "token")]
    public void MockTokenDatumVerlopen(string email, string wachtwoord, string token){
        // Given
        var moqEmailService = new Mock<IEmailService>();
        var moqGebruikerContext = new Mock<IGebruikerContext>();
        Gebruiker gebruiker = new Gebruiker(email, wachtwoord);

        if (gebruiker.Token != null){
            gebruiker.Token.token = token;
            gebruiker.Token?.VerloopDatum.AddDays(-4);
        }
        
        moqGebruikerContext.Setup(u => u.AlleGebruikers()).Returns(new List<Gebruiker> {gebruiker});

        GebruikerService service = new GebruikerService(moqEmailService.Object, moqGebruikerContext.Object);

        // When
        bool poging = service.Verifieer(email, wachtwoord);

        // Then
        Assert.False(poging);
    }
}

//zonder de afhankelijkheid testen, zorgen dat alleen de unit echt is 