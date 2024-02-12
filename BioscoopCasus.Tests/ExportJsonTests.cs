
using BioscoopCasus.Behaviours;
using BioscoopCasus.Domain;
using System.Text.Json;

namespace BioscoopCasus.Tests {
    public class ExportJsonTests {
        [Fact]
        public void ExportJson_ExportOrder_CheckFileContents() {
            // Arrange
            var order = new Order(1, false, new ExportJson());
            order.AddSeatReservation(new MovieTicket(new MovieScreening(new Movie("Test Movie"), new System.DateTime(2024, 2, 12), 10), 1, 1, false));

            // Act
            order.Export();

            // Assert
            Assert.True(File.Exists("order.json"));

            // Read the contents of the file
            string fileContent = File.ReadAllText("order.json");

            // Assert that the content matches the expected JSON format
            Assert.Contains("\"OrderNumber\": 1", fileContent);
            Assert.Contains("\"RowNr\": 1", fileContent);
            Assert.Contains("\"IsPremium\": false", fileContent);

            // Clean up
            File.Delete("order.json");
        }
    }
}
