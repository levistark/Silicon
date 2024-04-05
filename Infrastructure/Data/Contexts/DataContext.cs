﻿using Infrastructure.Entities;
using Infrastructure.Entities.Course;
using Infrastructure.Models.Identification;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;
public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public virtual DbSet<CourseCategoryEntity> Categories { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<CourseEntity> Courses { get; set; }
    public virtual DbSet<CourseStepEntity> CourseSteps { get; set; }
    public virtual DbSet<AuthorEntity> Authors { get; set; }
    public virtual DbSet<BadgeEntity> Badges { get; set; }
    public virtual DbSet<CourseBadgeEntity> CourseBadges { get; set; }
    public virtual DbSet<SocialMediaEntity> SocialMediaPlatforms { get; set; }
    public virtual DbSet<SubscriberEntity> Subscribers { get; set; }
    public virtual DbSet<UserCourseSubscriptionEntity> UserCourseSubscriptions { get; set; }
    public virtual DbSet<CourseSpecificationEntity> CourseSpecifications { get; set; }
}
