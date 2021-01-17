using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Models.Commands
{
    public class RemoveEventCommand : Command
    {
        public override string Name => @"/clear";

        public async override Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var events = db.Events;
                    var _event = await events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (_event != null)
                    {
                        var userList = _event.Participants;

                        if (userList.Count > 0)
                        {
                            db.Users.RemoveRange(userList);
                        }

                        db.Events.Remove(_event);

                        await db.SaveChangesAsync();

                        await client.SendTextMessageAsync(chatId, "Event and participants was successfully removed.");
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, "There is nothing to remove.");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(RemoveEventCommand)}:{e.Message}");
                    if (e.InnerException != null)
                    {
                        Debug.WriteLine(e.InnerException);
                    }
                }
            }
        }
    }
}