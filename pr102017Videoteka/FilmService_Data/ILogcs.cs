using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data
{
    [ServiceContract]
    public interface ILogcs
    {
        [OperationContract]
        string SviLogovi();
    }
}
