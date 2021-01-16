using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Models.Actions
{
    public static class MessageDistributor
    {
        public static async Task SendParticipantsMessageAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        foreach (var participant in secretEvent.Participants)
                        {
                            var giftUser = participant.GiftUser;
                            
                            var sb = new StringBuilder($"Person to whom you give a gift: {giftUser.FirstName} ");
                            if (!string.IsNullOrEmpty(giftUser.LastName))
                            {
                                sb.Append(giftUser.LastName);
                            }

                            if (!string.IsNullOrEmpty(giftUser.UserName))
                            {
                                sb.Append($"(@{giftUser.UserName})");
                            }

                            await client.SendTextMessageAsync(participant.ChatId, sb.ToString());
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, @"You have to start registration using command /register");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(MessageDistributor)}:{e.Message}");
                    if (e.InnerException != null)
                    {
                        Debug.WriteLine(e.InnerException);
                    }
                }
            }
        }
    }
}