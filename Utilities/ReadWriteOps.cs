using asiye.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace asiye.Utilities
{
    class ReadWriteOps
    {
        public static void WriteJsonFile(List<Channel> channels, string filename)
        {
            string output = JsonConvert.SerializeObject(channels, Formatting.Indented);
            Console.WriteLine(output);
            System.IO.File.WriteAllText(filename, output);
        }

        public static List<Channel> ReadJsonFile(string filename)
        {
            List<Channel> channels;
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                channels = JsonConvert.DeserializeObject<List<Channel>>(json);
            }
            return channels;
        }

        public static void WriteTextFile(List<Channel> channels, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {

                foreach (Channel channel in channels)
                {
                    sw.WriteLine($"{channel.Name}|{channel.Url}");
                }
            }
        }

        static List<Channel> ReadTextFile(string filename)
        {
            List<Channel> channels = new List<Channel>();
            char[] pipeSeparator = new char[] { '|' };
            string[] result;

            string line = "";
            using (StreamReader sr = new StreamReader(filename))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    result = line.Split(pipeSeparator, StringSplitOptions.None);
                    Channel tempChannel = new Channel(result[0], result[1]);
                    channels.Add(tempChannel);
                }
            }
            return channels;
        }
    }
}
