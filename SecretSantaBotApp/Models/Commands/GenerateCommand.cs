using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Models.Commands
{
    public class GenerateCommand : Command
    {
        private readonly string statusName = @"You are invited to `{0}` event\." + Environment.NewLine;
        private readonly string statusDate = @"Event is scheduled for `{0}`\." + Environment.NewLine;
        private readonly string statusCount = @"Number of participants: `{0}`\." + Environment.NewLine;
        private readonly string statusPlace = @"Event location `{0}`\." + Environment.NewLine;
        private readonly string statusInfo = @"Additional info about event: `{0}`" + Environment.NewLine;
        private readonly string statusLink = @"Your registration link: https://t\.me/privy\_santa\_bot?start\={0}";

        public override string Name => @"/generate";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            string status = @"Hello\!" + Environment.NewLine;

            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        //key = secretEvent.InviteKey;
                        if (!string.IsNullOrWhiteSpace(secretEvent.Name))
                        {
                            status += string.Format(statusName, secretEvent.Name);
                        }
                        else
                        {
                            status += string.Format(statusName, "Secret Santa");
                        }

                        if (!string.IsNullOrWhiteSpace(secretEvent.Date))
                        {
                            status += string.Format(statusDate, secretEvent.Date);
                        }

                        if (!string.IsNullOrWhiteSpace(secretEvent.Place))
                        {
                            status += string.Format(statusPlace, secretEvent.Place);
                        }

                        if (!string.IsNullOrWhiteSpace(secretEvent.ParticipantsCount.ToString()))
                        {
                            if (secretEvent.ParticipantsCount > 0)
                            {
                                status += string.Format(statusCount, secretEvent.ParticipantsCount);
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(secretEvent.Info))
                        {
                            status += string.Format(statusInfo, secretEvent.Info);
                        }

                        status += string.Format(statusLink, secretEvent.InviteKey);

                        await client.SendTextMessageAsync(chatId, "Forward the next message to participants");

                        await client.SendTextMessageAsync(chatId, status, ParseMode.MarkdownV2);

                        await client.SendTextMessageAsync(chatId, "When all participants are accepted their invitations (you can check it by command /status), use command /shuffle to shuffle participants list.");
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, "You have to start registration using command /register");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(GenerateCommand)}:{e.Message}");
                }
            }
        }
    }
}