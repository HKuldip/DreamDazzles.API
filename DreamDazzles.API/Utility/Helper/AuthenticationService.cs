namespace DreamDazzles.API.Utility.Helper;
public interface IAuthenticationService
{
    //Task<Tuple<AuthenticateResponse?, Response>> AuthenticateAsync(AuthenticateRequest model,  CancellationToken ctoken = default);
    //Task<bool> CheckTokenIsValid(JWTTokenRequest model, CancellationToken token);
    //string GetTokenExpirationTime(string? token, string ClaimType);
    //Task<AuthenticateResponse?> RevokeToken(UserDetailsDTO model);
}

public class AuthenticationService : IAuthenticationService
{
    //private readonly IOptions<JwtOptions> _appSettings;
    //private readonly IOptions<AppSettings> _appSettingsLocal;
    //public AuthenticationService(IOptions<JwtOptions> appSettings, ILoginService userService, IOptions<AppSettings> appSettingsLocal)
    //{
    //    _appSettings = appSettings;
    //    _loginService = userService;
    //    _appSettingsLocal = appSettingsLocal;
    //}
    //public async Task<Tuple<AuthenticateResponse?, Response>> AuthenticateAsync(AuthenticateRequest model, CancellationToken ctoken = default)
    //{
    //    var tuple = await _loginService.AuthenticateUserAsync(model);

    //    var user = tuple.Item1;
    //    Response result = tuple.Item2;

    //    // return null if user not found
    //    if (!result.IsSuccess) return await Task.FromResult(new Tuple<AuthenticateResponse, Response>(null, result));

    //    string refreshToken = GenerateRefreshToken();
    //    _ = int.TryParse(_appSettings.Value.RefreshTokenValidityInDays, out int refreshTokenValidityInDays);
    //    DateTime RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
    //    // authentication successful so generate jwt token        
    //    TokenandRefreshTokenGenerate(user, out string token);
    //    TokenDTO objRToken = new()
    //    {
    //        ID = user.UserId,
    //        RefreshToken = refreshToken,
    //        RefreshTokenExpiryTime = RefreshTokenExpiryTime
    //    };
    //    await _loginService.UpdateTokenAsync(objRToken);

    //    return await Task.FromResult(new Tuple<AuthenticateResponse, Response>(new AuthenticateResponse(user, token, refreshToken, _appSettings.Value.ExpiryMinutes), result));
    //}

    //private void TokenandRefreshTokenGenerate(UserDetailsDTO user, out string token)
    //{
    //    token = GenerateJwtToken(user);
    //    _loginService.UpdateLastLogin(user.UserId);
    //}

    //public async Task<bool> CheckTokenIsValid(JWTTokenRequest model, CancellationToken token)
    //{
    //    #region ManualWork
    //    //var tokenTicks = long.Parse(GetTokenExpirationTime(model.token));
    //    //var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

    //    //var now = DateTime.Now.ToUniversalTime();

    //    //var valid = tokenDate >= now; 
    //    #endregion
    //    JwtSecurityToken jwtSecurityToken;
    //    try
    //    {
    //        jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(model.token);
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }

    //    return jwtSecurityToken.ValidTo > DateTime.UtcNow;
    //}

    //private string GenerateJwtToken(UserDetailsDTO user)
    //{
    //    var key = Encoding.ASCII.GetBytes(_appSettings.Value.SecretKey);
    //    #region Old Code
    //    //        var tokenHandler = new JwtSecurityTokenHandler();

    //    //        var claims = new List<Claim>
    //    //                    {
    //    //                        new Claim(ClaimTypes.Email, user.Email),
    //    //                        new Claim(ClaimTypes.Name, user.FirstName),
    //    //                        new Claim(ClaimTypes.Surname, user.LastName),
    //    //                        new Claim(ClaimTypes.Role,Convert.ToString(user.RoleId))
    //    //                    };
    //    //        var tokenDescriptor = new SecurityTokenDescriptor
    //    //        {
    //    //            //Claims = (IDictionary<string, object>)claims,
    //    //            Subject = new ClaimsIdentity(new[] {
    //    //                new Claim("id", Convert.ToString(user.UserId)) ,
    //    //                new Claim("RoleID", Convert.ToString(user.RoleId))

    //    //            }),
    //    //            Expires = DateTime.Now.AddMinutes(_appSettings.Value.ExpiryMinutes),
    //    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
    //    //, SecurityAlgorithms.HmacSha256Signature)
    //    //        };
    //    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //    //        return tokenHandler.WriteToken(token);
    //    #endregion
    //    #region New Code
    //    var now = DateTime.Now;
    //    var secretKey = new SymmetricSecurityKey(key);
    //    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

    //    var path = _appSettingsLocal.Value.FilePath.ProfilePhoto;
    //    var url = _appSettingsLocal.Value.ServerURL;
    //    var LocalPath = _appSettingsLocal.Value.Smtp.LDFApphost;
    //    var ProfileIMG = user.ProfileIMG == null ? $"{LocalPath}/assets/images/DefaultProfileImg.png" : $"{url}/{path}{user.UserId}/ProfileImg/{user.ProfileIMG}";
    //    var jwtClaims = new List<Claim>
    //                {
    //                    new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(user.UserId)),
    //                    new Claim(JwtRegisteredClaimNames.UniqueName, Convert.ToString(user.UserId)),
    //                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString()),
    //                    //new Claim(JwtRegisteredClaimNames.Aud, _appSettings.Value.ValidAudience),

    //                    new Claim("id", Convert.ToString(user.UserId)),
    //                    new Claim(ClaimTypes.Email, user.Email),
    //                    new Claim(ClaimTypes.Name, user.FullName),
    //                    new Claim(ClaimTypes.Role,Convert.ToString(user.RoleCode)),
    //                    new Claim("TenantID",Convert.ToString(user.CompanyId)),
    //                    new Claim("BranchID",Convert.ToString(user.BranchId)),
    //                    new Claim("ProfileImgPath",Convert.ToString($"{ProfileIMG}")),
    //                    new Claim("GroupId",Convert.ToString(user.GroupID)),
    //                    new Claim("GroupCode",Convert.ToString(user.GroupCode)),
    //                    new Claim("GroupType",Convert.ToString(user.GroupType)),
    //                };


    //    var tokeOptions = new JwtSecurityToken(
    //        issuer: _appSettings.Value.Issuer,
    //        audience: _appSettings.Value.ValidAudience,
    //        claims: jwtClaims,
    //        expires: DateTime.Now.AddMinutes(_appSettings.Value.ExpiryMinutes),
    //        signingCredentials: signinCredentials
    //    );

    //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    //    return tokenString;
    //    #endregion
    //}



    //private static string GenerateRefreshToken()
    //{
    //    var randomNumber = new byte[64];
    //    using var rng = RandomNumberGenerator.Create();
    //    rng.GetBytes(randomNumber);
    //    return Convert.ToBase64String(randomNumber);
    //}

    //public string GetTokenExpirationTime(string? token, string ClaimType = "id")
    //{
    //    var handler = new JwtSecurityTokenHandler();
    //    var jwtSecurityToken = handler.ReadJwtToken(token);
    //    var tokenDetails = jwtSecurityToken.Claims.First(claim => claim.Type.Equals(ClaimType)).Value;
    //    return tokenDetails;
    //}

    //public async Task<AuthenticateResponse?> RevokeToken(UserDetailsDTO model)
    //{
    //    // return null if user not found
    //    if (model == null) return null;

    //    // authentication successful so generate jwt token        
    //    TokenandRefreshTokenGenerate(model, out string token);

    //    return await Task.FromResult(new AuthenticateResponse(model, token, model.RefreshToken, _appSettings.Value.ExpiryMinutes));
    //}
}
