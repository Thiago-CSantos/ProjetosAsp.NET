namespace WebApplication1.Models.Enums
{
    public interface Mensagens
    {
        public const String Required = "{0} é um campo obrigatório.";
        public const String Range = "{0} deve estar entre {1} e {2}.";
        public const String MinLength = "{0} deve ter no mínimo {1} caracteres.";
        public const String MaxLength = "{0} deve ter no máximo {1} caracteres.";
        public const String EmailAddress = "{0} deve ser um e-mail válido.";
    }
}
