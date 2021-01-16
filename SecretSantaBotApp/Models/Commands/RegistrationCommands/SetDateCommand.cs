using SecretSantaBotApp.Models.Actions;
using SecretSantaBotApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands.RegistrationCommands
{
    public class SetDateCommand : Command
    {
        public override string Name => @"/setdate";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            await Registration.SetStageAsync(message, client, RegStage.SetDate);
        }
    }
}