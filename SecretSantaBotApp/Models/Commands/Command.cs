using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretSantaBotApp.Models.Commands
{
    /// <summary>
    /// Represents a Telegram bot command.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Gets command name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// This method is used to execute <see cref="Command" />.
        /// </summary>
        /// <param name="message">The message from Telegram</param>
        /// <param name="client"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(Message message, TelegramBotClient client);
    }
}