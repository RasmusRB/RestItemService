using System;
using System.ComponentModel;

namespace ConsumeREST
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeWorker worker = new ConsumeWorker();
            worker.Start();


            Console.ReadLine();
        }
    }
}
