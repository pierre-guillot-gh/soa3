using BioscoopCasus.Domain;

namespace BioscoopCasus.Interfaces {
    public interface IExport {
        void Export(Order order);
    }
}