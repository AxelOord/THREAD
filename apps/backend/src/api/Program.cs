using Microsoft.OpenApi.Models;
using Thread.Infrastructure.Exstensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
        .InstallServicesFromAssemblies(
            builder.Configuration,
            Thread.Api.AssemblyReference.Assembly,
            Thread.Persistence.AssemblyReference.Assembly)
        .InstallModulesFromAssemblies(
            builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Thread",
        Version = "v1"
    });

    // Configure operation tags
    c.TagActionsBy(api => ["Thread"]);

    // OAuth2 scheme
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerZendeskAuth:AuthorizationUrl"]),
                TokenUrl = new Uri(builder.Configuration["SwaggerZendeskAuth:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                {
                    { builder.Configuration["SwaggerZendeskAuth:Scope"], "read permission to all resources" }
                }
            }
        }
    });

    // Security requirements for both schemes
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new List<string>()
        },
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(builder.Configuration["SwaggerZendeskAuth:ClientId"]);
        c.OAuthClientSecret(builder.Configuration["SwaggerZendeskAuth:ClientSecret"]);


        // Use response interceptor to store the access token
        c.UseResponseInterceptor("function(res) { " +
            "if (res.url === 'https://vanderlelie.zendesk.com/oauth/tokens') { " +
            "const responseBody = JSON.parse(res.data); " +
            "const accessToken = responseBody.access_token; " +
            "localStorage.setItem('oauthAccessToken', accessToken); " +
            "console.log('Access Token:', accessToken); " +
            "} " +
            "return res; }");

        // Use request interceptor to add the token to the custom header
        c.UseRequestInterceptor("function(req) { " +
            "const token = localStorage.getItem('oauthAccessToken'); " +
            "if (token) { req.headers['X-Oauth-Token'] = token; } " +
            "return req; }");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();