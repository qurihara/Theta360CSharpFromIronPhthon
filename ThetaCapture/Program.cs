using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThetaCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            //string saveDir = "C:/tmp/server/imgs/";
            string saveDir = args[0];
            Console.WriteLine(args[0]);
            bool suc = Theta360Wrapper.Theta360.Capture(saveDir);

        }
    }
}
