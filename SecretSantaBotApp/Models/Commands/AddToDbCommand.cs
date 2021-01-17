using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    public class AddToDbCommand : Command
    {
        public override string Name => @"/addtodb";

        public override async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            bool userExists = false;

            TelegramChat user = new TelegramChat
            {
                ChatId = chatId,
                FirstName = message.Chat.FirstName,
                LastName = message.Chat.LastName,
                UserName = message.Chat.Username
            };

            using (TelegramChatContext db = new TelegramChatContext())
            {
                await client.SendTextMessageAsync(chatId, "Created DB context");
                try
                {
                    /*db.Database.Connection.Open();
                    await client.SendTextMessageAsync(chatId, "Opened DB connection");*/

                    var users = db.Users;
                    await client.SendTextMessageAsync(chatId, "Get users");

                    foreach (var dbUser in users)
                    {
                        await client.SendTextMessageAsync(chatId, $"Check for user:{dbUser}");
                        if (user.ChatId == dbUser.ChatId)
                        {
                            await client.SendTextMessageAsync(chatId, "Your data already saved in db.");
                            userExists = true;
                            break;
                        }
                    }

                    if (!userExists)
                    {
                        await client.SendTextMessageAsync(chatId, "Try to add user to DB");
                        db.Users.Add(user);
                        await client.SendTextMessageAsync(chatId, "User has been added, trying to save changes");
                        await db.SaveChangesAsync();

                        await client.SendTextMessageAsync(chatId, $"ChatId={user.ChatId} and userName={user.FirstName} added to db.");
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, "User already exists in DB");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            await client.SendTextMessageAsync(chatId, "Disposed");
        }
    }
}