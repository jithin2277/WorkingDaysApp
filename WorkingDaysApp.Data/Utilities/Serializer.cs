using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WorkingDaysApp.Data
{
    public interface ISerializer
    {
        string Serialize(object value);
        T DeSerialize<T>(string value);
    }

    public class JsonSerializer : ISerializer
    {
        public T DeSerialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
