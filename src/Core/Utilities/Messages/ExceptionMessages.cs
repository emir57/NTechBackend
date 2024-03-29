﻿using System.Runtime.Serialization;

namespace Core.Utilities.Messages
{
    public class ExceptionMessages
    {
        public static readonly string UserIdNotFound = "UserId not found";
        public static readonly string NotEqualPropertyType = "Not equal property type";
        public static readonly string WrongValidationType = "Wrong validation type";
        public static readonly string WrongLoggingType = "Wrong logging type";
        public static readonly string SerilogNotFoundFolderPath = "Serilog not found folder path";
        public static readonly string Unauthorized = "Unauthorized";
        public static readonly string NullLanguageMessage = "Wrong language message type";
        public static readonly string NoSelectedDatabase = "No selected database";
        public static readonly string NoSelectedMessageResultLanguage = "No selected message result language";
    }
}
