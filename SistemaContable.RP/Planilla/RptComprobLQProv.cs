using DevExpress.XtraReports.UI;
using SistemaContable.DAL;

namespace SistemaContable.RP.Planilla
{
    public partial class RptComprobLQProv : XtraReport
    {
        private readonly DALBase _dal = new DALBase();
        private readonly int _idZafra;
        private readonly int _idCatorcena;
        private readonly int _idTipoPlanilla;
        private readonly int _codigo;
        private readonly int? _esPrueba;

        public RptComprobLQProv(int idZafra, int idCatorcena, int idTipoPlanilla,
            int codigo, int? esPrueba = null)
        {
            _idZafra = idZafra;
            _idCatorcena = idCatorcena;
            _idTipoPlanilla = idTipoPlanilla;
            _codigo = codigo;
            _esPrueba = esPrueba;
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            DataSource = _dal.EjecutarConsulta("[ECOMPROB].[RPT_COMPROB_LIQUIDACION_EMAIL_PROV]", new
            {
                ID_ZAFRA = _idZafra,
                ID_CATORCENA = _idCatorcena,
                ID_TIPO_PLANILLA = _idTipoPlanilla,
                CODIGO = _codigo,
                ES_PRUEBA = _esPrueba
            });
            DataMember = string.Empty;
        }
    }
}
