using SecretSantaBotApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Actions
{
    public static class Registration
    {
        public static async Task CheckStageAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var stage = await GetStageAsync(message);

            if (stage == RegStage.NotStarted)
            {
                await client.SendTextMessageAsync(chatId, "You have to register, use /start command to get instructions");
            }
            else if (stage == RegStage.SelectOption)
            {
                await client.SendTextMessageAsync(chatId, "You can fill optional fields or/and generate invitation using command /generate.");
            }
            else
            {
                if (await SetFieldAsync(message, stage))
                {
                    await SetStageAsync(message, client, RegStage.SelectOption);

                    await RegistrationStatus.Execute(message, client);
                }
            }
        }

        public static async Task<bool> SetFieldAsync(Message message, RegStage stage)
        {
            var chatId = message.Chat.Id;
            bool result = false;

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        switch (stage)
                        {
                            case RegStage.SetName:
                                secretEvent.Name = message.Text;
                                result = true;
                                break;
                            case RegStage.SetDate:
                                secretEvent.Date = message.Text;
                                result = true;
                                break;
                            case RegStage.SetPlace:
                                secretEvent.Place = message.Text;
                                result = true;
                                break;
                            case RegStage.SetCount:
                                int count;
                                if (int.TryParse(message.Text, out count))
                                {
                                    secretEvent.ParticipantsCount = count;
                                    result = true;
                                }

                                break;
                            case RegStage.SetInfo:
                                secretEvent.Info = message.Text;
                                result = true;
                                break;
                        }

                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(Registration)}.{nameof(SetFieldAsync)}:{e.Message}");
                }
            }

            return result;
        }

        public static async Task<RegStage> GetStageAsync(Message message)
        {
            var chatId = message.Chat.Id;

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        return secretEvent.RegistrationStage;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(Registration)}.{nameof(GetStageAsync)}:{e.Message}");
                    if (e.InnerException != null)
                    {
                        Debug.WriteLine(e.InnerException);
                    }
                }
            }

            return RegStage.NotStarted;
        }

        public static async Task SetStageAsync(Message message, TelegramBotClient client, RegStage stage)
        {
            var chatId = message.Chat.Id;

            using (TelegramChatContext db = new TelegramChatContext())
            {
                try
                {
                    var secretEvent = await db.Events.FirstOrDefaultAsync(x => x.HostChatId == chatId);
                    if (secretEvent != null)
                    {
                        secretEvent.RegistrationStage = stage;
                        await db.SaveChangesAsync();

                        await ShowStageMessageAsync(message, client, stage);
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, @"You have to start registration using command /register");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{nameof(Registration)}.{nameof(SetStageAsync)}:{e.Message}");
                }
            }
        }

        public static async Task ShowStageMessageAsync(Message message, TelegramBotClient client, RegStage stage)
        {
            var chatId = message.Chat.Id;

            switch (stage)
            {
                case RegStage.NotStarted:
                    break;
                case RegStage.SelectOption:
                    break;
                case RegStage.SetName:
                    await client.SendTextMessageAsync(chatId, "Send to me Name of your event");
                    break;
                case RegStage.SetDate:
                    await client.SendTextMessageAsync(chatId, "Send to me Date of your event");
                    break;
                case RegStage.SetPlace:
                    await client.SendTextMessageAsync(chatId, "Send to me Place of your event");
                    break;
                case RegStage.SetCount:
                    await client.SendTextMessageAsync(chatId, "Send to me Participants count of your event");
                    break;
                case RegStage.SetInfo:
                    await client.SendTextMessageAsync(chatId, "Send to me aditional Info about your event");
                    break;
                default:
                    break;
            }
        }
    }
}
