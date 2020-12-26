using HandlebarsDotNet;

namespace AwayFromKeyboard.Api.Services
{
    public class CodeGenerator : ICodeGenerator
    {
        public string GenerateCode(Domain.CodeGen.Template template, object model)
        {
            string generated;
            // Compile and run template
            try
            {
                generated = Handlebars.Compile(template.Value)(model);
            }
            catch (HandlebarsException ex)
            {
                generated = $"Code generation failed with error: {ex.Message}";
            }

            return generated;
        }
    }
}
