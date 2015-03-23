using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Threading;

namespace busca_de_aluno.Controllers
{
    public class BuscaController : Controller
    {
        //
        // GET: /Busca/

        public ActionResult Index()
        {
            return View();
        }

        int TotalLinhas(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }

        [HttpPost]
        public ActionResult Buscar(string RA)
        {

            try
            {

                string Url = string.Format(@"https://www.googleapis.com/mapsengine/v1/tables/16301484656389751053-01619059540675406410/features?where=RA_Aluno={0}&version=published&key=AIzaSyAsrcj7OColofVBkQHQOJDL0_dIQxGjyjY", RA);

                HttpClient client = null;

                client = new HttpClient();
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {

                    Newtonsoft.Json.Linq.JContainer registros = response.Content.ReadAsAsync<dynamic>().Result;

                    JValue Jlat = (JValue)registros["features"][0]["properties"]["lat"];
                    JValue Jlng = (JValue)registros["features"][0]["properties"]["lng"];

                    using (FileStream f = new FileStream(this.Server.MapPath("") + "\\lista.txt", FileMode.Append, FileAccess.Write))
                    using (StreamWriter s = new StreamWriter(f))
                        s.WriteLine(RA);

                    return Json(new { lat = Jlat.Value, lng = Jlng.Value }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception)
            {

                return null;

            }

            return null;

        }

        [HttpPost]
        public ActionResult Listar()
        {

            StreamReader sr = new StreamReader(this.Server.MapPath("") + "\\lista.txt");

            List<string> listRAs = new List<string>();

            string linha = sr.ReadLine();

            while (linha != null)
            {

                listRAs.Add(linha);

                linha = sr.ReadLine();

            }

            return Json(new { RAs = listRAs }, JsonRequestBehavior.AllowGet);

        }
    }

}
