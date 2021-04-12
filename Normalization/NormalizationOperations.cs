using System;

namespace Normalization
{
    // The NormalizationOperations class containes methods to normalize data downloaded from the Weather API
    /// <summary>
    /// The <c>NormalizationOperations</c> class.
    /// Containes all methods necessary to store the current weather information that we download from the Weather API (openweathermap.org).
    /// </summary>
    public static class NormalizationOperations
    {
        // Converts temperature from kelvin to celsius
        /// <summary>
        /// Converts temperature from kelvin to celsius.
        /// </summary>
        /// <param name="kelvin">Double temperature value in kelvin degrees.</param>
        /// <returns>Temperature in celsius degrees.</returns>
        public static double NormalizeTemperature(double kelvin)
        {
            double celsius = Math.Round((kelvin - 273.15), 2);
            return celsius;
        }

        // Converts time from seconds from 1.1.1970 to DateTime format and then to string
        /// <summary>
        /// Converts time from seconds from 1.1.1970 to DateTime format and then to string.
        /// </summary>
        /// <param name="secondsToAdd">Double seconds value from 1.1.1970.</param>
        /// <returns>Date as a string.</returns>
        public static string NormalizeDate(double secondsToAdd)
        {
            DateTime primaryDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            primaryDate = primaryDate.AddSeconds(secondsToAdd);
            return primaryDate.Date.ToString("d");
        }
    }
}
