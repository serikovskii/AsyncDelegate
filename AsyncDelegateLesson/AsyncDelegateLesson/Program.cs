using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDelegateLesson
{
    class Program
    {
        private delegate int AsyncDelegate(int first, int second);
        static void Main(string[] args)
        {
            GetCurrentThreadInfo();
            var executer = new AsyncDelegate(Sum);
            var asyncResult = executer.BeginInvoke(10, 15, SumCallback, null);

            //var result = executer.EndInvoke(asyncResult);
            //Sum(10, 15);

            Console.ReadLine();
        }

        public static void SumCallback(IAsyncResult result)
        {
            var stringData = result.AsyncState.ToString();
            var executer = (result as AsyncResult).AsyncDelegate as AsyncDelegate;
            var dataResult = executer.EndInvoke(result);
            Console.WriteLine(stringData + dataResult);
        }
        public static int Sum(int firstNumber, int secondNumber)
        {
            GetCurrentThreadInfo();

            Thread.Sleep(5000);
            return firstNumber + secondNumber;
            //Console.WriteLine($"{firstNumber.ToString()} + {secondNumber.ToString()} = {firstNumber+secondNumber}");
        }

        public static void GetCurrentThreadInfo()
        {
            var currentThread = Thread.CurrentThread;
            Console.WriteLine($"Происходит работа в потоке {currentThread.ManagedThreadId}");
        }
    }
}
