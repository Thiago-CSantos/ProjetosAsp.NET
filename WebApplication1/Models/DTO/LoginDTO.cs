namespace WebApplication1.Models.DTO
{
    /// <summary>
    /// DTO de Login
    /// id, username e senha
    /// </summary>
    /// <param name="id"></param>
    /// <param name="username"></param>
    /// <param name="senha"></param>
    public record LoginDTO(int id, String username, String senha);

}
