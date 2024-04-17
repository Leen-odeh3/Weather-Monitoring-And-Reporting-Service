using Weather_Monitoring_And_Reporting_Service.Subscriber;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service.Publisher
{
    public class WeatherPublisher : IWeatherPublisher
    {
        private bool _isNotified = false;
        private Weather _weatherData;

        public Weather WeatherData
        {
            set
            {
                if (!value.Equals(_weatherData))
                {
                    _weatherData = value;
                    _ = NotifyAsync();
                }
            }
        }

        public List<IWeatherSubscriber> Subscribers { get => _subscribers; set => _subscribers = value; }
        private List<IWeatherSubscriber> _subscribers = new List<IWeatherSubscriber>();

        private Weather newData;
        private ITextFormatStrategy textFormatStrategy;
        private BotConfiguration botConfig;

        public WeatherPublisher(string text, ITextFormatStrategy textFormat, BotConfiguration botConfig)
        {
            InitializeSubscribers(botConfig);
            _weatherData = textFormat.GetWeatherData(text);
            _ = NotifyAsync();
        }

        public WeatherPublisher(Weather newData, ITextFormatStrategy textFormatStrategy, BotConfiguration botConfig)
        {
            this.newData = newData;
            this.textFormatStrategy = textFormatStrategy;
            this.botConfig = botConfig;
        }

        public void InitializeSubscribers(BotConfiguration botConfig)
        {
            Attach(botConfig.RainBot);
            Attach(botConfig.SnowBot);
            Attach(botConfig.SunBot);
        }

        public void Attach(IWeatherSubscriber observer)
        {
            _subscribers.Add(observer);
        }

        public void Detach(IWeatherSubscriber observer)
        {
            _subscribers.Remove(observer);
        }

        public async Task NotifyAsync()
        {
            if (!_isNotified)
            {
                Console.WriteLine("Notifying observers .... ");
                await Task.Delay(500);
                foreach (var subscriber in _subscribers)
                {
                    subscriber.ProcessWeatherUpdate(_weatherData);
                    await Task.Delay(500);
                }
                _isNotified = true;
            }
        }
    }
}
