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
    private readonly IFixture _fixture;
    private readonly Mock<ITextFormatStrategy> _mockTextFormatStrategy;
    private readonly BotConfiguration _botConfig;

    public WeatherPublisherTest()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockTextFormatStrategy = _fixture.Create<Mock<ITextFormatStrategy>>();
        _botConfig = _fixture.Create<BotConfiguration>();
    }

    private WeatherPublisher CreateWeatherDataPublisher()
    {
        var text = _fixture.Create<string>();
        _mockTextFormatStrategy.Setup(x => x.GetWeatherData(text)).Returns(_fixture.Create<Weather>());

        return new WeatherPublisher(text, _mockTextFormatStrategy.Object, _botConfig);
    }

    [Fact]
    public void WeatherDataPublisher_AttachSubscriber()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = _fixture.Create<Mock<IWeatherSubscriber>>();

        sut.Attach(mockSubscriber.Object);

        // Assert
        var actualSubscribers = sut.Subscribers;
        var expectedSubscriber = mockSubscriber.Object;

        Assert.Contains(expectedSubscriber, actualSubscribers);
    }

    [Fact]
    public void WeatherDataPublisher_DetachSubscriber()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = _fixture.Create<Mock<IWeatherSubscriber>>();

        sut.Attach(mockSubscriber.Object);
        sut.Detach(mockSubscriber.Object);

        Assert.DoesNotContain(mockSubscriber.Object, sut.Subscribers);
    }

    [Fact]
    public void WeatherDataPublisher_WeatherDataDoesNotChange_DoesNotNotifyNewSubscriber()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber = _fixture.Create<Mock<IWeatherSubscriber>>();
        sut.Attach(mockSubscriber.Object);

        mockSubscriber.Verify(
            subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<Weather>()),
            Times.Never
        );
    }

    [Fact]
    public void InitializeSubscribers_AttachesBotsFromBotConfig()
    {
        // Arrange
        var sut = CreateWeatherDataPublisher();
        sut.Subscribers.Clear();
        var botConfig = _fixture.Build<BotConfiguration>()
                                 .With(x => x.RainBot, _fixture.Create<RainBot>())
                                 .With(x => x.SnowBot, _fixture.Create<SnowBot>())
                                 .With(x => x.SunBot, _fixture.Create<SunBot>())
                                 .Create();

        // Act
        sut.InitializeSubscribers(botConfig);

        // Assert
        Assert.Contains(botConfig.RainBot, sut.Subscribers);
        Assert.Contains(botConfig.SnowBot, sut.Subscribers);
        Assert.Contains(botConfig.SunBot, sut.Subscribers);
        Assert.Equal(3, sut.Subscribers.Count);
    }

    [Fact]
    public async Task Notify_CallsProcessWeatherUpdateForAllSubscribers()
    {
        var sut = CreateWeatherDataPublisher();
        var mockSubscriber1 = _fixture.Create<Mock<IWeatherSubscriber>>();
        var mockSubscriber2 = _fixture.Create<Mock<IWeatherSubscriber>>();

        sut.Attach(mockSubscriber1.Object);
        sut.Attach(mockSubscriber2.Object);

        await sut.NotifyAsync();

        mockSubscriber1.Verify(
            subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<Weather>()),
            Times.Exactly(2)
        );

        mockSubscriber2.Verify(
            subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<Weather>()),
            Times.Exactly(2)
        );
    }


}
