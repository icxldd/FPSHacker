using System;
using System.Threading.Tasks;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using Memory;
using memoryHack.Common;
using memoryHack.Game.Entity;
using memoryHack.Game.Global;
using memoryHack.Sample;

namespace memoryHack
{
    class Program
    {

        async static Task Main(string[] args)
        {
            await Task.Run(() =>
            {
                WinApi.EnableDebugPriv();

                MainDarw();
            });

            Console.ReadLine();

        }

        private static void memTest()
        {
            MemLib.OpenProcess("hl2");
            MemLib.GetModules();


            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(MemLib.modules));

        }

        public static Mem MemLib = new Mem();
        public static SingleWordChace WordChace = new SingleWordChace();





        private static SolidBrush _red;
        private static Font _font;
        private static Graphics _graphics;
        private static GraphicsWindow _window;

        private static Size _currentHanldSize;
        private static RECT _currenthanldRect;
        private static IntPtr _hanld;
        private static Game.Core.Game _game;
        private static void MainDarw()
        {
            MemLib.OpenProcess("hl2");
            _hanld = WinApi.FindWindow("Valve001", "Counter-Strike Source");


            _game = new Game.Core.Game(_hanld, MemLib);


            WinApi.GetWindowRect(_hanld, out _currenthanldRect);
            _currentHanldSize = new Size(_currenthanldRect.right - _currenthanldRect.left, _currenthanldRect.bottom - _currenthanldRect.top);
            _graphics = new Graphics(_hanld, _currentHanldSize.Width, _currentHanldSize.Height);
            _window = new GraphicsWindow(_currenthanldRect.left, _currenthanldRect.top, _currentHanldSize.Width, _currentHanldSize.Height, _graphics)
            {
                FPS = 60,
                IsTopmost = true,
                IsVisible = true
            };
            _window.DestroyGraphics += _window_DestroyGraphics;
            _window.DrawGraphics += _window_DrawGraphics;
            _window.SetupGraphics += _window_SetupGraphics;
            _window.PositionChanged += _window_PositionChanged;
            _window.Create();
            _window.Join();





        }

        private static void _window_PositionChanged(object sender, OverlayPositionEventArgs e)
        {


        }

        private static void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            _graphics.EndScene();
        }
        private static void UpdateDrawSize()
        {
            WinApi.GetWindowRect(_hanld, out _currenthanldRect);
            _currentHanldSize = new Size(_currenthanldRect.right - _currenthanldRect.left, _currenthanldRect.bottom - _currenthanldRect.top);
            _window.X = _currenthanldRect.left + 5;
            _window.Y = _currenthanldRect.top + 25;
            _window.Width = _currentHanldSize.Width;
            _window.Height = _currentHanldSize.Height;
        }


        private static void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            //_graphics.DrawText(_font, _red, _currentHanldSize.Width / 2, _currentHanldSize.Height / 2, "icxl nb");
            //_graphics.DrawLine(_red, 0, 0, 100, 100, 1);
            //_graphics.DrawRectangleEdges(_red, 20, 20, 80, 150, 2);
            //_graphics.DrawRectangle(_red, 20, 20, 80, 150, 2);
            //float currentZ = MemLib.ReadFloat("server.dll+004F2FEC,0x288");
            //float currentY = MemLib.ReadFloat($"server.dll+004F2FEC,{AryConvert.DenaryToHex(0x288 - 4)}");
            //float currentX = MemLib.ReadFloat($"server.dll+004F2FEC,{AryConvert.DenaryToHex(0x288 - 8)}");
            //WordChace.CurrentLocation = new Vector3(currentX, currentY, currentZ);
            //float currentMouseY = MemLib.ReadFloat("engine.dll+4622CC");
            //float currentMouseX = MemLib.ReadFloat($"engine.dll+{AryConvert.DenaryToHex(0x4622CC + 4)}");
            //WordChace.CurrentMouseLocation = new Vector2(currentMouseX, currentMouseY);
            UpdateDrawSize();
            _graphics.ClearScene();

            //矩阵
            var matrixArray = _game.GetWordMatrix();
            //矩阵所需屏幕宽高
            Size wordScreen = _game.WordToScreenSize;
            //本人基地址-》已指针
            IntPtr currentBasicAdress = (IntPtr)MemLib.ReadInt(_game.GetCurrentPeosonBasicAdress().PointerToHex());
            //本人阵营
            int currentCamp = _game.GetPeosonCamp(currentBasicAdress);
            //本人坐标
            var currentPointer = _game.GetPeosonPointer(_game.GetCurrentPeosonBasicAdress());
            //敌人人数
            int enemyCount = _game.GetEnemyCount();

            for (int i = 0; i < enemyCount; i++)
            {

                IntPtr selfEnemy = _game.GetEnemyBasicAdress() + (i) * 16;
                //敌人基地址-》已指针
                IntPtr enemyBasicAdress = (IntPtr)MemLib.ReadInt((selfEnemy).PointerToHex());
                int enemyCamp = _game.GetPeosonCamp(enemyBasicAdress);
                if (currentCamp == enemyCamp)
                {
                    continue;
                }
                int currentHP = _game.GetPeosonHP(enemyBasicAdress);

                if (currentHP <= 1)
                {
                    continue;
                }
                var enemyPointer = _game.GetPeosonPointer(selfEnemy);

                var _screenRectangle = FpsHelper.WordToScreen(wordScreen, enemyPointer, matrixArray);
                if (_screenRectangle.W == 0)
                    continue;
                double enemyDist = FpsHelper.GetEnemyDist(currentPointer, enemyPointer);


                _graphics.DrawRectangle(_red, _screenRectangle.X, _screenRectangle.Y2, _screenRectangle.X+ _screenRectangle.W, _screenRectangle.Y2+ _screenRectangle.H, 2);
                _graphics.DrawText(_font, _red, _screenRectangle.X, _screenRectangle.Y, $"距离:{enemyDist}");
                _graphics.DrawLine(_red, wordScreen.Width, 0, _screenRectangle.X, _screenRectangle.Y2, 2);




            }


        }

        private static void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            _red = _graphics.CreateSolidBrush(255, 99, 71);
            _font = _graphics.CreateFont("Microsoft YaHei", 10);
            _graphics.BeginScene();

        }


    }
}
