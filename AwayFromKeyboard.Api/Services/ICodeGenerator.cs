namespace AwayFromKeyboard.Api.Services
{
    public interface ICodeGenerator
    {
        string GenerateCode(Domain.CodeGen.Template template, object model);
    }
}