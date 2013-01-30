using System;
using System.Linq;
using Hotello.Common;
using Hotello.Services.Expedia.Hotels.Models;
using NUnit.Framework;

namespace Hotello.Services.Expedia.Tests
{
    [TestFixture]
    public class AmenityMaskTests
    {
        [Test(Description = "Amenity bitmask contains the right bitmasks when set")]
        public void BitmaskTest()
        {
            // Arrange

            HotelSummary hotelSummary = new HotelSummary
                {
                    AmenityMask = (int) new[]
                        {
                            Amenities.Internet | Amenities.IndoorPool | Amenities.KidsActivities
                        }
                        .CombineFlags()
                };

            // Act
            var amenities = hotelSummary.Amenities.GetFlags().ToList();

            foreach (var amenity in amenities)
            {
                Console.WriteLine(amenity.GetDescription());
            }

            // Assert

            Assert.That(amenities.Contains(Amenities.Internet));
            Assert.That(amenities.Contains(Amenities.IndoorPool));
            Assert.That(amenities.Contains(Amenities.KidsActivities));

        }
    }
}
