using SharedModels;

namespace Weather
{
    public interface IWheatherService
    {
        public IEnumerable<WeatherForecast> Get(int count = 5);
    }
}
