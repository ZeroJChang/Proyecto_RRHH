namespace RecursosHumanosAPI.Clase
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public string NIT { get; set; }
        public string DPI { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal Bono { get; set; }
        public DateTime FechaInicioTrabajo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Direccion { get; set; }
        public string Area { get; set; }
        public string Puesto { get; set; }
    }

}
