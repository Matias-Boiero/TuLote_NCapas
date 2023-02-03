
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TuLote.Entidades;

namespace TuLote.Servicios
{
    public class Servicio_API_Provincia : IServicio_API_Provincia
    {
        private static string _email;
        private static string _clave;
        private static string _baseUrl;
        private static string _token;

        public Servicio_API_Provincia()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // _email = builder.GetSection("ApiSetting:email").Value;
            //_clave = builder.GetSection("ApiSetting:clave").Value;
            _baseUrl = builder.GetSection("ServicesUrl:Provincias").Value;

        }

        //USAR REFERENCIAS 
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

        public async Task<List<Provincia>> Lista()
        {
            // List<Rootobject> lista = new List<Rootobject>();
            //Provincia provincias = new Provincia();
            List<Provincia> provincias = new List<Provincia>();

            // await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("/georef/api/provincias");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Provincia>(json_respuesta);

                foreach (var provincia in result.Provincias)
                {
                    provincias.Add(provincia);
                    //Console.WriteLine(provincia.ToString());
                }
                //result.provincias.ToString();

            }



            return provincias;


        }

        //public async Task<Provincia> Obtener(int idProvincia)
        //{
        //    Provincia objeto = new Provincia();

        //    await Autenticar();


        //    var cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri(_baseUrl);
        //    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        //    var response = await cliente.GetAsync($"/api/provincias/{idProvincia}");

        //    if (response.IsSuccessStatusCode)
        //    {

        //        var json_respuesta = await response.Content.ReadAsStringAsync();
        //        var resultado = JsonConvert.DeserializeObject<Provincia>(json_respuesta);
        //        objeto = resultado;
        //    }

        //    return objeto;
        //}

        //public async Task<bool> Guardar(Provincia objeto)
        //{
        //    bool respuesta = false;

        //    await Autenticar();


        //    var cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri(_baseUrl);
        //    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        //    var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

        //    var response = await cliente.PostAsync("/api/provincias", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        respuesta = true;
        //    }

        //    return respuesta;
        //}

        //public async Task<bool> Editar(int idProvincia, Provincia objeto)
        //{
        //    bool respuesta = false;

        //    await Autenticar();


        //    var cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri(_baseUrl);
        //    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        //    var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

        //    var response = await cliente.PutAsync($"/api/provincias/{idProvincia}", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        respuesta = true;
        //    }

        //    return respuesta;
        //}

        //public async Task<bool> Eliminar(int idProvincia)
        //{
        //    bool respuesta = false;

        //    await Autenticar();


        //    var cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri(_baseUrl);
        //    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


        //    var response = await cliente.DeleteAsync($"/api/provincias/{idProvincia}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        respuesta = true;
        //    }

        //    return respuesta;
        //}

    }
}
