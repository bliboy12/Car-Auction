using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarAuction.Persistence;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1. Database
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// 3. Repositories / Unit of Work / Identity service
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();


// 4. Command/Query Handlers
builder.Services.AddScoped<CreateAuctionCommandHandler>();
builder.Services.AddScoped<PlaceBidCommandHandler>();
builder.Services.AddScoped<GetAuctionByIdQueryHandler>();
builder.Services.AddScoped<GetBidsByAuctionIdQueryHandler>();
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<RegisterClientCommandHandler>();
builder.Services.AddScoped<GetClientProfileByIdQueryHandler>();
builder.Services.AddScoped<LoginCommandHandler>();



// 5. Standard ASP.NET Core plumbing
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// 6. MediatR - pick up every handler in the associated assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateAuctionCommand).Assembly,      // Modules.Auction.Application (covers Auction + Bid)
    typeof(CreateCarCommand).Assembly,          // Modules.Car.Application
    typeof(LoginCommand).Assembly,              // Modules.Identity.Application (covers Login, Register, GetClientProfileById)
    typeof(CreateTransactionCommand).Assembly   // Modules.Transaction.Application, once it exists
));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateActor = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();