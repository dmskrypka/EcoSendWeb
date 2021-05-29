using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EcoSendWeb.Helpers
{
    public static class SerializationUtils
    {
        internal static StringBuilder SerializeObjectToBuilder<T>(T obj)
        {
            StringWriter sw = new StringWriter();
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(sw, obj);
            sw.Flush();
            return sw.GetStringBuilder();
        }

        public static string XmlObjectToString<T>(this T obj)
        {
            return SerializeObjectToBuilder(obj).ToString();
        }

        public static T DeserializeObject<T>(XmlReader xmlData)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(xmlData);
        }

        public static T DeserializeObject<T>(string xmlData)
        {
            if (String.IsNullOrEmpty(xmlData))
            {
                return default(T);
            }

            StringReader rdr = new StringReader(xmlData);
            XmlTextReader xrdr = new XmlTextReader(rdr);
            return DeserializeObject<T>(xrdr);
        }
    }
}