using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AwayFromKeyboard.App
{
    public partial class MetaClient : IMetaClient
    {
        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await ModulesAllAsync();
        }

        public async Task<Module> GetModule(Guid moduleId)
        {
            return (await ModulesAllAsync()).Single(m => m.Id == moduleId);
        }

        public async Task AddEntity(CreateType model)
        {
            await Entities3Async(model);
        }
    }

    public interface IMetaClient
    {
        Task<IEnumerable<Module>> GetAllModules();
        Task<Module> GetModule(Guid moduleId);
        Task AddEntity(CreateType model);
    }
}
