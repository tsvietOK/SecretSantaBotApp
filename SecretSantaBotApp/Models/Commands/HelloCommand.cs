using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => @"/hello";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var userName = message.Chat.Username;

            await client.SendTextMessageAsync(chatId, $"Hello {userName}!");
        }
    }
}