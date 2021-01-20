using memoryHack.Game.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace memoryHack.Common
{
    public class FpsHelper
    {



        /// <summary>
        /// RECT to 窗口宽高
        /// </summary>
        /// <param name="_currenthanldRect"></param>
        /// <returns></returns>
        public static Size RectScreenToSize(RECT _currenthanldRect)
        {
            return new Size(_currenthanldRect.right - _currenthanldRect.left, _currenthanldRect.bottom - _currenthanldRect.top);
        }

        /// <summary>
        /// 获取敌人距离
        /// </summary>
        /// <param name="current"></param>
        /// <param name="Enemy"></param>
        /// <returns></returns>
        public static double GetEnemyDist(Vector3 current, Vector3 Enemy)
        {

            double distX = current.X - Enemy.X;

            double distY = current.Y - Enemy.Y;

            return Math.Round(Math.Sqrt(distX * distX + distY * distY) / 10, 0);

        }

        /// <summary>
        /// 世界地址转屏幕地址
        /// </summary>
        /// <param name="comuterScreen"></param>
        /// <param name="enemyPointer"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public static ScreenRectangle WordToScreen(Size comuterScreen, Vector3 enemyPointer, float[,] words)
        {
            /*
             .版本 2

        ' 竖矩阵算法
        相机Z ＝ ViewWorld [3] [1] × 敌人坐标.x ＋ ViewWorld [3] [2] × 敌人坐标.y ＋ ViewWorld [3] [3] × 敌人坐标.z ＋ ViewWorld [3] [4]
        缩放比例 ＝ 1 ÷ 相机Z
        .如果真 (相机Z ＜ 0)
            到循环尾 ()  ' break
        .如果真结束
        相机X ＝ 视角宽 ＋ (ViewWorld [1] [1] × 敌人坐标.x ＋ ViewWorld [1] [2] × 敌人坐标.y ＋ ViewWorld [1] [3] × 敌人坐标.z ＋ ViewWorld [1] [4]) × 缩放比例 × 视角宽
        相机Y ＝ 视角高 － (ViewWorld [2] [1] × 敌人坐标.x ＋ ViewWorld [2] [2] × 敌人坐标.y ＋ ViewWorld [2] [3] × (敌人坐标.z － 8) ＋ ViewWorld [2] [4]) × 缩放比例 × 视角高
        相机Y2 ＝ 视角高 － (ViewWorld [2] [1] × 敌人坐标.x ＋ ViewWorld [2] [2] × 敌人坐标.y ＋ ViewWorld [2] [3] × (敌人坐标.z ＋ 78) ＋ ViewWorld [2] [4]) × 缩放比例 × 视角高
        方框高度 ＝ 相机Y － 相机Y2
        方框宽度 ＝ 方框高度 × 0.526515151552

             */
            int viewW = comuterScreen.Width;
            int viewH = comuterScreen.Height;

            float cameraZ = words[2, 0] * enemyPointer.X + words[2, 1]
                * enemyPointer.Y + words[2, 2] * enemyPointer.Z + words[2, 3];
            float zoomRate = 1 / cameraZ;
            if (cameraZ < 0)
            {
                return new ScreenRectangle();
            }
            else
            {
                float cameraX = viewW + (words[0, 0] * enemyPointer.X +
                    words[0, 1] * enemyPointer.Y + words[0, 2] *
                    enemyPointer.Z + words[0, 3]) * zoomRate * viewW;
                float cameraY = viewH -
                    (words[1, 0] * enemyPointer.X +
                    words[1, 1] * enemyPointer.Y + words[1, 2] *
                    (enemyPointer.Z - 8) + words[1, 3])
                    * zoomRate * viewH;
                float cameraY2 = viewH - (words[1, 0] * enemyPointer.X +
                    words[1, 1] * enemyPointer.Y + words[1, 2] *
                    (enemyPointer.Z + 78) + words[1, 3]) * zoomRate * viewH;

                //方框高度
                float boxHeight = cameraY - cameraY2;
                //方框宽度
                float boxWidth = boxHeight * 0.526515151552f;
                float boxX = cameraX - boxWidth / 2;
                float boxY = cameraY;
                float boxY2 = cameraY2;
                return new ScreenRectangle(boxX, boxY, boxY2, boxWidth, boxHeight);

            }

        }

    }
}
