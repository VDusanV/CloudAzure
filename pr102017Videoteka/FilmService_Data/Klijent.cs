using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data
{
    public class Klijent : TableEntity
    {
        public string ime { get; set; }
        public string  prezime { get; set; }

        public Klijent(string brojTelefona, string imee,string prezimee)
        {
            PartitionKey = "Klijent";
            RowKey = brojTelefona;
            ime = imee;
            prezime = prezimee;
        }
        public Klijent()
        {

        }
    }
}
