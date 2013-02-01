using System;
using System.Collections.Generic;
using System.Linq;
using Hotello.Common;
using Hotello.Services.Expedia.Hotels.Models;
using NUnit.Framework;

namespace Hotello.Services.Expedia.Tests
{
    [TestFixture]
    public class AmenityTests
    {
        [Test(Description = "Amenity bitmask contains the right bitmasks when set")]
        public void BitmaskTest()
        {
            // Arrange
            HotelSummary hotelSummary = new HotelSummary();

            // Act
            hotelSummary.AmenityMask = (int) new[] { Amenities.Internet | Amenities.IndoorPool | Amenities.KidsActivities }.CombineFlags();
            List<Amenities> amenities = hotelSummary.Amenities.GetFlags().ToList();


            // Assert
            Assert.True(hotelSummary.Amenities.HasFlag(Amenities.Internet));
            Assert.True(hotelSummary.Amenities.HasFlag(Amenities.IndoorPool));
            Assert.True(hotelSummary.Amenities.HasFlag(Amenities.KidsActivities));

            foreach (var amenity in amenities)
            {
                Console.WriteLine(amenity.GetDescription());
            }

            Assert.That(amenities.Contains(Amenities.Internet));
            Assert.That(amenities.Contains(Amenities.IndoorPool));
            Assert.That(amenities.Contains(Amenities.KidsActivities));

        }
    }
}
