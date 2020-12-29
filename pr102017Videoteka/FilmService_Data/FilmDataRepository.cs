using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data
{
    public class FilmDataRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        private CloudTable _tableKlijent;
        private CloudTable _tableIznajmljen;
        public FilmDataRepository()
        {
            _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
            Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("FilmTable");
            _table.CreateIfNotExists();

            _tableKlijent = tableClient.GetTableReference("KlijentTable");
            _tableKlijent.CreateIfNotExists();

            _tableIznajmljen = tableClient.GetTableReference("IznajmljenTable");
            _tableIznajmljen.CreateIfNotExists();
        }
        public IQueryable<Film> RetrieveAllFilms()
        {
            var results = from g in _table.CreateQuery<Film>()
                          where g.PartitionKey == "Film"
                          select g;
            return results;
        }
        public void AddFilm(Film f)
        {
            // Samostalni rad: izmestiti tableName u konfiguraciju servisa.
            TableOperation insertOperation = TableOperation.Insert(f);
            
            _table.Execute(insertOperation);
        }        public void DeleteFilm(Film f)
        {
            /*
            TableOperation insertOperation = TableOperation.Delete(f);
            _table.Execute(insertOperation);*/
            _table.Execute(TableOperation.Delete(new TableEntity(f.PartitionKey, f.RowKey) { ETag = "*" }));

        }
        public void UpdateFilm(Film f)
        {
            _table.Execute(TableOperation.Delete(new TableEntity(f.PartitionKey, f.RowKey) { ETag = "*" }));
            TableOperation insertOperation = TableOperation.Insert(f);

            _table.Execute(insertOperation);
        }





        public IQueryable<Klijent> RetrieveAllClients()
        {
            var results = from g in _tableKlijent.CreateQuery<Klijent>()
                          where g.PartitionKey == "Klijent"
                          select g;
            return results;
        }
        public void AddClient(Klijent f)
        {
            // Samostalni rad: izmestiti tableName u konfiguraciju servisa.
            TableOperation insertOperation = TableOperation.Insert(f);

            _tableKlijent.Execute(insertOperation);
        }        public void DeleteClient(Klijent f)
        {
            /*
            TableOperation insertOperation = TableOperation.Delete(f);
            _table.Execute(insertOperation);*/
            _tableKlijent.Execute(TableOperation.Delete(new TableEntity(f.PartitionKey, f.RowKey) { ETag = "*" }));

        }
        public void UpdateClient(Klijent f)
        {
            _tableKlijent.Execute(TableOperation.Delete(new TableEntity(f.PartitionKey, f.RowKey) { ETag = "*" }));
            TableOperation insertOperation = TableOperation.Insert(f);

            _tableKlijent.Execute(insertOperation);
        }


        public IQueryable<IznajmljeniFilm> RetrieveAllRented()
        {
            var results = from g in _tableIznajmljen.CreateQuery<IznajmljeniFilm>()
                          where g.PartitionKey == "Iznajmljeni"
                          select g;
            return results;
        }
        public void AddRented(IznajmljeniFilm f)
        {
           
            TableOperation insertOperation = TableOperation.Insert(f);

            _tableIznajmljen.Execute(insertOperation);
        }        public void DeleteRented(IznajmljeniFilm f)
        {
           
            _tableIznajmljen.Execute(TableOperation.Delete(new TableEntity(f.PartitionKey, f.RowKey) { ETag = "*" }));

        }
        public void UpdateRented(IznajmljeniFilm f)
        {
            _tableIznajmljen.Execute(TableOperation.Delete(new TableEntity(f.PartitionKey, f.RowKey) { ETag = "*" }));
            TableOperation insertOperation = TableOperation.Insert(f);

            _tableIznajmljen.Execute(insertOperation);
        }





    }
}
