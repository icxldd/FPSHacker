using memoryHack.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace memoryHack.Game.Global
{
    public static class ScaleConvertExtend
    {


        /// <summary>
        /// 十进制转16
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string IntToHex(this int source)
        {
            return AryConvert.DenaryToHex(source).ToString();
        }

        /// <summary>
        /// 十六进制显示
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string PointerToHex(this IntPtr source)
        {
            return Convert.ToString((int)source, 16);
        }


    }
}
