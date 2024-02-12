using BioscoopCasus.Domain;
using BioscoopCasus.Interfaces;
using System.Text.Json;

namespace BioscoopCasus.Behaviours {
    public class ExportJson : IExport {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
      
        public void Export(Order order) {
            var json = JsonSerializer.Serialize(order, _jsonSerializerOptions);
            Console.WriteLine(json);
            File.WriteAllText("order.json", json);
        }
    }
}
