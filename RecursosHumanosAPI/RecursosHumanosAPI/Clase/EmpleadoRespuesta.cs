namespace RecursosHumanosAPI.Clase
{
    public class EmpleadoRespuesta
    {
        public string Nombre { get; set; }
        public decimal SalarioBase { get; set; }
        public DateTime FechaInicioTrabajo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Puesto { get; set; }
        public decimal Bono14 { get; set; }
        public decimal Aguinaldo { get; set;}
        public decimal Vacaciones { get; set; }
        public decimal Indemnizacion { get; set; }
    }
}
