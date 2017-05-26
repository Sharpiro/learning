namespace InterviewPrep.Encryption.Extensions
{
    public static class MathExtension
    {
        public static int Modulus(int x, int y)
        {
            //return (x % y + y) % y;
            var remainder = x % y;
            var modulus = remainder >= 0 ? remainder : remainder + y;
            return modulus;
        }
    }
}