using memoryHack.Game.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace memoryHack.Game.Global
{
    public class SingleWordChace
    {

        private Vector3 _currentLocation;

        /// <summary>
        /// 当前角色xy地址
        /// </summary>
        public Vector3 CurrentLocation
        {
            get
            {

                return _currentLocation;
            }
            set
            {
                _currentLocation = value;
            }
        }

        private Vector2 _currentMouseLocation;

        /// <summary>
        /// 当前角色鼠标xy地址
        /// </summary>
        public Vector2 CurrentMouseLocation
        {
            get
            {

                return _currentMouseLocation;
            }
            set
            {
                _currentMouseLocation = value;
            }
        }


        public override string ToString()
        {
            return $"当前角色人物坐标-》X位置:{CurrentLocation.X},Y位置：{CurrentLocation.Y},Z位置：{CurrentLocation.Z}。当前人物鼠标坐标-》X位置：{CurrentMouseLocation.X}，Y位置：{CurrentMouseLocation.Y}";
        }


    }
}
