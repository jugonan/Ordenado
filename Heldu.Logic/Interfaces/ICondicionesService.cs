using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heldu.Entities.Models;

namespace Heldu.Logic.Interfaces
{
    public interface ICondicionesService
    {
        public Task<List<Condicion>> GetCondiciones();
        public Task<Condicion> GetCondicionById(int? id);
        public Task CreateCondicion(Condicion condicion);
        public Task<Condicion> EditCondicionGet(int? id);
        public Task EditCondicionPost(Condicion condicion);
        public Task DeleteCondicionPost(int? id);
        public bool ExistCondicion(int id);
    }
}
