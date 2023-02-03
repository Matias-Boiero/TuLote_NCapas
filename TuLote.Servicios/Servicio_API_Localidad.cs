using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TuLote.Entidades;

namespace TuLote.Servicios
{
    public class Servicio_API_Localidad : IServicio_API_Localidad
    {
        private static string _email;
        private static string _clave;
        private static string _baseUrl;
        private static string _token;



        public Servicio_API_Localidad()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ServicesUrl:Provincias").Value;
        }

        public async Task<List<Localidad>> Lista()
        {
            //var settings = new JsonSerializerSettings();

            List<Localidad> lista = new List<Localidad>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("https://apis.datos.gob.ar/georef/api/localidades?provincia=Buenos%20aires&max=900&exacto=true");
            //

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Localidad>(json_respuesta);
                foreach (var localidades in resultado.localidades)
                {
                    lista.Add(localidades);
                }
            }
            return lista;
        }
    }
}
