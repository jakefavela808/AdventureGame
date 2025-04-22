using System;
using System.Threading;

namespace AdventureS25
{
    public static class TextPrinter
    {
        // Adjustable delays (in milliseconds)
        public static int NormalDelay = 35;
        public static int CommaDelay = 90;
        public static int PeriodDelay = 180;
        public static int ExclamationDelay = 210;
        public static int QuestionDelay = 210;
        public static int NewlineDelay = 60;
        public static int SpaceDelay = 18;

        public static void Print(string text)
        {
            SkipToken.Reset();
            using (var skipListener = new SkipListener())
            {
                skipListener.Start();
                int i = 0;
                while (i < text.Length)
                {
                    if (SkipToken.SkipRequested)
                    {
                        Console.Write(text.Substring(i));
                        break;
                    }
                    char c = text[i];
                    Console.Write(c);
                    int delay = NormalDelay;
                    switch (c)
                    {
                        case ',': delay = CommaDelay; break;
                        case '.': delay = PeriodDelay; break;
                        case '!': delay = ExclamationDelay; break;
                        case '?': delay = QuestionDelay; break;
                        case '\n': delay = NewlineDelay; break;
                        case ' ': delay = SpaceDelay; break;
                    }
                    if (i+1 < text.Length && IsPunctuation(text[i+1]) && IsPunctuation(c))
                        delay = 0;
                    Thread.Sleep(delay);
                    i++;
                }
                Console.WriteLine();
            }
        }

        private static bool IsPunctuation(char c)
        {
            return c == ',' || c == '.' || c == '!' || c == '?' || c == '\n';
        }
    }
}
