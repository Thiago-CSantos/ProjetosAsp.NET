using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using System.Net;
using System.Text;
using WebApplication1.Connection;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProdutosService
    {

        private DataBaseHelperAbs db { get; }

        private List<SelectListItem> selectListItems { get; set; }

        public ProdutosService(DataBaseHelperAbs pDb)
        {
            this.db = pDb;
        }

        public List<SelectListItem> listCategoria()
        {
            String sql = "SELECT id, categoria FROM categoria_produto";

            selectListItems = new List<SelectListItem>();

            using (DbDataReader reader = db.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    selectListItems.Add(new SelectListItem { Value = reader.GetInt32(0).ToString(), Text = reader.GetString(1) });
                }
            }

            return selectListItems;

        }

        public HttpStatusCode insertCategoria(String categoria)
        {
            String sql = "INSERT INTO categoria_produto(categoria) VALUES(@cat) ";

            db.AddParameter("@cat", categoria);

            if (db.ExecuteNonQuery(sql) > 0)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.Conflict;
        }

        public JsonResult insertProduto(ProdutosModel pModel)
        {
            StringBuilder sql = new StringBuilder()
                .AppendLine("INSERT INTO produto(nome,descricao,sku,quantidade,cod_barra,categoria)")
                .AppendLine("VALUES(@nome,@descricao,@sku,@quantidade,@cod_barra,@categoria)");

            db.AddParameter("@nome", pModel.nome);
            db.AddParameter("@descricao", pModel.descricao);
            db.AddParameter("@sku", pModel.SKU);
            db.AddParameter("@quantidade", pModel.quantidade);
            db.AddParameter("@cod_barra", pModel.codigoBarra);
            db.AddParameter("@categoria", pModel.categoria);

            if (db.ExecuteNonQuery(sql.ToString()) != 0)
            {
                return new JsonResult(new
                {
                    nome = pModel.nome,
                    descricao = pModel.descricao,
                    sku = pModel.SKU,
                    quantidade = pModel.quantidade,
                    cod_barra = pModel.codigoBarra,
                    insertCategoria = pModel.categoria,
                });
            }
            else
            {
                return new JsonResult(null);
            }
        }
    }
}
