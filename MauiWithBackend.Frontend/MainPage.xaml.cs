using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MauiWithBackend.Integration.Models;

namespace MauiWithBackend.Frontend;

public partial class MainPage : ContentPage
{
    int count = 0;

    //ITextToSpeech textToSpeech = new TextToSpeech();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        var forecast = await GetWeatherForecastAsync();
        var first = forecast.First();

        Weather.Text = $"The weather on {first.Date} will be {first.TemperatureC} celcius";
    }

    private async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        const string url = "https://localhost:7036/weatherforecast";

        using var client = new HttpClient();
        var resp = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>(url);
        
        ArgumentNullException.ThrowIfNull(resp);
        return resp;
    }

    //async Task StartListening(CancellationToken cancellationToken)
    //{
    //    var isGranted = await speechToText.RequestPermissions(cancellationToken);
    //    if (!isGranted)
    //    {
    //        await Toast.Make("Permission not granted").Show(CancellationToken.None);
    //        return;
    //    }

    //    speechToText.RecognitionResultUpdated += OnRecognitionTextUpdated;
    //    speechToText.RecognitionResultCompleted += OnRecognitionTextCompleted;
    //    await speechToText.StartListenAsync(new SpeechToTextOptions { Culture = CultureInfo.CurrentCulture, ShouldReportPartialResults = true }, CancellationToken.None);
    //}

    //async Task StopListening(CancellationToken cancellationToken)
    //{
    //    await speechToText.StopListenAsync(CancellationToken.None);
    //    speechToText.RecognitionResultUpdated -= OnRecognitionTextUpdated;
    //    speechToText.RecognitionResultCompleted -= OnRecognitionTextCompleted;
    //}

    //void OnRecognitionTextUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs args)
    //{
    //    RecognitionText += args.RecognitionResult;
    //}

    //void OnRecognitionTextCompleted(object? sender, SpeechToTextRecognitionResultCompletedEventArgs args)
    //{
    //    RecognitionText = args.RecognitionResult;
    //}
}
