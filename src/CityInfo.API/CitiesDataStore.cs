using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore() {

            Cities = new List<CityDto>()
            {

                new CityDto()
                {
                    Id = 1,
                    Name = "New York",
                    Description = "Big Park",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Tower",
                            Description = "Long tower"
                        },
                         new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Tower",
                            Description = "Long tower"
                        },
                    }

                },
                   new CityDto()
                {
                    Id = 2,
                    Name = "Paris",
                    Description = "Croissant factory",
                       PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Tower",
                            Description = "Long tower"
                        },
                    }

                },
                      new CityDto()
                {
                    Id = 3,
                    Name = "Washington DC",
                    Description = "American one",

                       PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Tower",
                            Description = "Long tower"
                        },
                    }

                },
                         new CityDto()
                {
                    Id = 4,
                    Name = "Amsterdam",
                    Description = "Drug lords area",
                       PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Tower",
                            Description = "Long tower"
                        },
                    }

                },


            };

        }
    }
}

