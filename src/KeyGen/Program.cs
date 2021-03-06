﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dragon.Common.Util;

namespace KeyGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dragon Context - Key Generator");

            Console.WriteLine();
            Console.WriteLine("Key: " + CryptUtil.GenerateKey());
            Console.WriteLine("IV:  " + CryptUtil.GenerateIV());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();
        }
    }
}
