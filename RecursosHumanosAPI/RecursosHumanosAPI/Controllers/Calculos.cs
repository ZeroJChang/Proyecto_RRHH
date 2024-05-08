namespace RecursosHumanosAPI.Controllers
{
    public class Calculos
    {

        public int CalcularDias(DateTime fechaInicio, DateTime fechaFinal)
        {
            TimeSpan diferencia = fechaFinal - fechaInicio;
            return diferencia.Days;
        }

        public int CalcularDiasBono14(DateTime fechaInicio, DateTime fechaFinal)
        {
            // Validate if fechaInicio is before 01/06/2024
            if (fechaInicio < new DateTime(2023, 6, 1))
            {
                fechaInicio = new DateTime(2023, 6, 1);
                // Calculate the difference in days
                TimeSpan diferencia = fechaFinal - fechaInicio;
                return diferencia.Days;
                
            }
            else
            {

                TimeSpan diferencia = fechaFinal - fechaInicio;
                return diferencia.Days;
            }
        }

        public int CalcularDiasAguinaldo(DateTime fechaInicio, DateTime fechaFinal)
        {
            // Validate if fechaInicio is before 01/06/2024
            if (fechaInicio < new DateTime(2023, 12, 1))
            {
                fechaInicio = new DateTime(2023, 12, 1);
                // Calculate the difference in days
                TimeSpan diferencia = fechaFinal - fechaInicio;
                return diferencia.Days;
            }
            else
            {
                TimeSpan diferencia = fechaFinal - fechaInicio;
                return diferencia.Days;
            }
        }
    }
}
