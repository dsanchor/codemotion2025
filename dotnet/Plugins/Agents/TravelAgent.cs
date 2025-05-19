using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.AI.Projects;
using Azure.Core;
using Azure.Core.Pipeline;

public sealed class TravelAgent : BaseAgent
{
    [KernelFunction, Description("You are an agent responsible for suggesting activities "
        + "and creating an itinerary for a given location with respect to the weather")]
    public async Task<string> Recommend([Description("location")]
        string location, [Description("weather")] string weather)
    {
        Console.WriteLine("TravelAgent Recommend called with location: " + location + " and weather: " + weather);

        var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("settings.json"));
        var connectionString = config["aiProjectCS"];
        var agentId = config["travelAgentId"];
        string messageContent = "I am planning to visit " + location + " and the weather is " + weather;

        return await RunAgentAsync(connectionString, agentId, messageContent);
    }
}