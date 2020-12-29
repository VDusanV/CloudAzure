using FilmService_Data;
using FilmService_Data.Interfejsi;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriterWorkerRole
{
    public class WriteFilmImplementacija : IWriteFilm
    {
        public void AzurirajFilm(Film f)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.UpdateFilm(f);
            string s = "Azuriram film " + f.RowKey + " " + f.Godina + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));

        }

        public void AzurirajIznajmljen(IznajmljeniFilm i)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.UpdateRented(i);
            string s = "Azuriram iznajmljen film " + i.RowKey + " " + i.imeKlijenta + " " + i.brojTelefonaKlijenta + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void AzurirajKlijenta(Klijent k)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.UpdateClient(k);
            string s = "Azuriram klijenta " + k.RowKey + " " + k.ime + " " + k.prezime + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void ObrisiFilm(Film f)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.DeleteFilm(f);
            string s = "Brisem film " + f.RowKey + " " + f.Godina + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void ObrisiIznajmljen(IznajmljeniFilm i)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.DeleteRented(i);
            string s = "Brisem iznajmljen film " + i.RowKey + " " + i.imeKlijenta + " " + i.brojTelefonaKlijenta + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void ObrisiKlijenta(Klijent k)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.DeleteClient(k);
            string s = "Brisem klijenta " + k.RowKey + " " + k.ime + " " + k.prezime + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void UpisiFilm(Film f)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.AddFilm(f);
            string s = "Dodajem film " + f.RowKey + " " + f.Godina + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void UpisiIznajmljen(IznajmljeniFilm i)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.AddRented(i);
            string s = "Dodajem iznajmljen film " + i.RowKey + " " + i.imeKlijenta + " " + i.brojTelefonaKlijenta  + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }

        public void UpisiKlijenta(Klijent k)
        {
            FilmDataRepository repo = new FilmDataRepository();
            repo.AddClient(k);
            string s = "Dodajem klijenta " + k.RowKey + " " + k.ime + " " + k.prezime + " " + DateTime.Now.ToString();
            CloudQueue queue = QueueHelper.GetQueueReference("queue");
            queue.AddMessage(new CloudQueueMessage(s));
        }
    }
}
