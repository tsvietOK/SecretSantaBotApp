namespace SecretSantaBotApp.Models
{
    /// <summary>
    /// Represents a Telegram chat (user).
    /// </summary>
    public class TelegramChat
    {
        /// <summary>
        /// Gets or sets id of the user (chat).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Telegram chat id of the user (chat).
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user (chat).
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user (chat).
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user name of the user (chat).
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets another <see cref="TelegramChat" /> (user) to whom this user gives a gift.
        /// </summary>
        public TelegramChat GiftUser { get; set; }

        //public string WishList { get; set; }
    }
}