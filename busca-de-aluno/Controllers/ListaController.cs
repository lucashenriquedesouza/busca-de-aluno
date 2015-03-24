using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace busca_de_aluno.Controllers
{
    public class ListaController : Controller
    {
        //
        // GET: /List/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Listar(string RA)
        {

            if (!RA.Equals(""))
            {
                using (FileStream f = new FileStream(HttpRuntime.AppDomainAppPath + "\\lista.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter s = new StreamWriter(f))
                    s.WriteLine(RA);
            }

            FileInfo fi = new FileInfo(HttpRuntime.AppDomainAppPath + "\\lista.txt");
            StreamReader sr;
            
            if (!fi.Exists)
                sr = new StreamReader(fi.Create());
            else
                sr = new StreamReader(HttpRuntime.AppDomainAppPath + "\\lista.txt");

            List<string> listRAs = new List<string>();

            string linha = sr.ReadLine();

            while (linha != null)
            {

                listRAs.Add(linha);

                linha = sr.ReadLine();

            }

            sr.Close();

            return Json(new { RAs = listRAs.Reverse<string>().Take<string>(10) }, JsonRequestBehavior.AllowGet);

        }

    }
}
