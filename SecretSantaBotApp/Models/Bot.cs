using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Telegram.Bot;
using SecretSantaBotApp.Models.Commands;
using SecretSantaBotApp.Models.Commands.RegistrationCommands;

namespace SecretSantaBotApp.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandList;
        public static IReadOnlyList<Command> Commands
        {
            get => commandList.AsReadOnly();
        }
        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (client != null)
            {
                return client;
            }

            commandList = new List<Command>
            {
                new StartCommand(),
                new HelloCommand(),
                new RegisterCommand(),
                new RegistrationStatusCommand(),
                new SetNameCommand(),
                new SetDateCommand(),
                new SetPlaceCommand(),
                //new SetCountCommand(),
                new SetInfoCommand(),
                new GenerateCommand(),
                new RemoveEventCommand(),
                new RandomizeCommand(),
                new ContactCommand()
            };
            //commandList.Add(new ClearCommand());
            //TODO: Add more commands

            client = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, @"api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }
    }
}