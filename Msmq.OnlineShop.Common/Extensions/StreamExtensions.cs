﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace Msmq.OnlineShop.Common.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadToEnd(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static T ReadFromJson<T>(this Stream stream)
        {
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static object ReadFromJson(this Stream stream, string messageType)
        {
            var type = Type.GetType(messageType);
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}
