using System;
using System.Collections.Generic;
using System.Text;

namespace memoryHack.Common
{
    public class AryConvert
    {

        /// <summary>
        /// 十进制转2进制
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DenaryToBinary(int source)
        {
            return Convert.ToString(source, 2);
        }

        /// <summary>
        /// 十进制转16进制
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DenaryToHex(int source)
        {
            return Convert.ToString(source, 16);
        }

        /// <summary>
        /// 二进制转10进制
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int BinaryToDenary(string source)
        {
            return Convert.ToInt32(source, 2);
        }


        /// <summary>
        /// 二进制转10进制
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int HexToDenary(string source)
        {
            return Convert.ToInt32(source, 16);
        }

    }
}
