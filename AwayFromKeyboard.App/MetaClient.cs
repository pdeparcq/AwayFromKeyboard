using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwayFromKeyboard.App
{
    public partial class MetaClient : IMetaClient
    {
        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await ModulesAllAsync();
        }
    }

    public interface IMetaClient
    {
        Task<IEnumerable<Module>> GetAllModules();
    }
}
