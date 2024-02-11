using BioscoopCasus.Domain;
using System.Diagnostics;
namespace BioscoopCasus.Tests {
    public class OrderTests {
        [Fact]
        public void CalculatePrice_OneStandardTicket_Weekend_NonStudentOrder_ShouldCalculateCorrectPrice() {
            // Arrange
            Order order = CreateOrder(1, 10, false, true, false);

            // Act
            var totalPrice = order.CalculatePrice();

            // Assert
            Assert.Equal(10, totalPrice);
        }

        [Fact]
        public void CalculatePrice_FiveStandardTickets_Weekday_NonStudentOrder_ShouldCalculateCorrectPrice() {
            // Arrange
            Order order = CreateOrder(5, 10, false, false, false);

            // Act
            var totalPrice = order.CalculatePrice();

            // Assert
            Assert.Equal(30, totalPrice);
        }

        [Fact]
        public void CalculatePrice_EightStandardTickets_Weekend_NonStudentOrder_ShouldCalculateCorrectPrice() {
            // Arrange
            Order order = CreateOrder(8, 10, false, true, false);

            // Act
            var totalPrice = order.CalculatePrice();

            // Assert
            Assert.Equal(36, totalPrice);
        }

        [Fact]
        public void CalculatePrice_FiveStandardTicket_Weekday_StudentOrder_ShouldCalculateCorrectPrice() {
            // Arrange
            Order order = CreateOrder(5, 10, true, false, true);

            // Act
            var totalPrice = order.CalculatePrice();

            // Assert
            Assert.Equal(36, totalPrice);
        }

        [Fact]
        public void CalculatePrice_SixPremiumTicket_Weekday_NonStudentOrder_ShouldCalculateCorrectPrice() {
            // Arrange
            Order order = CreateOrder(6, 10, true, false, false);

            // Act
            var totalPrice = order.CalculatePrice();

            // Assert
            Assert.Equal(35.1M, totalPrice);
        }

       private static Order CreateOrder(int numberOfTickets, decimal price, bool isPremium, bool isWeekend, bool isStudentOrder) {
            Movie movie = new Movie("Spongebob");
            DateTime date = isWeekend ? new DateTime(2024, 2, 10) : new DateTime(2024, 2, 6);
            MovieScreening movieScreening = new MovieScreening(movie, date, price);
            Order order = new Order(1, isStudentOrder);

            for (int i = 0; i < numberOfTickets; i++) {
                order.AddSeatReservation(new MovieTicket(movieScreening, 1, 1, isPremium));
            }

            return order;
        }
    }
}