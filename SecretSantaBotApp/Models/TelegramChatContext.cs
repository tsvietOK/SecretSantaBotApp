using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SecretSantaBotApp.Models
{
    public class TelegramChatContext : DbContext
    {
        public TelegramChatContext()
            : base(ConfigurationManager.ConnectionStrings["SecretSantaDbConnection"].ConnectionString)
        {
        }

        public DbSet<SecretSantaEvent> Events { get; set; }

        public DbSet<TelegramChat> Users { get; set; }
    }
}