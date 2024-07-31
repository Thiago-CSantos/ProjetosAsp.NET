using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using WebApplication1.Connection;
using WebApplication1.Services;

namespace WebApplication1.Models
{
    public class ProdutosModel
    {
        public String? nome { get; set; }
        public String? descricao { get; set; }
        public Double preco { get; set; }
        public String? codigoBarra { get; set; }
        public String? categoria { get; set; }
        public int quantidade { get; set; }
        public String? SKU { get; set; }

        public List<SelectListItem>? listCategoria { get; set; }

        public ProdutosModel(string nome, string descricao, double preco, string codigoBarra, string categoria, String SKU, int quantidade)
        {
            this.nome = nome;
            this.descricao = descricao;
            this.preco = preco;
            this.codigoBarra = codigoBarra;
            this.categoria = categoria;
            this.SKU = SKU;
            this.quantidade = quantidade;
        }

        public ProdutosModel()
        {
        }
        public List<SelectListItem> ListCategoria()
        {
            using (DataBaseHelperAbs db = DatabaseHelper.GetProvider())
            {
                ProdutosService service = new ProdutosService(db);
                return service.listCategoria();
            }
        }

        public HttpStatusCode InsertCategoria(String categoria)
        {

            using (DataBaseHelperAbs db = DatabaseHelper.GetProvider())
            {
                ProdutosService service = new ProdutosService(db);
                service.insertCategoria(categoria);
            }

            return HttpStatusCode.OK;
        }


        public JsonResult AddProduto(ProdutosModel pModel)
        {
            using (DataBaseHelperAbs db = DatabaseHelper.GetProvider())
            {
                ProdutosService service = new ProdutosService(db);
                return service.insertProduto(pModel);
                
                    }
        }
    }
}
