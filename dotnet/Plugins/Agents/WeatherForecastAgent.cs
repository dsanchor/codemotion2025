using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.AI.Projects;
using Azure.Core;
using Azure.Core.Pipeline;

public sealed class WeatherForecastAgent : BaseAgent
{
    [KernelFunction, Description("Retrieve the weather information for a given location.")]
    public async Task<string> Forecast([Description("location")]
        string location)
    {
        Console.WriteLine("WeatherForecastAgent Forecast called with location: " + location);

        var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("settings.json"));
        var connectionString = config["aiProjectCS"];
        var agentId = config["weatherAgentId"];
        string messageContent = location;

        return await RunAgentAsync(connectionString, agentId, messageContent);
    }
}