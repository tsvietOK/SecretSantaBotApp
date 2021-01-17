using SecretSantaBotApp.Models;
using SecretSantaBotApp.Models.Actions;
using SecretSantaBotApp.Models.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretSantaBotApp.Controllers
{
    [Route(@"api/message/update")]
    public class MessageController : ApiController
    {
        [HttpPost]
        public async Task<OkResult> Post([FromBody] Update update)
        {
            if (update == null)
            {
                return Ok();
            }

            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.GetBotClientAsync();
            bool commandFound = false;

            var prevMessage = update.Message.ReplyToMessage;

            if (prevMessage != null)
            {
                await CommandNotFound.Execute(prevMessage, client);
            }

            if (string.IsNullOrWhiteSpace(message.Text))
            {
                return Ok();
            }

            if (message.Text.StartsWith("/")) //is command
            {
                foreach (var command in commands)
                {
                    if (message.Text.Equals(command.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        await command.ExecuteAsync(message, client);
                        commandFound = true;
                        break;
                    }

                    if (message.Text.StartsWith(command.Name))
                    {
                        await UserStart.Execute(message, client);
                        commandFound = true;
                        break;
                    }
                }

                if (!commandFound)
                {
                    await CommandNotFound.Execute(message, client);
                }
            }
            else
            {
                await Registration.CheckStageAsync(message, client);
            }

            return Ok();
        }
    }
}
