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
    public class IznajmljeniController : Controller
    {
        private String handlerEndPoint = "InternalRequest";
        public ActionResult Index()
        {
            IReadFilm proxy;
            var binding = new NetTcpBinding();
            ChannelFactory<IReadFilm> factory = new
           ChannelFactory<IReadFilm>(binding, new EndpointAddress("net.tcp://localhost:6000/InputRequest"));
            proxy = factory.CreateChannel();
            ViewBag.iznajmljeni = proxy.DobaviIznajmljene();
            HttpContext.Application["iznajmljeni"] = ViewBag.iznajmljeni;
            return View();
        }


        [HttpPost]
        public ActionResult Iznajmljen(string naziv, string ime, string br)
        {
            IznajmljeniFilm i = new IznajmljeniFilm(naziv, ime, br);
            List<IznajmljeniFilm> iz = (List<IznajmljeniFilm>)HttpContext.Application["iznajmljeni"];

            var binding = new NetTcpBinding();

            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.Roles["WriterWorkerRole"].Instances[0].InstanceEndpoints[handlerEndPoint];
            string endpoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, handlerEndPoint);

            ChannelFactory<IWriteFilm> factory = new ChannelFactory<IWriteFilm>(binding, endpoint);
            IWriteFilm proxy = factory.CreateChannel();
            foreach (IznajmljeniFilm item in iz)
            {
                if (item.RowKey == i.RowKey)
                {
                    proxy.AzurirajIznajmljen(i);
                    return RedirectToAction("Index");

                }
            }
            proxy.UpisiIznajmljen(i);


            return RedirectToAction("Index");
        }
        public ActionResult DeleteIznajmljen(string naziv, string ime, string br)
        {
            IznajmljeniFilm i = new IznajmljeniFilm(naziv, ime, br);


            var binding = new NetTcpBinding();

            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.Roles["WriterWorkerRole"].Instances[2].InstanceEndpoints[handlerEndPoint];
            string endpoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, handlerEndPoint);

            ChannelFactory<IWriteFilm> factory = new ChannelFactory<IWriteFilm>(binding, endpoint);
            IWriteFilm proxy = factory.CreateChannel();
            proxy.ObrisiIznajmljen(i);

            return RedirectToAction("Index");

        }
    }
}