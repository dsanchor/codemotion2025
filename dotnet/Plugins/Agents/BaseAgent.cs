using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.AI.Projects;
using Azure.Core;
using Azure.Core.Pipeline;

public abstract class BaseAgent
{
    protected async Task<string> RunAgentAsync(string connectionString, string agentId, string messageContent)
    {
        var projectClient = new AIProjectClient(connectionString, new DefaultAzureCredential());
        AgentsClient agentClient = projectClient.GetAgentsClient();

        // Create thread for communication
        Azure.Response<AgentThread> threadResponse = await agentClient.CreateThreadAsync();
        AgentThread thread = threadResponse.Value;

        // Create message to thread
        Azure.Response<ThreadMessage> messageResponse = await agentClient.CreateMessageAsync(
            thread.Id,
            MessageRole.User,
            messageContent);
        ThreadMessage message = messageResponse.Value;

        Azure.Response<Azure.AI.Projects.Agent> agentResponse = await agentClient.GetAgentAsync(agentId);
        Azure.AI.Projects.Agent agent = agentResponse.Value;

        // Run the agent
        Azure.Response<Azure.AI.Projects.ThreadRun> runResponse = await agentClient.CreateRunAsync(thread, agent);
        Console.WriteLine($"Run ID: {runResponse.Value.Id}");
        do
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            Console.WriteLine("Waiting for agent response..." + runResponse.Value.Status);
            runResponse = await agentClient.GetRunAsync(thread.Id, runResponse.Value.Id);
        }
        while (runResponse.Value.Status == RunStatus.Queued
            || runResponse.Value.Status == RunStatus.InProgress);

        Azure.Response<PageableList<ThreadMessage>> afterRunMessagesResponse
            = await agentClient.GetMessagesAsync(thread.Id);
        IReadOnlyList<ThreadMessage> messages = afterRunMessagesResponse.Value.Data;

        string result = "";

        // Note: messages iterate from newest to oldest, with the messages[0] being the most recent
        foreach (ThreadMessage threadMessage in messages)
        {
            if (threadMessage.Role.ToString().ToLower() == "assistant")
            {
                Console.WriteLine("Response from Agent:");
                foreach (MessageContent contentItem in threadMessage.ContentItems)
                {
                    if (contentItem is MessageTextContent textItem)
                    {
                        Console.Write(textItem.Text);
                        result = textItem.Text;
                    }
                    break;
                    Console.WriteLine();
                }
            }
        }

        return result;
    }
}