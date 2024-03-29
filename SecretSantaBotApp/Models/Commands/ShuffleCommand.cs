﻿using SecretSantaBotApp.Extensions;
using SecretSantaBotApp.Models.Actions;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Models.Commands
{
    public class ShuffleCommand : Command
    {
        private readonly int minimumParticipantsCount = 3;

        public override string Name => @"/shuffle";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        var telegramChatsIds = secretEvent.Participants.Select(x => x.Id).ToArray();
                        if (telegramChatsIds.Length >= minimumParticipantsCount)
                        {
                            telegramChatsIds.Shuffle();

                            for (int i = 0; i < telegramChatsIds.Count(); i++)
                            {
                                var chat = secretEvent.Participants.ElementAt(i);
                                chat.GiftUser = secretEvent.Participants.First(x => x.Id == telegramChatsIds[i]);
                            }

                            await client.SendTextMessageAsync(chatId, "The Magic Hat completed its work successfully! Each participant will" +
                                " automatically receive a message with the name of the person to whom he is giving the gift.");

                            await MessageDistributor.SendParticipantsMessageAsync(message, client);
                        }
                        else
                        {
                            await client.SendTextMessageAsync(chatId, $"Sorry, to use this command, there must be at least {minimumParticipantsCount} participants.");
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, @"You have to start registration using command /register.");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(ShuffleCommand)}:{e.Message}");
                    if (e.InnerException != null)
                    {
                        Debug.WriteLine(e.InnerException);
                    }
                }
            }
        }
    }
}