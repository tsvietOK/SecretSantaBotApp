using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using SecretSantaBotApp.Models.Actions;
using System.Diagnostics;
using SecretSantaBotApp.Models.Enums;
using System.Data.Entity;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Models.Commands
{
    public class RegisterCommand : Command
    {
        public override string Name => @"/register";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var events = db.Events;
                    var _event = await events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (_event == null)
                    {
                        SecretSantaEvent secretSantaEvent = new SecretSantaEvent(chatId);
                        if (events.Where(x => x.InviteKey.Equals(secretSantaEvent.InviteKey)).Any())
                        {
                            secretSantaEvent.GenerateInviteKey();
                        }

                        db.Events.Add(secretSantaEvent);
                        await db.SaveChangesAsync();

                        await client.SendTextMessageAsync(chatId, "You can fill optional fields or/and generate invitation using command /generate.");
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, "You are already registered. You can fill optional fields or/and generate invitation.");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(RegisterCommand)}:{e.Message}");
                    if (e.InnerException != null)
                    {
                        Debug.WriteLine(e.InnerException.Message);
                    }
                }
            }

            await Registration.SetStageAsync(message, client, RegStage.SelectOption);
            await RegistrationStatus.Execute(message, client);
        }
    }
}