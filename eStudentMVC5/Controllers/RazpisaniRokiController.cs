using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStudentMVC5.Controllers
{
    [Authorize]
    public class RazpisaniRokiController : Controller
    {
        estudentEntities db = new estudentEntities();

        // http://joelabrahamsson.com/extending-aspnet-mvc-music-store-with-elasticsearch/
        public ActionResult ReIndex()
        {
            Uri myUri = new Uri("http://localhost");
            var setting = new ConnectionSettings(myUri, "49997");
            var client = new ElasticClient(setting);

            foreach (var u in db.predmet)
            {
                try
                {
                    client.Index(u);
                    //client.Index(u, System.Func<Nest.IndexDescriptor<u.ime>,Nest.IndexDescriptor<u.idUporabnik>>);
                }
                catch
                {
                    Log.Error("Nocem indeksirat. :(");
                }
            }

            return RedirectToAction("Index");
        }

       
        public ActionResult Search(string q)
        {
            var result = ElasticClient.Search<uporabnik>(body =>
                body.Query(query =>
                query.QueryString(qs => qs.Query(q))));

            var genre = new uporabnik()
            {
                ime = "Search results for " + q,
                predmet = ResultExecutedContext.Documents.ToList()

            };

            return View("Browse", genre);
        }

        private static ElasticClient ElasticClient
        {
            get
            {
                Uri myUri = new Uri("http://localhost");
                var setting = new ConnectionSettings(myUri, "49997");
                setting.SetDefaultIndex("musicstore");
                return new ElasticClient(setting);
            }
        }


        // GET: RazpisaniRoki
        public ActionResult Index()
        {
            try
            {
                List<izpitnirok> izpitniRoki = (from s in db.izpitnirok select s).ToList();
                ViewBag.izpitniRoki = izpitniRoki;
            }
            catch
            {
                Log.Error("Napaka pri pripravi izpitnih rokov.");
            }

            return View();
        }

        public ActionResult IzbrisiRok(int idRoka)
        {
            try
            {
                izpitnirok temp = db.izpitnirok.Find(idRoka);
                db.izpitnirok.Remove(temp);
                db.SaveChanges();
            }
            catch
            {
                Log.Error("Napaka pri izbrisu izpitnega roka.");
            }
            return RedirectToAction("Index");
        }
    }
}