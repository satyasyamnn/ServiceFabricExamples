using System.ServiceModel;
using System.Threading.Tasks;

namespace CalculatorService
{
    [ServiceContract]
    public interface ICalculatorService 
    {
        [OperationContract]
        Task<string> Add(int a, int b);
        [OperationContract]
        Task<string> Substract(int a, int b);
    }
}
