using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IShoppingCartService
    {
        [OperationContract]
        Task AddItem(ShoppingCartItem item);
        [OperationContract]
        Task DeleteItem(ShoppingCartItem item);
        [OperationContract]
        Task<IList<ShoppingCartItem>> GetItems();
    }
}
