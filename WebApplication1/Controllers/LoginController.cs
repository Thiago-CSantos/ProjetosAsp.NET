using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using WebApplication1.Connection;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {



        public IActionResult Index()
        {
            ViewBag.IsLoginPage = true;
            return View("Login");
        }

        [HttpPost]
        public IActionResult ValidaLogin(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                LoginDTO? dto = loginModel.BuscaUsuarioLogin(loginModel.cpf_cnpj);
                if (loginModel.ValidaLogin(dto, loginModel.senha))
                {
                    ViewBag.IsLoginPage = false;
                    return View("../Home/Index");
                }
                else
                {
                    ViewBag.ErrorUsername = "Username ou Senha incorreta";
                    ViewBag.ErrorSenha = "Username ou Senha incorreta";
                }
            }
            else
            {
                ViewBag.ErrorUsername = "Campos invalidos";
            }
            ViewBag.IsLoginPage = true;
            return View("Login");
        }

        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult CreateUsers(LoginModel model)
        {
            LoginModel mv = model;
            if (ModelState.IsValid)
            {
                model.CadastrarUsuario(mv);
            }

            return View("CriarUsuario");
        }
    }
}
