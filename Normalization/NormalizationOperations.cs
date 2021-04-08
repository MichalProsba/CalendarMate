using System;

namespace Normalization
{
    public static class NormalizationOperations
    {
        public static double NormalizeTemperature(double kelvin)
        {
            double celsius = Math.Round((kelvin - 273.15), 2);
            return celsius;
        }
        public static string NormalizeDate(double secondsToAdd)
        {
            DateTime primaryDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            primaryDate = primaryDate.AddSeconds(secondsToAdd);
            return primaryDate.Date.ToString("d");
        }
    }
}
