using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Actions
{
    public static class UserStart
    {
        public static async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            bool userExists = false;

            Regex regex = new Regex(@"^\/start\s(.*)");
            Match match = regex.Match(message.Text);

            if (match.Success)
            {
                string eventKey = match.Groups[1].Value;

                using (TelegramChatContext db = new TelegramChatContext())
                {
                    try
                    {
                        var events = db.Events;
                        var _event = await events.FirstOrDefaultAsync(x => x.InviteKey == eventKey);

                        TelegramChat user = new TelegramChat
                        {
                            ChatId = chatId,
                            FirstName = message.Chat.FirstName,
                            LastName = message.Chat.LastName,
                            UserName = message.Chat.Username,
                        };

                        foreach (var eventUser in _event.Participants)
                        {
                            if (eventUser.ChatId == user.ChatId)
                            {
                                userExists = true;
                                break;
                            }
                        }

                        if (!userExists)
                        {
                            _event.Participants.Add(user);
                            await db.SaveChangesAsync();

                            await client.SendTextMessageAsync(chatId, $"Congratulations! You are registered to event!");
                            //await client.SendTextMessageAsync(chatId, $"Now you can add wishlist using next syntax: /addWish Type your wish here");
                            //await client.SendTextMessageAsync(chatId, $"To clear wish list use command /clearWishList");
                            //await client.SendTextMessageAsync(chatId, $"To show wish list use command /showWishList");
                            await client.SendTextMessageAsync(chatId, $"When the event organizer closes the registration, " +
                                $"I will send you a message with a random participant to whom you will give a gift.");
                        }
                        else
                        {
                            await client.SendTextMessageAsync(chatId, "You don't need to register to this event again.");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"{nameof(UserStart)}:" + e.Message);
                    }
                }
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "Ooops, something wrong with your link or such event is not exists.");
            }
        }
    }
}