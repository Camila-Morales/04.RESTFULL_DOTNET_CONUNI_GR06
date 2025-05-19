namespace WS_Con_Uni_RESTFULL.ec.edu.monster.modelo
{
    public class ConversionUnidadesModelo
    {
        public double ConvertInchesToCm(double inches)
        {
            return inches * 2.54;
        }

        public double ConvertCmToInches(double cm)
        {
            return cm / 2.54;
        }
    }
}
