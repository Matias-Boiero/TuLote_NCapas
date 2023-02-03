
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TuLote.Entidades;


namespace TuLote.Servicios
{
    public class Servicio_API_Municipio : IServicio_API_Municipio
    {
        private static string _email;
        private static string _clave;
        private static string _baseUrl;
        private static string _token;

        public Servicio_API_Municipio()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //_email = builder.GetSection("ApiSetting:email").Value;
            //_clave = builder.GetSection("ApiSetting:clave").Value;
            _baseUrl = builder.GetSection("ServicesUrl:Provincias").Value;

        }

        ////USAR REFERENCIAS 
        //public async Task Autenticar()
        //{


        //    var cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri(_baseUrl);

        //    var credenciales = new Credencial() { Email = _email, Contraseña = _clave };


        //    var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
        //    var response = await cliente.PostAsync("api/Auth/login", content);
        //    var json_respuesta = await response.Content.ReadAsStringAsync();

        //    var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);
        //    _token = resultado.token;
        //}

        public async Task<List<Municipio>> Lista()
        {
            List<Municipio> lista = new List<Municipio>();

            //await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync(" https://apis.datos.gob.ar/georef/api/municipios?provincia=buenos aires&max=300"); //ver

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Municipio>(json_respuesta);
                foreach (var municipios in resultado.Municipios)
                {
                    lista.Add(municipios);
                    //Console.WriteLine(provincia.ToString());
                }

            }

            return lista;
        }

    }
}
