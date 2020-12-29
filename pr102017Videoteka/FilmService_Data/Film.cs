using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data
{
    public class Film : TableEntity
    {
        public string Godina { get; set; }
        public string Zanr { get; set; }

        public Film (string nazivFilma,string god,string zanrr)
        {
            PartitionKey = "Film";
            RowKey = nazivFilma;
            Godina = god;
            Zanr = zanrr;
        }
        public Film() { }
    }
}
