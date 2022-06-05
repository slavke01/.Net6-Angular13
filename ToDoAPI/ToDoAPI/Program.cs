
using Microsoft.EntityFrameworkCore;
using ToDoInfrastructure;
using ToDoInfrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ToDoAPI;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args) ;

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
        );

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
                    options.Authority = builder.Configuration["Domain"];
                    options.Audience = builder.Configuration["Audience"];
                    

    options.TokenValidationParameters = new TokenValidationParameters
    {

                NameClaimType = ClaimTypes.NameIdentifier,
                
                    };

                });



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.Requirements.Add(new HasScopeRequirement("Admin", builder.Configuration["Domain"])));
});
//Cors
builder.Services.AddCors(p => p.AddPolicy("cors", builder => {
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
//Database
builder.Services.AddDbContext<ToDoContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//IOC
builder.Services.AddTransient<ToDoContext,ToDoContext>();
builder.Services.AddTransient<IToDoListOperations, ToDoListOperations>();
builder.Services.AddTransient<IToDoListItemOperations, ToDoListItemOperations>();


builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();



builder.Services.AddHttpContextAccessor();

//background task
builder.Services.AddHostedService<ReminderHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


//Automatsko Update-Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ToDoContext>();
    context.Database.Migrate();
}

app.UseCors("cors");
app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
