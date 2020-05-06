using System.Collections.Generic;

namespace Heldu.Logic.Interfaces
{
    public interface IHelperService
    {
        public List<int> RandomProductos(int largo);
        public void EnviarEmail(string asunto, string mensaje);
    }
}
