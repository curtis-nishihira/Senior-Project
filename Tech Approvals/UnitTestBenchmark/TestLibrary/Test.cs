using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestLibrary
{
    public class Test
    {
        int NumberOfItems = 100000;
        private double timeElapsed;

        public bool generateList()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                var list = new List<string>(NumberOfItems);
                for (int i = 0; i < NumberOfItems; i++)
                {
                    list.Add("Hello World!" + i);
                }
                watch.Stop();
                timeElapsed = watch.Elapsed.TotalSeconds;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double getElapsedTime()
        {
            return timeElapsed;
        }
    }
}
