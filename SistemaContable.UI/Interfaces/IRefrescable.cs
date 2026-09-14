using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContable.UI.Interfaces
{
    /// <summary>
    /// Formularios que implementan esta interfaz serán refrescados
    /// automáticamente al ser reactivados desde el menú principal.
    /// </summary>
    public interface IRefrescable
    {
        void Refrescar();
    }
}
