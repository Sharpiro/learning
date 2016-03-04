using System;

namespace InterviewPrep.DailyProgrammer
{
    public class AtbashCipherer
    {
        public void Encipher()
        {
            var test = "/r/dailyprogrammer";
            var dif = 7;
            foreach (var character in test)
            {
                int cryptoChar;
                if (character == ' ' || character == '/')
                {
                    cryptoChar = character;
                }
                else
                {
                    var diff2 = character - 'a' + dif;
                    cryptoChar = 'a' - diff2;
                }
                Console.Write((char)(cryptoChar));
            }
        }
    }
}