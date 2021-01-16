using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public class ContactCommand : Command
    {
        public override string Name => @"/contact";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId, "Developer of this bot: @tsvietOK");
        }
    }
}