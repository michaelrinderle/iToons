namespace iToons.Mobile
{
    public static class Constants
    {
        public static string Http = "http://";
        public static readonly string Host = "yep.redirectme.net:";
        public static int Port = 80;

        public static string BaseMetaUrl = "/music/getmetadata";
        public static string BaseStreamUrl = "/music/getsongstream?id=";

        public static string GetBaseMetaUrl()
        {
            return Http + Host + Port.ToString() + BaseMetaUrl;
        }
        public static string GetBaseStreamUrl(int id)
        {
            return Http + Host + Port.ToString() + BaseStreamUrl + id.ToString();
        }
    }
}