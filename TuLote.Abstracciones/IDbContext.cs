namespace TuLote.Abstracciones
{
    public interface IDbContext<T> : ICrud<T> where T : class
    {

    }
}
