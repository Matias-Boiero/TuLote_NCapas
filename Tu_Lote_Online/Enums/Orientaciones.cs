using Microsoft.EntityFrameworkCore;

namespace TuLote.Enums
{
    [Keyless]
    public class Orientacion
    {
        public enum Orientaciones
        {
            N,
            S,
            NO,
            NE,
            SE,
            SO
        }
    }


}
