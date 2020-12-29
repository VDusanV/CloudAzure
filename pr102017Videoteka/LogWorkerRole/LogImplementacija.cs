using FilmService_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogWorkerRole
{
    public class LogImplementacija : ILogcs
    {
        BlobHelper blobHelper = new BlobHelper();

        public string SviLogovi()
        {
            string str = "";
            str = blobHelper.DownloadFromBlob("blob");
            return str;
        }
    }
}
