﻿namespace SecretSantaBotApp.Models
{
    public class TelegramChat
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public TelegramChat GiftUser { get; set; }

        //public string WishList { get; set; }
    }
}