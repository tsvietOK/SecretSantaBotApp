using SecretSantaBotApp.Models.Actions;
using SecretSantaBotApp.Models.Enums;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands.RegistrationCommands
{
    public class SetPlaceCommand : Command
    {
        public override string Name => @"/setPlace";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            await Registration.SetStageAsync(message, client, RegStage.SetPlace);
        }
    }
}