using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStudentMVC5.Models;
using eStudentMVC5.Business;

namespace eStudentMVC5.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        // http://joelabrahamsson.com/extending-aspnet-mvc-music-store-with-elasticsearch/
        public ActionResult IndexOld()
        {
            Uri node = new Uri("http://localhost:49997");

            ConnectionSettings settings = new ConnectionSettings(
                node,
                defaultIndex: "estudent"
            );

            ElasticClient client = new ElasticClient(settings);

            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            client.CreateIndex(c => c.Index("estudent")
                                 .InitializeUsing(indexSettings)
                                 .AddMapping<uporabnik>(map => map.MapFromAttributes(maxRecursion: 1)));

            List<uporabnik> users = BusinessLogic.vrniVseStudente();
            foreach (uporabnik c in users)
            {
                var result = client.Index(c);
                if (!result.IsValid)
                {
                    Log.Error("Neuspelo indeksiranje.");
                }
            }

            var results = client.Search<uporabnik>(s => s
                //.From(0)
                //.Size(10)
                            .Query(q => q
                                 .Term(p => p.ime, "Matic")
                            )
                        );

            var searchResults = client.Search<uporabnik>(s => s.AllIndices());

            return View("Index");
        }

        public ActionResult Index()
        {
            var node = new Uri("http://localhost:49997");

            var settings = new ConnectionSettings(
                node,
                defaultIndex: "my-application"
            );

            var client = new ElasticClient(settings);


            //List<uporabnik> users = BusinessLogic.vrniVseStudente();
            //foreach (uporabnik u in users)
            //{
            //    var index = client.Index(u);
            //}

            var uporabnik = new uporabnik
            {
                idUporabnik = 99,
                ime = "Tinnchi",
                priimek = "Novak"
            };

            var index = client.Index(uporabnik);
            if (!index.IsValid)
            {
                //Log.Error(index.ServerError.Error);
                Log.Error("Error when indexing.");
            }

            var results = client.Search<uporabnik>(s => s
                //.From(0)
                //.Size(10)
                            .Query(q => q
                                 .Term(p => p.priimek, "Novak")
                            )
                        );

            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel s)
        {

            return RedirectToAction("Index");
        }

        /*
        public ActionResult Search()
        {
            var node = new Uri("http://localhost:49997");

            var settings = new ConnectionSettings(
                node,
                defaultIndex: "my-application"
            );

            var client = new ElasticClient(settings);

            var uporabnik = new uporabnik
            {
                idUporabnik = 99,
                ime = "John",
                priimek = "Schwarz"
            };

            var index = client.Index(uporabnik);
            if (!index.IsValid)
            {
                Log.Error("Error when indexing.");
            }

            var results = client.Search<uporabnik>(s => s.Query(q => q.Term(p => p.priimek, "Novak")));

            return View();
        }
        */
    }
}