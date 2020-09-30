using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public static class CommandNotFound
    {
        public static Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            return client.SendTextMessageAsync(chatId, "Unrecognized command " + message.Text);
        }
    }
}