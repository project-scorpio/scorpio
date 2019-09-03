public class Util
{
    public static int CreateStamp() => (int)(DateTime.UtcNow - new DateTime(2017, 1, 1)).TotalDays;
}