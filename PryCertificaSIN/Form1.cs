using PryCertificaSIN.ServiceFacturacionCodigosSIN;
using PryCertificaSIN.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace PryCertificaSIN
{
    public partial class frmMainSIN : Form
    {
        public frmMainSIN()
        {
            InitializeComponent();
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btConectar_Click(object sender, EventArgs e)
        {
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJFbnpvIiwiY29kaWdvU2lzdGVtYSI6IjcyNDE4NDAwQzFFQzI4QjZFODRFMTJGIiwibml0IjoiSDRzSUFBQUFBQUFBQURPMk1EQzB0REF6TURRSEFHNkxrVU1LQUFBQSIsImlkIjo2MjAwNDUsImV4cCI6MTY3MjM1ODQwMCwiaWF0IjoxNjY0ODAzMTMyLCJuaXREZWxlZ2FkbyI6MzgwMTk4NjAxNywic3Vic2lzdGVtYSI6IlNGRSJ9._kO93DpE2118YqOZ8k8ZB8NYU7w5ZjYq5MjY62mP24Rh-7btKca6fGBhwSJWadoNmbSJwLuoLlSWR1-49IP8aA";
            //string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJFbnpvIiwiY29kaWdvU2lzdGVtYSI6IjZEMjQ5Njk0MEFCMzk0QzU3MERBMTJGIiwibml0IjoiSDRzSUFBQUFBQUFBQURPMk1EQzB0REF6TURRSEFHNkxrVU1LQUFBQSIsImlkIjo2MjAwNDUsImV4cCI6MTY0NDUzNzYwMCwiaWF0IjoxNjQ0MTUxMzMyLCJuaXREZWxlZ2FkbyI6MzgwMTk4NjAxNywic3Vic2lzdGVtYSI6IlNGRSJ9.QkqgBdnu3El3ktPg7l05_O0T-P4gkzieiJrj3hHZ87jKZ8uyqySbEOlCXxDnIjflGgI1maTwFlivKQLi6vtbTg";
            string endpointAddress = "https://pilotosiatservicios.impuestos.gob.bo/v2/FacturacionCodigos?wsdl";//"https://pilotosiatservicios.impuestos.gob.bo/v2/FacturacionOperaciones?wsdl";//
            //https://pilotosiatservicios.impuestos.gob.bo/v2/FacturacionCodigos?wsdl
            //string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJFbnpvIiwiY29kaWdvU2lzdGVtYSI6IjZEMjQ5Njk0MEFCMzk0QzU3MERBMTJGIiwibml0IjoiSDRzSUFBQUFBQUFBQURPMk1EQzB0REF6TURRSEFHNkxrVU1LQUFBQSIsImlkIjo2MjAwNDUsImV4cCI6MTY0MzU4NzIwMCwiaWF0IjoxNjQwMjkyMTI5LCJuaXREZWxlZ2FkbyI6MzgwMTk4NjAxNywic3Vic2lzdGVtYSI6IlNGRSJ9.WSJzg9BY7JvLRtApjO86880x8ezJUocNoVHf4U9BvhqujOsKUCy7sWb0Ypp_IgxHbHuios0dRCc9Grb6jPjRIw";
            BasicHttpBinding binding = new BasicHttpBinding
            {
                SendTimeout = TimeSpan.FromSeconds(1000),
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                AllowCookies = true,
                ReaderQuotas = XmlDictionaryReaderQuotas.Max
            };
            binding.Security.Mode = BasicHttpSecurityMode.Transport; // https   apikey: TokenApi   
            //binding.Security.Mode = BasicHttpSecurityMode.None; // http
            EndpointAddress address = new EndpointAddress(endpointAddress);

            ServicioFacturacionCodigosClient servicio = new ServicioFacturacionCodigosClient(binding, address);

            servicio.Endpoint.EndpointBehaviors.Add(new CustomAuthenticationBehaviour($"TokenApi {token}"));  // ($"apikey TokenApi {token}"))
            //servicio.Open();
            try
            {
                Task<verificarComunicacionResponse> resp = servicio.verificarComunicacionAsync();
                //Task<verificarComunicacionResponse> resp = servicio.verificarComunicacionAsync();

                resp.Wait();
                var respuesta = resp.Result.RespuestaComunicacion.ToString();//resp.Result.RespuestaComunicacion.ToString();
                MessageBox.Show(respuesta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("el error es"+ ex.Message);
            }

        }
    }
}
