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
    public class HomeController : Controller
    {
        private String handlerEndPoint = "InternalRequest";
        public ActionResult Index()
        {
            
                IReadFilm proxy;
                var binding = new NetTcpBinding();
                ChannelFactory<IReadFilm> factory = new
               ChannelFactory<IReadFilm>(binding, new EndpointAddress("net.tcp://localhost:6000/InputRequest"));
                proxy = factory.CreateChannel();
                ViewBag.filmovi = proxy.DobaviFilmove();
                HttpContext.Application["filmovi"] = ViewBag.filmovi;
          
            return View();
        }

        [HttpPost]
        public ActionResult Film(string naziv, string godina, string zanr)
        {
            Film f = new Film(naziv,godina, zanr);
            List<Film> filmovi =(List<Film>) HttpContext.Application["filmovi"];
           


            var binding = new NetTcpBinding();
 
            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.Roles["WriterWorkerRole"].Instances[1].InstanceEndpoints[handlerEndPoint];
            string endpoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, handlerEndPoint);

            ChannelFactory<IWriteFilm> factory = new ChannelFactory<IWriteFilm>(binding, endpoint);
            IWriteFilm proxy = factory.CreateChannel();

            foreach (Film item in filmovi)
            {
                if (item.RowKey == f.RowKey)
                {
                    proxy.AzurirajFilm(f);
                    return RedirectToAction("Index");

                }
            }
            proxy.UpisiFilm(f);


            return RedirectToAction("Index");




        }
        public ActionResult DeleteFilm(string naziv, string godina, string zanr)
        {
            Film f = new Film(naziv, godina, zanr);

            var binding = new NetTcpBinding();

            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.Roles["WriterWorkerRole"].Instances[1].InstanceEndpoints[handlerEndPoint];
            string endpoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, handlerEndPoint);

            ChannelFactory<IWriteFilm> factory = new ChannelFactory<IWriteFilm>(binding, endpoint);
            IWriteFilm proxy = factory.CreateChannel();
            proxy.ObrisiFilm(f);

            return RedirectToAction("Index");

        }
    }
}