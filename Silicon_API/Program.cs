using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
