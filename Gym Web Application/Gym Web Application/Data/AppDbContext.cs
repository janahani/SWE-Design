namespace Gym_Web_Application.Data;

using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
public class AppDbContext :DbContext
{
     public AppDbContext(DbContextOptions<AppDbContext> options):base(options){

     }
     public DbSet<ClientModel> Clients { get; set; }
     public DbSet<PackageModel> Packages { get; set; }
}