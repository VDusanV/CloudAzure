using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data.Interfejsi
{
    [ServiceContract]
    public interface IReadFilm
    {
        [OperationContract]
        List<Film> DobaviFilmove();

        [OperationContract]
        List<Klijent> DobaviKlijente();

        [OperationContract]
        List<IznajmljeniFilm> DobaviIznajmljene();
    }
}
