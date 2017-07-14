using System.ServiceModel;
using System.Threading.Tasks;

namespace CalculatorService
{
    [ServiceContract]
    public interface ICalculatorService 
    {
        [OperationContract]
        Task<int> Add(int a, int b);
        [OperationContract]
        Task<int> Substract(int a, int b);
    }
}
