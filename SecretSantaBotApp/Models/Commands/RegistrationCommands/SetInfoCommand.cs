using SecretSantaBotApp.Models.Actions;
using SecretSantaBotApp.Models.Enums;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands.RegistrationCommands
{
    public class SetInfoCommand : Command
    {
        public override string Name => @"/setinfo";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            await Registration.SetStageAsync(message, client, RegStage.SetInfo);
        }
    }
}