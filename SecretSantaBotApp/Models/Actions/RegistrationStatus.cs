using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Models
{
    public static class RegistrationStatus
    {
        //private static readonly string status = "\r\nInvite key: `{0}`\r\nEvent name: `{1}` /setname\r\nEvent date: `{2}` /setdate\r\nPlace: `{3}` /setplace\r\nParticipants count: `{4}` /setcount\r\nEvent info: `{5}` /setinfo\r\n";
        private static readonly string status =
            @"Invite key: `{0}`
Event name: `{1}` /setname
🗓Event date: `{2}` /setdate
🏠Place: `{3}` /setplace
ℹ️Event info: `{4}` /setinfo
Registered participants count: `{5}`
If you are ready for sending invitations, run /generate command and forward generated message to participants
You can check event status using command /status";
        private static readonly string emptyText = "🚫(optional)";

        public static async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            string key = string.Empty;
            string name = emptyText;
            string dateTime = emptyText;
            string place = emptyText;
            string info = emptyText;
            int count = 0;

            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        key = secretEvent.InviteKey;
                        if (!string.IsNullOrWhiteSpace(secretEvent.Name))
                        {
                            name = secretEvent.Name;
                        }
                        if (!string.IsNullOrWhiteSpace(secretEvent.Date))
                        {
                            dateTime = secretEvent.Date;
                        }
                        if (!string.IsNullOrWhiteSpace(secretEvent.Place))
                        {
                            place = secretEvent.Place;
                        }
                        if (!string.IsNullOrWhiteSpace(secretEvent.Info))
                        {
                            info = secretEvent.Info;
                        }

                        count = secretEvent.Participants.Count();
                        secretEvent.ParticipantsCount = count;
                        await db.SaveChangesAsync();
                        

                        await client.SendTextMessageAsync(chatId, string.Format(status, key, name, dateTime, place, info, count), ParseMode.MarkdownV2);
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, @"You have to start registration using command /register");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(RegistrationStatus)}:{e.Message}");
                    if (e.InnerException != null)
                    {
                        Debug.WriteLine(e.InnerException);
                    }
                }
            }
        }
    }
}