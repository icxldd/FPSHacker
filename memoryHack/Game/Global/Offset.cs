using System;
using System.Collections.Generic;
using System.Text;

namespace memoryHack.Game.Global
{
    public class Offset
    {
        //服务器类
        public const string serverDllName = "server.dll";

        //客户端类
        public const string clientDllName = "client.dll";

        //引擎类
        public const string engineDllName = "engine.dll";

        /// <summary>
        /// 矩阵偏移
        /// </summary>
        public const int matrixOffset = 5861104;

        /// <summary>
        /// 自己人物基地址server+4F2FEC
        /// </summary>
        public const int currentPeosonAdress = 0x4F2FEC;

        /// <summary>
        /// 敌人人物基地址server +0x4F2FFC
        /// </summary>
        public const int enemyPeosonAdress = 0x4F2FFC;

        /// <summary>
        /// 人物阵营 人物基地址+阵营偏移
        /// </summary>
        public const int campOffset = 500;

        /// <summary>
        /// 敌人人数 服务器类+偏移
        /// </summary>
        public const int enemyCountOffset = 0x588878;

        /// <summary>
        /// 人物血量 人物地址+偏移
        /// </summary>
        public const int peosonHP = 228;

        /// <summary>
        /// 人物z偏移
        /// </summary>
        public const int pointerOffset = 0x288;


    }
}
