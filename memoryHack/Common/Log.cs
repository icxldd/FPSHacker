using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace memoryHack.Common
{
    public class Log
    {
        public static void Debug(object obj)
        {
            string selfTime = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
            string data = JsonConvert.SerializeObject(obj);
            Console.WriteLine($"{selfTime}->{data}");
        }


    }
}
