using BioscoopCasus.Behaviours;
using BioscoopCasus.Domain;

namespace BioscoopCasus.Tests {
    public class ExportPlainTextTests {
        [Fact]
        public void ExportPlainText_ExportOrder_CheckFileContents() {
            // Arrange
            var order = new Order(1, false, new ExportPlainText());
            order.AddSeatReservation(new MovieTicket(new MovieScreening(new Movie("Test Movie"), new System.DateTime(2024, 2, 12), 10), 1, 1, false));

            // Act
            var exportPlainText = new ExportPlainText();
            exportPlainText.Export(order);

            // Assert
            Assert.True(File.Exists("order.txt"));
            string fileContent = File.ReadAllText("order.txt");
            Assert.Contains("Row number: 1", fileContent);
            Assert.Contains("Seat number: 1", fileContent);
            Assert.Contains("Is premium: No", fileContent);

            // Clean up
            File.Delete("order.txt");
        }
    }
}
