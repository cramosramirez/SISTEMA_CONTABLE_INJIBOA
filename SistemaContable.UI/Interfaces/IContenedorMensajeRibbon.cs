using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaContable.UI.Interfaces
{
    public interface IContenedorMensajeRibbon
    {
        Panel PanelMensaje { get; }
        Label LabelMensaje { get; }
    }
}
