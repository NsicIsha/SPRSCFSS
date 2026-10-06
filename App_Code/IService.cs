using System.Data;
using System.ServiceModel;

// NOTE: If you change the interface name "IService" here, you must also update the reference to "IService" in Web.config.
[ServiceContract]
public interface IService
{
    [OperationContract]
    DataSet FethData();
}
