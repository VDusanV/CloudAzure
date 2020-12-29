using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data
{
    public class IznajmljeniFilm : TableEntity
    {
        public string imeKlijenta { get; set; }
        public string  brojTelefonaKlijenta { get; set; }

        public IznajmljeniFilm(string nazivFilma,string imeK,string brojK)
        {
            PartitionKey = "Iznajmljeni";
            RowKey = nazivFilma;
            imeKlijenta = imeK;
            brojTelefonaKlijenta = brojK;
        }
        public IznajmljeniFilm()
        {

        }
    }
}
