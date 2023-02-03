using TuLote.Entidades;


namespace TuLote.Servicios
{
    public interface IServicio_API_Provincia
    {
        Task<List<Provincia>> Lista();
    }
}
