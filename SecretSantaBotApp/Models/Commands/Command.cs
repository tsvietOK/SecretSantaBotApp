using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, TelegramBotClient client);

        public bool Contains(Message message)
        {
            return message.Text.Contains(Name);
        }
    }
}