namespace ClothesDataMicroservice.Model.Entities
{
    public class WeatherConditions : Entity
    {
        public double MinTemp { get; private set; }
        public double MaxTemp { get; private set; }

        public bool IsInadvisableForRain { get; private set; }
        public bool IsInadvisableForSnow { get; private set; }

        public WeatherConditions(long id): base(id)
        {

        }
        public WeatherConditions(long id, double minTemp, double maxTemp, bool snow, bool rain) : base(id)
        {
            this.MinTemp = minTemp;
            this.MaxTemp = maxTemp;
            this.IsInadvisableForRain = rain;
            this.IsInadvisableForSnow = snow;

        }
        public bool IsTemperatureBetweenMinMax(double temperature)
        {
            return MinTemp <= temperature && MaxTemp >= temperature;
        }

        public bool IsSuitableOnThisWeather(bool isSnowy, bool isRainy)
        {
            if ((isSnowy && IsInadvisableForSnow) || (isRainy && IsInadvisableForRain))
            {
                return false;
            }
            return true;
        }
    }
}
