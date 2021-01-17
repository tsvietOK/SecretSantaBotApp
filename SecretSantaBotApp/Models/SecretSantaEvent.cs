using SecretSantaBotApp.Helpers;
using SecretSantaBotApp.Models.Enums;
using System.Collections.Generic;

namespace SecretSantaBotApp.Models
{
    /// <summary>
    /// Represents a Secret Santa event.
    /// </summary>
    public class SecretSantaEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecretSantaEvent" /> class.
        /// </summary>
        public SecretSantaEvent()
        {
            Participants = new List<TelegramChat>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretSantaEvent" /> class.
        /// </summary>
        /// <param name="hostChatId"><see cref="SecretSantaEvent" /> hosts's Telegram chat (user) id.</param>
        public SecretSantaEvent(long hostChatId) : this()
        {
            HostChatId = hostChatId;
            GenerateInviteKey();
        }

        /// <summary>
        /// Gets or sets id of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets host chat (user) id of the event.
        /// </summary>
        public long HostChatId { get; set; }

        /// <summary>
        /// Gets or sets event invitation key to the event.
        /// </summary>
        public string InviteKey { get; set; }

        /// <summary>
        /// Gets or sets current registration stage of the event.
        /// </summary>
        public RegStage RegistrationStage { get; set; }

        /// <summary>
        /// Gets or sets name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets event date.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets event location.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets number of participants.
        /// </summary>
        public int ParticipantsCount { get; set; }

        /// <summary>
        /// Gets or sets additional info about event.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Gets or sets collection of participants.
        /// </summary>
        public virtual ICollection<TelegramChat> Participants { get; set; }

        /// <summary>
        /// Generates a new invite key.
        /// </summary>
        public void GenerateInviteKey()
        {
            InviteKey = StringGenerator.RandomString(16);
        }
    }
}