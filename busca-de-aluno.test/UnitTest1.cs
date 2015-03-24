using System.Web.Mvc;
using busca_de_aluno.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace busca_de_aluno.test
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TesteBusca()
        {

            BuscaController buscaController = new BuscaController();

            Assert.IsNotNull(buscaController.Buscar("111024705"));

        }
    }
}
