using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public CityInfoContext(DbContextOptions<CityInfoContext> dbContextOptions):base(dbContextOptions) 
        {
            
        }


        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                 .HasData(
                    new City("New York")
                    {
                        Id= 1,
                        Description="Big park"
                    },
                    new City("Islamabad")
                    {
                        Id = 2,
                        Description = "Corrupt city"
                    },
                    new City("Dubai")
                    {
                        Id = 3,
                        Description = "Materialistic"
                    },
                    new City("London")
                    {
                        Id = 4,
                        Description = "Dangerous city with knife crime."
                    }
                );

            modelBuilder.Entity<PointOfInterest>()
        .HasData(
            new PointOfInterest("Central Park")
            {
                Id = 1,
                CityId = 1,
                Description = "Most visited park in the United States"
            },
            new PointOfInterest("Times Square")
            {
                Id = 2,
                CityId = 1,
                Description = "Famous commercial intersection"
            },
            new PointOfInterest("Faisal Mosque")
            {
                Id = 3,
                CityId = 2,
                Description = "Largest mosque in Pakistan"
            },
            new PointOfInterest("Burj Khalifa")
            {
                Id = 4,
                CityId = 3,
                Description = "Tallest building in the world"
            },
            new PointOfInterest("The Dubai Mall")
            {
                Id = 5,
                CityId = 3,
                Description = "Largest mall in the world"
            },
            new PointOfInterest("Buckingham Palace")
            {
                Id = 6,
                CityId = 4,
                Description = "Official residence of the monarch"
            },
            new PointOfInterest("The British Museum")
            {
                Id = 7,
                CityId = 4,
                Description = "World-famous museum"
            }
        );
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("con string");
        //}
    }
}
