namespace WebApplication1.Seguranca
{
    /// <summary>
    /// Usando a biblioteca que foi achada no GitHub BCrypt.Net-Core
    /// <url>https://github.com/neoKushan/BCrypt.Net-Core?tab=readme-ov-file</url>
    /// </summary>
    public interface CriptografiaInterface
    {
        // hash and save a password
        /// <summary>
        /// Deve ser implementado da seguinte forma BCrypt.Net.BCrypt.HashPassword(senha);
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        String EncryptSenha(String senha);

        // check a password
        /// <summary>
        /// Deve ser implementado da seguinte forma BCrypt.Net.BCrypt.Verify(senha, senhaBanco);
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="senhaBanco"></param>
        /// <returns>Boolean</returns>
        Boolean VerifySenha(String senha, String senhaBanco);
    }
}
