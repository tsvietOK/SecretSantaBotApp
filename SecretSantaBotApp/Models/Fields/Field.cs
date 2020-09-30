using SecretSantaBotApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretSantaBotApp.Models.Fields
{
    public class Field
    {
        public string Value { get; set; }
        
        public FieldType Type { get; set; }

        public string Message { get; set; }
    }
}