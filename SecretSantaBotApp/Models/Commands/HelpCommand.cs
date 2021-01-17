using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public class HelpCommand : Command
    {
        private readonly string help =
            @"1️⃣ Create a new Secret Santa event using the /register command." + Environment.NewLine +
            @"2️⃣ Fill in the information for this event using the commands provided." + Environment.NewLine +
            @"3️⃣ Generate an invitation message and forward it to the participants." + Environment.NewLine +
            @"4️⃣ Participants follow the link in the invitation to accept it." + Environment.NewLine +
            @"5️⃣ When all participants accept their invitations, the event creator shuffles participants list using /shuffle command, " +
            "after which all participants receive a message with the names they are giving the gift to.";

        public override string Name => @"/help";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, help);
        }
    }
}