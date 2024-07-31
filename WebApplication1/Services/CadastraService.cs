using System.Security.Cryptography;
using WebApplication1.Connection;
using WebApplication1.Seguranca;

namespace WebApplication1.Services
{
    public class CadastraService : CriptografiaInterface
    {
        private DataBaseHelperAbs db { get; }

        public CadastraService(DataBaseHelperAbs pDb)
        {
            db = pDb;
        }

        public bool cadastraUsuario(String? cnpj_cpnf, String? username, String senha, String? nome, String? roles)
        {
            try
            {
                String senhaCriptografada = EncryptSenha(senha);
                String sql = "INSERT INTO Usuario(cpf_cnpj, username, senha, nome, roles) VALUES(@CNPJ_CPF, @USERNAME, @SENHA, @NOME, @ROLES)";

                db.AddParameter("@CNPJ_CPF", cnpj_cpnf);
                db.AddParameter("@USERNAME", username);
                db.AddParameter("@SENHA", senhaCriptografada);
                db.AddParameter("@NOME", nome);
                db.AddParameter("@ROLES", roles);

                int linhasAfetadas = db.ExecuteNonQuery(sql);
                if (linhasAfetadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public string EncryptSenha(string senha)
        {

            String chave = "thiago";
            byte[] salt = new byte[16];
            int keySizeInBytes = 32; // Tamanho da chave em bytes para AES-256 (256 bits)

            byte[] key = DeriveKey(chave, salt, keySizeInBytes);
            byte[] iv = DeriveKey(chave, salt, 16);

            CriptografiaAES criptografiaAES = new CriptografiaAES(key, iv);

            byte[] encrypted = criptografiaAES.Encrypt(senha);

            return Convert.ToBase64String(encrypted);

            //return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerifySenha(string senha, string senhaBanco)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaBanco);
        }

        public byte[] DeriveKey(string password, byte[] salt, int keySizeInBytes)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000)) // 10000 é o número de iterações
            {
                return deriveBytes.GetBytes(keySizeInBytes);
            }
        }
    }
}
