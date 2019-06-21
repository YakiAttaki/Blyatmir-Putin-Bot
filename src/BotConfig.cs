﻿using Blyatmir_Putin_Bot.services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Blyatmir_Putin_Bot
{
    class BotConfig
    {
        private static void Main(string[] args)
            => new BotConfig().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            await StartBotAsync();
        }

        public static DiscordSocketClient _client;
        public static CommandService Commands;
        public const char prefix = '/';

        public async static Task StartBotAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService();

            CommandHandler commandHandler = new CommandHandler(_client, Commands);

            //_client.MessageReceived += RestrictedWordsService.CheckForBannedTerms;
            _client.MessageReceived += FInChatService.Respond;
            _client.Log += Log;

            await _client.SetGameAsync("Rebuilding the USSR");
            await _client.LoginAsync(TokenType.Bot, "NTU2NjkwNjc5MzM0Njk5MDY4.D3yhrA.B1Ny_d9wDZ5drBqAuKr4lP9K7eQ");
            await commandHandler.InstallCommandsAsync();
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        public async static Task StopBotAsync()
        {
            await _client.StopAsync();
            Environment.Exit(1);
        }

        public static Task Log(LogMessage message)
        {
            //Dedicated Logging Method
            Task.Run(() => Console.WriteLine(message.ToString()));
            return Task.CompletedTask;
        }
    }
}