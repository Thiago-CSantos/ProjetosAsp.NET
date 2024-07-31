using System.ComponentModel.DataAnnotations;
using WebApplication1.Connection;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Enums;
using WebApplication1.Services;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        public int id { get; set; } // auto_increment

        [Required(ErrorMessage = Mensagens.Required)]
        [MaxLength(14, ErrorMessage = Mensagens.MaxLength)] // 11 sem mascara e 14 com mascara
        public String? cpf_cnpj { get; set; }

        //[Required(ErrorMessage = Mensagens.Required)]
        [MaxLength(255, ErrorMessage = Mensagens.MaxLength)]
        public String? username { get; set; }

        [Required(ErrorMessage = Mensagens.Required)]
        [MaxLength(255, ErrorMessage = Mensagens.MaxLength)]
        [DataType(DataType.Password)]
        public String? senha { get; set; }

        //[Required(ErrorMessage = Mensagens.Required)]
        [MaxLength(100, ErrorMessage = Mensagens.MaxLength)]
        [DataType(DataType.Text)]
        public String? nome { get; set; }

        //[Required(ErrorMessage = Mensagens.Required)]
        [MaxLength(30, ErrorMessage = Mensagens.MaxLength)]
        public String? roles { get; set; }

        //[Required(ErrorMessage = Mensagens.Required)]
        public short ativo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dt_criacao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dt_alteracao { get; set; }

        public LoginDTO? BuscaUsuarioLogin(String? cpf_cnpj)
        {
            using (DataBaseHelperAbs db = DatabaseHelper.GetProvider())
            {
                LoginService loginService = new LoginService(db);
                return loginService.buscaUsuarioLogin(cpf_cnpj);
            }
        }

        public Boolean ValidaLogin(LoginDTO? dto, String? senhaInput)
        {

            if (dto != null)
            {

                using (DataBaseHelperAbs db = DatabaseHelper.GetProvider())
                {
                    LoginService service = new LoginService(db);
                    return service.VerifySenha(senhaInput, dto.senha);
                }
            }
            return false;

        }

        public Boolean CadastrarUsuario(LoginModel mv)
        {
            try
            {
                using (DataBaseHelperAbs db = DatabaseHelper.GetProvider())
                {
                    CadastraService service = new CadastraService(db);
                    // remove espaços
                    return service.cadastraUsuario(mv.cpf_cnpj?.Trim(), mv.username?.Trim(), mv.senha?.Trim(), mv.nome, mv.roles);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
