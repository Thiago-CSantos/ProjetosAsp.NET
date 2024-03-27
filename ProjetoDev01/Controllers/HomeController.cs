using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjetoDev01.Models;
using ProjetoDev1.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ProjetoDev01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempData["menssagemParaView"] != null)
            {
                ViewBag.parametro = TempData["menssagemParaView"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult TestandoPost(IFormCollection dados)
        {
            using (BancoDados db = new BancoDados(
                new DbContextOptionsBuilder<BancoDados>().UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=PROJETO_DEVdb;User Id=postgres;Password=root;").Options)
                )
            {
                if (db.Usuarios.FirstOrDefault(m => m.Email == dados["Email"].ToString()) == null) // Se nenhum elemento atender à condição, ele retornará null
                {
                    Usuario model = new Usuario
                    {
                        Name = dados["Name"],
                        Email = dados["Email"],
                        Senha = dados["Senha"]
                    };

                    db.Usuarios.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("CadUsuario");
                }

            }

            TempData["menssagemParaView"] = "Sucesso";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CadastrarAtividade(IFormCollection dados)
        {
            using (BancoDados db = new BancoDados(
                new DbContextOptionsBuilder<BancoDados>().UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=PROJETO_DEVdb;User Id=postgres;Password=root;").Options)
                )
            {
                string? retencaoObrigatoria = null;
                if (dados["RetencaoSim"] == "Sim")
                {
                     retencaoObrigatoria = dados["RetencaoSim"];
                    }
                else if (dados["RetencaoNao"] == "Nao")
                {
                    retencaoObrigatoria = dados["RetencaoNao"];
                }
                CadastroAtividade model = new CadastroAtividade
                {
                    CodAtividade = dados["CodAtividade"],
                    Grupo = dados["GrupoAtividade"],
                    CodigoFederal = dados["CodFederal"],
                    Descricao = dados["Descricao"],
                    AtivPrevistaExcecao = dados["Excecao"],
                    RetencaoObrigatoria = retencaoObrigatoria,
                    Aliquota = float.Parse(dados["Aliquota"]),
                    Recolhimento = dados["Recolhimento"]

                };
                db.Add(model);
                db.SaveChanges();
            }
            TempData["menssagemParaViewCadAtividade"] = "Sucesso-Atividade";
            return RedirectToAction("CadAtividades");
            // return Json(new { menssage = "Deu certo" });
        }

        public IActionResult CadAtividades()
        {
            if (TempData["menssagemParaViewCadAtividade"] != null)
            {
                ViewBag.parametro = TempData["menssagemParaViewCadAtividade"];
            }
            return View("~/Views/Atividades/CadastrarAtividade.cshtml");
        }

        [HttpGet("CadastrarUsuario")] // dá para chamar pelo href=""
        public IActionResult CadUsuario()
        {   
            return View("~/Views/Home/Cadastrar.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
