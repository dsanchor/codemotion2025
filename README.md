# Multiagent demo for Codemotion 2025

## Introduction

This repository contains the code for the multi-agent demo used in the Codemotion 2025.

The first set of demos are part of the same polyglot notebook and written in c#. The notebook is available in the `dotnet` with name [`sk-agents.dib`](dotnet/sk-agents.dib).

Before running any demo, copy the `settings-emtpy.json` file to a new `settings.json`. Set all the necessary parameters there.

For the second part of demos, we have a set of notebooks in Python. The notebooks are available in the `python` folder.

You must create a new `.env` file based on the `.empty-env` file. Set all the necessary parameters there.

## Runnning the demos 

I have prepared a `devcontainer` for you to run the demos. You can use it to run the demos in a local environment or in a cloud environment, such as GitHub Codespaces. 

For the latest, you can use the `<> Code` button in the GitHub repository to create a new Codespace. It takes a few minutes to set up, but once it's done, you can run the demos in the browser.

## Demos

### dotnet

- Installation of required packages and setup of the configuration. [Set up](dotnet/sk-agents.dib#install-packages-and-read-configuration)
- [First agent](dotnet/sk-agents.dib#create-an-agent-based-on-the-most-simple-kernel) using Semantic Kernel
- [Semantic Kernel Agent with plugins](dotnet/sk-agents.dib#agent-with-a-vitamined-kernel)
- Multi-agents orchestation:
    - [Tour planner](dotnet/sk-agents.dib#tour-planner): An Agents group chat with sequential selection strategy and simple approval termination strategy.
    - [Hotel recepcionist service: a day plan ](dotnet/sk-agents.dib#hotel-recepcionist-service-a-day-plan): An Agents group chat with custom selection strategy and custom approval termination strategy.

### python

- Installation of required packages and setup of the configuration. [Set up](python/sk-mcp.ipynb#packages-and-configurations)
- [Travel Agent with MCP](python/sk-mcp.ipynb#travel-agent-with-mcp): An example of using Semantic Kernel with MCP plugins (Airbnb and Ticketmaster) as tools for travel planning. For the `ticketmaster` mcp server, you need to set up the API key in the `.env` file. You can get one at the [Ticketmaster Developer Portal](https://developer.ticketmaster.com/).