using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;

        }

        // Devolve uma lista de Sellers
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
    }
}
