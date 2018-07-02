using System;

namespace InterviewPrep.Encryption
{
    public enum Alphabet
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M,
        N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        Space = 32
    }

    public static class EnumExtensions
    {
        public static Alphabet GetEnumChar(this char inputChar)
        {
            try
            {
                return (Alphabet)Enum.Parse(typeof(Alphabet), inputChar.ToString(), true);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
                return Alphabet.Space;
            }
        }
        public static Alphabet GetEnumChar(this int inputChar)
        {
            var numberString = inputChar.ToString();
            return (Alphabet)Enum.Parse(typeof(Alphabet), numberString, true);
        }
    }

}
