﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTE_BL;
using GTE_BL.Entities;
using GTE_BL.Services;

namespace GET_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //VelocifyService s = new VelocifyService("brian@bestrate-insurance.com", "7164789226");
            //Console.WriteLine(s.GetContactIdByPhone("7164789226"));
            
            var sUPC = "99999";
            var b = int.TryParse(sUPC, out int upc);
            Console.WriteLine(upc);

            Console.ReadLine();
        }
    }
}
