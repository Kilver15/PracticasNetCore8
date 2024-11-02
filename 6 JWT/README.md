Implementacion de JWTBearer en ASP .NET
- Instalar paquete Microsoft.AspNetCore.Authentication.JwtBearer
- En appsettings.json, crear la clave secreta de JWT:
JWT : {
  Key : { Clave }
}
La clave se puede generar en SqlServer con SELECT NEWID()
- Crear archivo de Utilidades y crear las siguientes funciones:
 public string encriptarSHA256(string texto)
 {
     using (SHA256 sha256Hash = SHA256.Create())
     {
         byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
         StringBuilder builder = new StringBuilder();
         for (int i = 0; i < bytes.Length; i++)
         {
             builder.Append(bytes[i].ToString("x2"));
         }
         return builder.ToString();
     }
 }

 public string generarJWT(User modelo)
 {
     var userclaims = new[]
     {
         new Claim(ClaimTypes.NameIdentifier, modelo.Id.ToString()),
         new Claim(ClaimTypes.Name, modelo.Username!),
     };

     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
     var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

     var jwtConfig = new JwtSecurityToken(
         claims: userclaims,
         expires: DateTime.Now.AddMinutes(30),
         signingCredentials: credenciales
     );

     return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
- En program.cs , agregar el singleton y la autenticacion:
builder.Services.AddSingleton<Utilidades>();

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
        };
    });

Usar [AllowAnonimous] en controladores que no se requiera el JWT, y [Authorize] en los que si.
