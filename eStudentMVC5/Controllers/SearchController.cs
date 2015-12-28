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
        estudentEntities db = new estudentEntities();

        // GET: Search
        public ActionResult Index()
        {
            return View(new SearchModel());
        }

        [HttpPost]
        public ActionResult Index(SearchModel querySM)
        {
            SearchModel sm = poisciNizBaza(querySM.query);
            sm.query = querySM.query;
            return View(sm);
        }

        public SearchModel poisciNizBaza(string q)
        {
            List<uporabnik> st = (from s in db.uporabnik where ((s.ime.Equals(q) || s.priimek.Equals(q) || s.email.Equals(q) || s.vpisnaStevilka.ToString().Equals(q)) && s.idVloge == 1) select s).ToList();
            List<uporabnik> z = (from s in db.uporabnik where ((s.ime.Equals(q) || s.priimek.Equals(q) || s.email.Equals(q) || s.vpisnaStevilka.ToString().Equals(q)) && s.idVloge == 2) select s).ToList();
            List<predmet> p = (from s in db.predmet where (s.imePredmeta.Equals(q)) select s).ToList();
            List<izpitnirok> i = (from s in db.izpitnirok where (s.prostor.Equals(q)) select s).ToList();

            SearchModel sm = new SearchModel(st, z, p, i);
            return sm;
        }
        

        /// <summary>
        /// Iskanje sem poskusal implementirati s knjiznico NEST, vendar zaenkrat ne deluje.
        /// 
        /// http://nest.azurewebsites.net/
        /// https://nest.azurewebsites.net/nest/quick-start.html
        /// http://nest.azurewebsites.net/nest/core/
        /// https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/_nest.html
        /// http://joelabrahamsson.com/extending-aspnet-mvc-music-store-with-elasticsearch/
        /// https://www.devbridge.com/articles/getting-started-with-elastic-using-net-nest-library-part-two/
        /// https://github.com/elastic/elasticsearch-net-example/blob/master/src/NuSearch.Indexer/Program.cs
        /// </summary>
        /// <returns></returns>
        public ActionResult Nest()
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

            var tmp = new uporabnik
            {
                idUporabnik = 99,
                ime = "Tinnchi",
                priimek = "Novak"
            };

            var index = client.Index(tmp);
            if (!index.IsValid)
            {
                //Log.Error(index.ServerError.Error);
                Log.Error("Error when indexing.");
            }

            var results = client.Search<uporabnik>(s => s
                .From(0)
                .Size(10)
                            .Query(q => q
                                 .Term(p => p.priimek, "Novak")
                            )
                        );

            return View();
        }

        public ActionResult NestExtended()
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
                .From(0)
                .Size(10)
                            .Query(q => q
                                 .Term(p => p.ime, "Matic")
                            )
                        );

            var searchResults = client.Search<uporabnik>(s => s.AllIndices());

            return View("Index");
        }
    }
}