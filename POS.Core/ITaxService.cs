using POS.Core.DTO;

namespace POS.Core
{
    public interface ITaxService
    {
        List<Tax> GetAllTaxes();
        Tax GetTaxById(int id);

        Tax CreateTax(CreateTaxRequest request);

        Tax UpdateTax(UpdateTaxRequest request);

        void DeleteTaxById(int id);
    }
}
