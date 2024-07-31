using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Buffers.Text;
using System.Data.Common;
using System.Security.Cryptography;
using WebApplication1.Connection;
using WebApplication1.Models.DTO;
using WebApplication1.Seguranca;

namespace WebApplication1.Services
{
    public class LoginService : CriptografiaInterface
    {
        private String chave = "thiago";
        private DataBaseHelperAbs db { get; }

        public LoginService(DataBaseHelperAbs pDb)
        {
            this.db = pDb;
        }

        public LoginService()
        {
        }

        //public List<LoginDTO> buscaUsuarioLogin()
        //{
        //    List<LoginDTO> listDto = new List<LoginDTO>();

        //    String sql = "SELECT id, username, senha FROM Usuario";

        //    using (DbDataReader reader = db.ExecuteReader(sql))
        //    {
        //        int c;
        //        while (reader.Read())
        //        {
        //            c = 0;

        //            int id = reader.GetInt32(c); c++;
        //            String username = reader.GetString(c); c++;
        //            String senha = reader.GetString(c);

        //            listDto.Add(new LoginDTO(id, username, senha));
        //        }
        //    }
        //    return listDto;
        //}

        public LoginDTO? buscaUsuarioLogin(String? pCpf_cnpj)
        {
            LoginDTO dto;

            String sql = "SELECT id, username, senha FROM Usuario WHERE CPF_CNPJ = @CPF_CNPJ";

            db.AddParameter("@CPF_CNPJ", pCpf_cnpj);

            using (DbDataReader reader = db.ExecuteReader(sql))
            {
                int c;
                while (reader.Read())
                {
                    c = 0;

                    int id = reader.GetInt32(c); c++;
                    String username = reader.GetString(c); c++;
                    String senha = reader.GetString(c);

                    return dto = new LoginDTO(id, username, senha);
                }
            }
            return null;
        }

        public string EncryptSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha); // usando a Biblioteca com BCrypt da interface - CriptografiaInterface
        }

        public bool VerifySenha(string? senha, string senhaBanco)
        {
            try
            {

                byte[] salt = new byte[16];
                int keySizeInBytes = 32; // Tamanho da chave em bytes para AES-256 (256 bits)

                byte[] key = DeriveKey(this.chave, salt, keySizeInBytes);
                byte[] iv = DeriveKey(this.chave, salt, 16);


                CriptografiaAES criptografiaAES = new CriptografiaAES(key, iv);

                byte[] encrypted = criptografiaAES.Encrypt(senhaBanco);
                String decrypted = criptografiaAES.Decrypt(encrypted);

                byte[] senhaInputCrypt = criptografiaAES.Encrypt(senha);

                return decrypted.Equals(Convert.ToBase64String(senhaInputCrypt));
                // return BCrypt.Net.BCrypt.Verify(senha, senhaBanco);
            }
            catch (SaltParseException ex)
            {
                throw new SaltParseException("Senha não está criptografada corretamente: " + ex.Message);
            }
        }

        public String DecryptSenha(String senhaBanco)
        {
            byte[] salt = new byte[16];
            int keySizeInBytes = 32; // Tamanho da chave em bytes para AES-256 (256 bits)

            byte[] key = DeriveKey(this.chave, salt, keySizeInBytes);
            byte[] iv = DeriveKey(this.chave, salt, 16);

            CriptografiaAES criptografiaAES = new CriptografiaAES(key, iv);

            String decrypted = criptografiaAES.Decrypt(Convert.FromBase64String(senhaBanco));


            return decrypted;


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

