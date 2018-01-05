namespace asiye.Models
{
    class Channel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Channel(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
