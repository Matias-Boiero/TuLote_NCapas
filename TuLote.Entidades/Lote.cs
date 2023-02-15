using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TuLote.Entidades
{

    public class Lote
    {


        public int Id { get; set; }
        [Required(ErrorMessage = "El número del lote es requerido")]
        [Display(Name = "Número de lote")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Los metros cuadrados son requeridos")]
        [Display(Name = "Metros (m2)")]
        public string Metros { get; set; }

        [EnumDataType(typeof(Etapas))]
        [Required(ErrorMessage = "La etapa es requerida")]
        [Display(Name = "Etapa/Área")]
        public Etapas Etapas { get; set; }

        [EnumDataType(typeof(Tipo))]
        [Required(ErrorMessage = "La ubicación es requerida")]
        [Display(Name = "Ubicación")]
        public Tipo Tipo { get; set; }

        [EnumDataType(typeof(Orientaciones))]
        [Required(ErrorMessage = "La orientación es requerida")]
        public Orientaciones Orientacion { get; set; }

        public bool Disponible { get; set; }
        [Required(ErrorMessage = "Por favor cargue el monto")]
        [Display(Name = "Precio")]
        public int Precio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creación del lote")]
        [Required(ErrorMessage = "Especifique una fecha")]
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("Barrio")]
        public int Barrio_Id { get; set; }
        public Barrio Barrio { get; set; }
        [ForeignKey("Usuario")]
        public string Usuario_Id { get; set; }
        public Usuario Usuario { get; set; }
    }



    public enum Orientaciones
    {
        [Display(Name = "NORTE")] N = 1,
        [Display(Name = "SUR")] S = 2,
        [Display(Name = "NOROESTE")] NO = 3,
        [Display(Name = "NORESTE")] NE = 4,
        [Display(Name = "SUDESTE")] SE = 5,
        [Display(Name = "SUDOESTE")] SO = 6,
    }
    public enum Tipo
    {
        [Display(Name = "Interno")] Interno = 1,
        [Display(Name = "Perimetral")] Perimetral = 2,
        [Display(Name = "Laguna")] Laguna = 3,
        [Display(Name = "Rio")] Rio = 4,
        [Display(Name = "Bosque")] Bosque = 5,
        [Display(Name = "Golf")] Golf = 6,
        [Display(Name = "Espejo de agua")] Espejo_de_Agua = 7,
        [Display(Name = "Abierto")] Abierto = 8,
        [Display(Name = "Semicerrado")] Semicerrado = 9
    }

    public enum Etapas
    {
        Sin_Etapa,
        Alameda_1,
        Alameda_2,
        Alameda_3,
        Canton_Golf,
        Canton_Islas,
        Canton_Puerto,
        Canton_Norte,
        ComarcasLujan_La_Elina,
        ComarcasLujan_Santa_Matilde,
        ComarcasLujan_Santa_Ines,
        ComarcasLujan_Santa_Irene,
        ComarcasLujan_San_Roque,
        CamposCardales_Etapa_1,
        CamposCardales_Etapa_2,
        ChacrasDeLaCruz_Area1,
        ChacrasDeLaCruz_Area2,
        ChacrasDeLaCruz_Area3,
        CumbreDeLaRosa_Etapa1,
        CumbreDeLaRosa_Etapa2,
        CumbreDeLaRosa_Etapa3,
        EstanciaVillaMaria_Los_Silos,
        EstanciaVillaMaria_Arroyito,
        EstanciaVillaMaria_La_Esperanza,
        EstanciaVillaMaria_Los_Alamos,
        EstanciaVillaMaria_Manzanar_Chico,
        EstanciaVillaMaria_Casco,
        EstanciaVillaMaria_Lagunas_Del_Golf,
        LaCañada,
        LaCañada_Los_Robles,
        LaCañada_Los_Arces,
        LaCañada_Los_Tilos,
        Naudir_Aguas_Privadas,
        Naudir_Delta,
        SanSebastian_Area_1,
        SanSebastian_Area_2,
        SanSebastian_Area_3,
        SanSebastian_Area_4,
        SanSebastian_Area_5,
        SanSebastian_Area_6,
        SanSebastian_Area_7,
        SanSebastian_Area_8,
        SanSebastian_Area_9,
        SanSebastian_Area_10,
        SanSebastian_Area_11,
        SanSebastian_Area_12,
        SanSebastian_Area_13,
        SanSebastian_Area_13_E2,
        San_Simon_1,
        San_Simon_2,
        SantaIsabel_Etapa_3A,
        SantaIsabel_Etapa_3B,
        Tiempos_de_Canning,
        TiemposCanning_Aeris,
        TiemposCanning_Lacus,
        Tres_Pinos,
        TresPinos_El_Estribo,
        TresPinos_Polo,
        TresPinos_Laguna,
        TresPinos_Norte,
        TresPinos_Casco,
        TresPinos_Golf,
        TresPinos_Isla,
        Etapa_1,
        Etapa_2,
        Etapa_3,
        Etapa_4,
        Etapa_5,
        Area_1,
        Area_2,
        Area_3,
        Area_4,
        Area_5,
        Sector_1,
        Sector_2,
        Sector_3,
        Sector_4,
        Sector_5,
    }
}
