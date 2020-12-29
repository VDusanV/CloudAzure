using FilmService_Data;
using FilmService_Data.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWorkerRole
{
    public class ReadFilmImplementacija : IReadFilm
    {
        public List<Film> DobaviFilmove()
        {
            FilmDataRepository repo = new FilmDataRepository();
            return repo.RetrieveAllFilms().ToList();
        }

        public List<IznajmljeniFilm> DobaviIznajmljene()
        {
            FilmDataRepository repo = new FilmDataRepository();
            return repo.RetrieveAllRented().ToList();
        }

        public List<Klijent> DobaviKlijente()
        {
            FilmDataRepository repo = new FilmDataRepository();
            return repo.RetrieveAllClients().ToList();
        }
    }
}
