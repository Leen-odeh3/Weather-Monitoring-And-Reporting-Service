
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Publisher;
using Weather_Monitoring_And_Reporting_Service.Strategies;
using Weather_Monitoring_And_Reporting_Service.Subscriber;
using Weather_Monitoring_And_Reporting_Service.WeatherBot;

namespace Weather_Monitoring_And_Reporting_Service_Tests.PublisherTest;

public class WeatherPublisherTest
{
    private readonly IFixture fixture;

    public WeatherPublisherTest()
    {
        fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    private WeatherPublisher CreateWeatherDataPublisher()
    {
        var mockTextFormatStrategy = new Mock<ITextFormatStrategy>();
        var text = fixture.Create<string>();
        var botConfig = fixture.Create<BotConfiguration>();

        mockTextFormatStrategy.Setup(x => x.GetWeatherData(text)).Returns(fixture.Create<Weather>());

        return new WeatherPublisher(text, mockTextFormatStrategy.Object, botConfig);
    }

    [Fact]
    public void WeatherDataPublisher_AttachSubscriber()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = new Mock<IWeatherSubscriber>();

        sut.Attach(mockSubscriber.Object);

        Assert.Contains(mockSubscriber.Object, sut.Subscribers);
    }

    [Fact]
    public void WeatherDataPublisher_DetachSubscriber()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = new Mock<IWeatherSubscriber>();

        sut.Attach(mockSubscriber.Object);
        sut.Detach(mockSubscriber.Object);

        Assert.DoesNotContain(mockSubscriber.Object, sut.Subscribers);
    }

    [Fact]
    public void WeatherDataPublisher_WeatherDataChanges_NotifiesNewSubscriberWithNewWeatherData()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = new Mock<IWeatherSubscriber>();
        sut.Attach(mockSubscriber.Object);

        var newWeatherData = fixture.Create<Weather>();
        sut.WeatherData = newWeatherData;

        mockSubscriber.Verify(
            subscriber => subscriber.ProcessWeatherUpdate(newWeatherData)
        );
    }

    [Fact]
    public void WeatherDataPublisher_WeatherDataDoesNotChange_DoesNotNotifyNewSubscriber()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = new Mock<IWeatherSubscriber>();
        sut.Attach(mockSubscriber.Object);

        mockSubscriber.Verify(
            subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<Weather>()),
            Times.Never
        );
    }

    [Fact]
    public void InitializeSubscribers_AttachesBotsFromBotConfig()
    {
        var botConfig = new BotConfiguration
        {
            RainBot = fixture.Create<RainBot>(),
            SnowBot = fixture.Create<SnowBot>(),
            SunBot = fixture.Create<SunBot>()
        };

        var sut = CreateWeatherDataPublisher();
        sut.Subscribers.Clear();

        sut.InitializeSubscribers(botConfig);

        Assert.Contains(botConfig.RainBot, sut.Subscribers);
        Assert.Contains(botConfig.SnowBot, sut.Subscribers);
        Assert.Contains(botConfig.SunBot, sut.Subscribers);
        Assert.Equal(3, sut.Subscribers.Count);
    }



    [Fact]
    public void Notify_CallsProcessWeatherUpdateForAllSubscribers()
    {
        var sut = CreateWeatherDataPublisher();
        sut.Subscribers.Clear();

        var mockSubscribers = new List<Mock<IWeatherSubscriber>>();

        for (int i = 0; i < 3; i++)
        {
            var mockSubscriber = new Mock<IWeatherSubscriber>();
            mockSubscribers.Add(mockSubscriber);
            sut.Attach(mockSubscriber.Object);
        }

        sut.NotifyAsync();

        foreach (var mockSubscriber in mockSubscribers)
        {
            mockSubscriber.Verify(
                subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<Weather>()),
                Times.Once
            );
        }
    }
}
