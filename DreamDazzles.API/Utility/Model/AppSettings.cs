namespace LoanCentral.API.Utility.Model;

public class AppOptions
{
    public string Name { get; set; }    
    public string Version { get; set; }
}
public class AppSettings
{
    public string? Secret { get; set; }
    public int TokenExpiryInMins { get; set; }
    public Smtp Smtp { get; set; }

    public int ResetPwdExpTimeInMins { get; set; }
    public FilePath FilePath { get; set; }
    public ResponseMSG ResponseMSG { get; set; }
    public TwilioCrd TwilioCrd { get; set; }
    public string ServerURL { get; set; }
    public string LoginPassword { get; set; }
}

public class ResponseMSG
{
    public string WelComeUserPasswordLinkMailSuccess { get; set; }
    public string WelComeUserPasswordLinkMailFailed { get; set; }
    public string ResetPasswordLinkMailSuccess { get; set; }
    public string ResetPasswordLinkMailFailed { get; set; }
}

public class TwilioCrd
{
    public string? TWILIO_ACCOUNT_SID { get; set; }
    public string? TWILIO_AUTH_TOKEN { get; set; }
}



public class Smtp
{
    public string LDFApphost { get; set; }
    public string Server { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSSL { get; set; }
}

public class JwtOptions
{
    public string SecretKey { get; set; }
    public bool ValidateIssuer { get; set; }
    public string Issuer { get; set; }
    public int ExpiryMinutes { get; set; }
    public bool ValidateLifetime { get; set; }
    public bool ValidateAudience { get; set; }
    public string ValidAudience { get; set; }
    public string RefreshTokenValidityInDays { get; set; }
}

public class FilePath
{    
    public string ProfilePhoto { get; set; }
    public string LenderLogo { get; set; }
    public string DocumentTemplates { get; set; }
}

