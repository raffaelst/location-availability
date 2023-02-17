using Microsoft.AspNetCore.Mvc;

namespace YuxiLocation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly List<Location> _locations = new List<Location>
        {
            new Location { Name = "Pharmacy", Availability = new TimeSpan(10, 0, 0), ClosingTime = new TimeSpan(20, 0, 0) },
            new Location { Name = "Bakery", Availability = new TimeSpan(6, 30, 0), ClosingTime = new TimeSpan(17, 30, 0) },
            new Location { Name = "Barber Shop", Availability = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(19, 0, 0) },
            new Location { Name = "Supermarket", Availability = new TimeSpan(8, 0, 0), ClosingTime = new TimeSpan(22, 0, 0) },
            new Location { Name = "Candy Store", Availability = new TimeSpan(10, 30, 0), ClosingTime = new TimeSpan(21, 0, 0) },
            new Location { Name = "Cinema Complex", Availability = new TimeSpan(11, 0, 0), ClosingTime = new TimeSpan(23, 0, 0) },
            new Location { Name = "Library", Availability = new TimeSpan(9, 0, 0), ClosingTime = new TimeSpan(17, 0, 0) },
            new Location { Name = "Bookstore", Availability = new TimeSpan(10, 0, 0), ClosingTime = new TimeSpan(20, 0, 0) },
            new Location { Name = "Coffee Shop", Availability = new TimeSpan(7, 0, 0), ClosingTime = new TimeSpan(19, 0, 0) },
            new Location { Name = "Ice Cream Parlor", Availability = new TimeSpan(12, 0, 0), ClosingTime = new TimeSpan(22, 0, 0) },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            var availabilityStart = new TimeSpan(10, 0, 0);
            var availabilityEnd = new TimeSpan(13, 0, 0);
            var availableLocations = _locations.Where(l => availabilityStart >= l.Availability && availabilityEnd <= l.ClosingTime);
            return Ok(availableLocations);
        }

        [HttpPost]
        public ActionResult AddLocation(AddLocationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = new Location
            {
                Name = dto.Name,
                Availability = TimeSpan.Parse(dto.Availability),
                ClosingTime = TimeSpan.Parse(dto.ClosingTime)
            };
            _locations.Add(location);

            return CreatedAtAction(nameof(Get), location);
        }
    }

    public class Location
    {
        public string Name { get; set; }
        public TimeSpan Availability { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }

    public class AddLocationDto
    {
        public string Name { get; set; }
        public string Availability { get; set; }
        public string ClosingTime { get; set; }
    }
}

