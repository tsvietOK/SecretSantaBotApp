using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public class StartCommand : Command
    {
        private string startMessage = "Hello {0}! Now you are organizer of Secret Santa event.";
        //private string startMessage = "Hello {0}! I will help you to find a person whom you give a gift.";
        public override string Name => @"/start";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var userName = message.Chat.FirstName;

            //TODO: Bot logic

            await client.SendTextMessageAsync(chatId, string.Format(startMessage, userName));
            await client.SendTextMessageAsync(chatId, "Press /register to create a new event");
        }
    }
}