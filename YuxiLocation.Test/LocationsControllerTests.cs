using Microsoft.AspNetCore.Mvc;
using YuxiLocation.Controllers;

namespace YuxiLocation.Test
{
    public class LocationsControllerTests
    {
        [Fact]
        public void Get_ShouldReturnOnlyLocationsWithAvailabilityBetween10amAnd1pm()
        {
            var availabilityStart = new TimeSpan(10, 0, 0);
            var availabilityEnd = new TimeSpan(13, 0, 0);
            // Arrange
            var controller = new LocationsController();

            // Act
            var result = controller.Get();
            var okResult = result.Result as OkObjectResult;
            var locations = okResult?.Value as IEnumerable<Location>;

            // Assert
            Assert.NotNull(locations);
            Assert.Equal(7, locations.Count());
            Assert.True(locations.All(l => availabilityStart >= l.Availability && availabilityEnd <= l.ClosingTime));
        }

    }
}