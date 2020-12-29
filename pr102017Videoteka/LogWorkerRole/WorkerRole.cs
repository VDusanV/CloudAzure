using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FilmService_Data;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace LogWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        LogProvider lp = new LogProvider();
        CloudQueue cloudQueue;
        CloudQueueMessage receivedMessage;
        BlobHelper blobHelper;


        public override void Run()
        {
            Trace.TraceInformation("LogWorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            blobHelper = new BlobHelper();
            cloudQueue = QueueHelper.GetQueueReference("queue");

            Trace.TraceInformation("LogWorkerRole has been started");
            lp.Open();
            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("LogWorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("LogWorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                receivedMessage = cloudQueue.GetMessage();
                if (receivedMessage != null)
                {
                    try
                    {
                        string s = "";
                        s = blobHelper.DownloadFromBlob("blob");
                        s = s + "..........\n" + receivedMessage.AsString;

                        blobHelper.UploadToBlob("blob", s);
                        Trace.WriteLine(blobHelper.DownloadFromBlob("blob"));
                        cloudQueue.DeleteMessage(receivedMessage);
                    }
                    catch { }
                }

                Trace.TraceInformation("Working");
                await Task.Delay(2000);
            }
        }
    }
}
