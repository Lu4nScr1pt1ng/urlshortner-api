namespace urlshortner.Utils
{
    public class GenerateLink
    {
        public string Generate()
        {
            Random random = new Random();
            string text = "";
            string poss = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for(int i = 0; i < 5; i++)
            {
                text += poss[random.Next(1, poss.Length)]; ;
            }
            return text;
        }
    }
}
