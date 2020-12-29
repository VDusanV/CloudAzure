using FilmService_Data;
using FilmService_Data.Interfejsi;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace WebRole.Controllers
{
    public class KlijentController : Controller
    {
        private String handlerEndPoint = "InternalRequest";

        public ActionResult Index()
        {
            IReadFilm proxy;
            var binding = new NetTcpBinding();
            ChannelFactory<IReadFilm> factory = new
           ChannelFactory<IReadFilm>(binding, new EndpointAddress("net.tcp://localhost:6000/InputRequest"));
            proxy = factory.CreateChannel();
            ViewBag.klijenti = proxy.DobaviKlijente();
            HttpContext.Application["klijenti"] = ViewBag.klijenti;
            return View();
        }

        [HttpPost]
        public ActionResult Klijent(string br,string ime,string prezime)
        {
            Klijent k = new Klijent(br, ime, prezime);
            List<Klijent> kl = (List<Klijent>)HttpContext.Application["klijenti"];


            var binding = new NetTcpBinding();

            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.Roles["WriterWorkerRole"].Instances[1].InstanceEndpoints[handlerEndPoint];
            string endpoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, handlerEndPoint);

            ChannelFactory<IWriteFilm> factory = new ChannelFactory<IWriteFilm>(binding, endpoint);
            IWriteFilm proxy = factory.CreateChannel();
            foreach (Klijent item in kl)
            {
                if (item.RowKey == k.RowKey)
                {
                    proxy.AzurirajKlijenta(k);
                    return RedirectToAction("Index");

                }
            }
            proxy.UpisiKlijenta(k);


            return RedirectToAction("Index");
        }
        public ActionResult DeleteKlijent(string br, string ime, string prezime)
        {
            Klijent k = new Klijent(br, ime, prezime);

            var binding = new NetTcpBinding();

            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.Roles["WriterWorkerRole"].Instances[1].InstanceEndpoints[handlerEndPoint];
            string endpoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, handlerEndPoint);

            ChannelFactory<IWriteFilm> factory = new ChannelFactory<IWriteFilm>(binding, endpoint);
            IWriteFilm proxy = factory.CreateChannel();
            proxy.ObrisiKlijenta(k);

            return RedirectToAction("Index");

        }
    }
}