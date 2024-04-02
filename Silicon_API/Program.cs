using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Silicon_API.Configurations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterSwagger();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BadgeRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseStepRepository>();
builder.Services.AddScoped<CourseBadgeRepository>();
builder.Services.AddScoped<CourseSpecificationsRepository>();
builder.Services.AddScoped<UserCourseSubscriptionRepository>();
builder.Services.AddScoped<SubscriberRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CourseManager>();
builder.Services.AddScoped<CourseSubscriptionManager>();

builder.Services.AddHttpClient();
builder.Services.RegisterJwt(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicon Web Api v1"));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
