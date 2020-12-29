using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data.Interfejsi
{
    [ServiceContract]
    public interface IWriteFilm
    {
        [OperationContract]
        void UpisiFilm(Film f);

        [OperationContract]
        void ObrisiFilm(Film f);

        [OperationContract]
        void AzurirajFilm(Film f);

        [OperationContract]
        void UpisiKlijenta(Klijent k);

        [OperationContract]
        void ObrisiKlijenta(Klijent k);

        [OperationContract]
        void AzurirajKlijenta(Klijent k);

        [OperationContract]
        void UpisiIznajmljen(IznajmljeniFilm i);

        [OperationContract]
        void ObrisiIznajmljen(IznajmljeniFilm i);

        [OperationContract]
        void AzurirajIznajmljen(IznajmljeniFilm i);
    }
}
