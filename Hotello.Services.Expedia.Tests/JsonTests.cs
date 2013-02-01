using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Response;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;

namespace Hotello.Services.Expedia.Tests
{
    [TestFixture]
    public class JsonTests
    {
        private readonly string _sampleDataPath = Path.Combine(Environment.CurrentDirectory, "SampleData");

        private string PathFor(string sampleFile)
        {
            return Path.Combine(_sampleDataPath, sampleFile);
        }

        [Test(Description = "Can deserialise hotel summary")]
        public void JsonAmenitiesBitmaskFromHotelSummary()
        {
            // Arrange
            string doc = File.ReadAllText(PathFor("HotelSummary.json"));

            // Act
            JsonDeserializer jsonDeserializer = new JsonDeserializer { RootElement = "HotelSummary" };
            List<HotelSummary> output = jsonDeserializer.Deserialize<List<HotelSummary>>(new RestResponse { Content = doc });

            // Assert
            Assert.NotNull(output);
        }


        [Test(Description = "Can deserialise hotel information")]
        public void JsonHotelInformationResponse()
        {
            // Arrange
            string doc = File.ReadAllText(PathFor("HotelInformationResponse.json"));

            // Act
            JsonDeserializer jsonDeserializer = new JsonDeserializer { RootElement = "HotelInformationResponse" };
            HotelInformationResponse output = jsonDeserializer.Deserialize<HotelInformationResponse>(new RestResponse { Content = doc });

            // Assert
            Assert.NotNull(output);

            Assert.That(output.HotelSummary.Name == "The Strand Palace");

            Assert.NotNull(output.HotelDetails);

            Assert.That(output.HotelDetails.NumberOfRooms == 785);

            Assert.That(output.RoomTypes.RoomType.Any(roomType => roomType.RoomAmenities.RoomAmenity.Any(roomAmenity => roomAmenity.Amenity == "In-room safe (laptop compatible) ")));

            Assert.NotNull(output.HotelImages);

            Assert.NotNull(output.HotelSummary);

            Assert.NotNull(output.PropertyAmenities);

            Assert.NotNull(output.Suppliers);

        }

        [Test(Description = "Can deserialise room availability response")]
        public void JsonRoomAvailabilityResponse()
        {
            // Arrange
            string doc = File.ReadAllText(PathFor("HotelRoomAvailabilityResponse.json"));
            
            // Act
            JsonDeserializer jsonDeserializer = new JsonDeserializer() {RootElement = "HotelRoomAvailabilityResponse"};
            HotelRoomAvailabilityResponse output = jsonDeserializer.Deserialize<HotelRoomAvailabilityResponse>(new RestResponse() {Content = doc});

            // Assert
            Assert.NotNull(output);

        }



    }
}
