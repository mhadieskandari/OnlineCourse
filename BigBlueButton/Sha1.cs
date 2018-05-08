namespace BigBlueButton
{
    public class Sha1
    {
        public static string GetSha1(string strValue)
        {
            Md5 md = new Md5();
            return md.encryptString(strValue, 1);
        }
    }
}
