using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands.RegistrationCommands
{
    public class RegistrationStatusCommand : Command
    {
        public override string Name => @"/status";

        public override Task Execute(Message message, TelegramBotClient client)
        {
            return RegistrationStatus.Execute(message, client);
        }
    }
}