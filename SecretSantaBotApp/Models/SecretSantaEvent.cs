using SecretSantaBotApp.Helpers;
using SecretSantaBotApp.Models.Enums;
using System.Collections.Generic;

namespace SecretSantaBotApp.Models
{
    public class SecretSantaEvent
    {
        public SecretSantaEvent()
        {
            Participants = new List<TelegramChat>();
        }

        public SecretSantaEvent(long hostChatId)
        {
            Participants = new List<TelegramChat>();
            HostChatId = hostChatId;
            GenerateInviteKey();
        }

        public int Id { get; set; }

        public long HostChatId { get; set; }

        public string InviteKey { get; set; }

        public RegStage RegistrationStage { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string Place { get; set; }

        public int ParticipantsCount { get; set; }

        public string Info { get; set; }

        public virtual ICollection<TelegramChat> Participants { get; set; }

        public void GenerateInviteKey()
        {
            InviteKey = StringGenerator.RandomString(16);
        }
    }
}