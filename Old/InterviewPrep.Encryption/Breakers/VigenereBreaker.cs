using System;
using System.Collections.Generic;
using System.IO;
using InterviewPrep.Encryption.Cyphers;

namespace InterviewPrep.Encryption.Breakers
{
    public class VigenereBreaker
    {
        private readonly List<string> _commonWordsList;

        public VigenereBreaker(List<string> commonWordsList)
        {
            _commonWordsList = commonWordsList;
        }

        public List<string> BreakVigenere(string inputText, List<string> nineLetterWords)
        {
            var outputList = new List<string>();
            //float iteration = 0;
            //float maxComputations = 26*26*26*26;
            for (var i = -1; i < 26; i++)
            {
                for (var j = -1; j < 26; j++)
                {
                    for (var k = -1; k < 26; k++)
                    {
                        for (var l = -1; l < 26; l++)
                        {
                            for (var m = 0; m < 26; m++)
                            {
                                //iteration++;
                                //float status = iteration / maxComputations;
                                //Console.WriteLine("{0:P}", iteration / maxComputations);
                                string a = (i < 0) ? string.Empty : ((char)(i + UnicodeConstants.UpperDelta)).ToString();
                                string b = (j < 0) ? string.Empty : ((char)(j + UnicodeConstants.UpperDelta)).ToString();
                                string c = (k < 0) ? string.Empty : ((char)(k + UnicodeConstants.UpperDelta)).ToString();
                                string d = (l < 0) ? string.Empty : ((char)(l + UnicodeConstants.UpperDelta)).ToString();
                                string e = (m < 0) ? string.Empty : ((char)(m + UnicodeConstants.UpperDelta)).ToString();
                                var testKey = string.Format("{0}{1}{2}{3}{4}", a, b, c, d, e);
                                var plainText = Vigenere.DecryptString(inputText, testKey);
                                if (BreakerFunctions.HasCommonWords(plainText, _commonWordsList))
                                {
                                    outputList.Add(plainText);
                                    Console.WriteLine(plainText);
                                    File.AppendAllText("resultsList.txt", plainText);
                                }
                                //if (BreakerFunctions.HasNineLetterWords(plainText, nineLetterWords))
                                //{
                                //    outputList.Add(plainText);
                                //    Console.WriteLine(plainText);
                                //    Printer.AppendFile("resultsList.txt", plainText);
                                //}
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("Total Iterations: {0}", iteration);
            return outputList;
        }
    }
}
