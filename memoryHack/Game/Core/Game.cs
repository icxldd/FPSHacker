using Memory;
using memoryHack.Common;
using memoryHack.Game.Entity;
using memoryHack.Game.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace memoryHack.Game.Core
{


    public interface IPeoson
    {
        /// <summary>
        /// 获取本人基地址
        /// </summary>
        /// <returns></returns>
        public IntPtr GetCurrentPeosonBasicAdress();
        /// <summary>
        /// 获取本人坐标
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCurrentPeosonPointer();
        /// <summary>
        /// 获取人物阵营
        /// </summary>
        /// <param name="peosonAdress"></param>
        /// <returns></returns>
        public int GetPeosonCamp(IntPtr peosonAdress);
        /// <summary>
        /// 获取人物血量
        /// </summary>
        /// <param name="peosonAdress"></param>
        /// <returns></returns>
        public int GetPeosonHP(IntPtr peosonAdress);
        /// <summary>
        /// 获取人物坐标
        /// </summary>
        /// <param name="peosonAdress"></param>
        /// <returns></returns>
        public Vector3 GetPeosonPointer(IntPtr peosonAdress);
        /// <summary>
        /// 获取敌人数量
        /// </summary>
        /// <returns></returns>
        public int GetEnemyCount();
        /// <summary>
        /// 获取敌人基地址
        /// </summary>
        /// <returns></returns>
        public IntPtr GetEnemyBasicAdress();

    }

    public interface IGame : IPeoson
    {
        /// <summary>
        /// 获取游戏句柄
        /// </summary>
        IntPtr GameHanld { get; set; }
        /// <summary>
        /// 内存访问
        /// </summary>
        Mem MemLib { get; set; }
        /// <summary>
        /// 获取游戏界面宽高
        /// </summary>
        Size GameSize { get; }
        /// <summary>
        /// 获取世界坐标转屏幕坐标视角
        /// </summary>
        Size WordToScreenSize { get; }

        /// <summary>
        /// 获取矩阵
        /// </summary>
        /// <returns></returns>
        float[,] GetWordMatrix();

    }

    public class Game : IGame
    {
        public Game(IntPtr _gameHanld, Mem mem)
        {
            this.GameHanld = _gameHanld;
            this.MemLib = mem;
            this.MemLib.GetModules();
        }

        /// <summary>
        /// 游戏size
        /// </summary>
        public Size GameSize
        {
            get
            {
                RECT _currenthanldRect;
                WinApi.GetWindowRect(this.GameHanld, out _currenthanldRect);
                return FpsHelper.RectScreenToSize(_currenthanldRect);
            }
        }
        /// <summary>
        /// 世界坐标转屏幕坐标视角
        /// </summary>
        public Size WordToScreenSize
        {
            get
            {
                Size GameSize_ = GameSize;
                GameSize_.Height /= 2;
                GameSize_.Width /= 2;
                return GameSize_;
            }
        }

        public IntPtr GameHanld { get; set; }
        public Mem MemLib { get; set; }
        public IntPtr GetModuleAdress(string name)
        {
            return MemLib.modules[name];
        }
        public IntPtr GetCurrentPeosonBasicAdress()
        {
            return GetModuleAdress(Offset.serverDllName) + Offset.currentPeosonAdress;
        }

        public Vector3 GetPeosonPointer(IntPtr peosonAdress)
        {
            float currentZ = MemLib.ReadFloat($"{peosonAdress.PointerToHex()},{Offset.pointerOffset.IntToHex()}");
            float currentY = MemLib.ReadFloat($"{peosonAdress.PointerToHex()},{(Offset.pointerOffset - 4).IntToHex()}");
            float currentX = MemLib.ReadFloat($"{peosonAdress.PointerToHex()},{(Offset.pointerOffset - 8).IntToHex()}");
            return new Vector3(currentX, currentY, currentZ);
        }

        public IntPtr GetEnemyBasicAdress()
        {
            return GetModuleAdress(Offset.serverDllName) + Offset.enemyPeosonAdress;
        }

        public int GetEnemyCount()
        {
            return MemLib.ReadInt($"{Offset.serverDllName}+{Offset.enemyCountOffset.IntToHex()}");
        }

        public int GetPeosonCamp(IntPtr peosonAdress)
        {
            return MemLib.ReadInt($"{(peosonAdress + Offset.campOffset).PointerToHex()}");
        }

        public int GetPeosonHP(IntPtr peosonAdress)
        {
            return MemLib.ReadInt($"{(peosonAdress + Offset.peosonHP).PointerToHex()}");
        }

        public Vector3 GetCurrentPeosonPointer()
        {
            return GetPeosonPointer(GetCurrentPeosonBasicAdress());
        }

        public float[,] GetWordMatrix()
        {
            var Rs = new float[4, 4];
            for (int I = 0; I < Rs.GetLength(0); I++)
            {
                for (int i = 0; i < Rs.GetLength(1); i++)
                {
                    Rs[I, i] = MemLib.ReadFloat(((GetModuleAdress(Offset.engineDllName) + Offset.matrixOffset) + (i + (I * 4)) * 4).PointerToHex());
                }
            }
            return Rs;
        }
    }
}
