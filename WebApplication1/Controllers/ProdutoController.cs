using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            ProdutosModel mv = new ProdutosModel();
            mv.listCategoria = mv.ListCategoria();
            return View(mv);
        }

        [HttpPost]
        public HttpStatusCode InsertCategoria([FromQuery] String categoria)
        {
            ProdutosModel model = new ProdutosModel();

            return model.InsertCategoria(categoria);

        }

        public IActionResult CadastraProduto(ProdutosModel pModel)
        {

            ProdutosModel mv = new ProdutosModel();
            JsonResult json = mv.AddProduto(pModel);

            return View(json);
        }

    }
}
