using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMono.Core.Managers
{
    public static class ColorManager
    {
        public static Color GetColor(string color)
        {
            if(color == "black")
            {
                return Color.Black;
            } 
            if (color == "white")
            {
                return Color.White;
            }
            if (color == "red")
            {
                return Color.Red;
            }
            if (color == "green")
            {
                return Color.Green;
            }
            if (color == "blue")
            {
                return Color.Blue;
            }
            if (color == "yellow")
            {
                return Color.Yellow;
            }
            if (color == "pink")
            {
                return Color.Pink;
            }
            if (color == "purple")
            {
                return Color.Purple;
            }
            if (color == "turquoise")
            {
                return Color.Turquoise;
            }
            if (color == "gray")
            {
                return Color.Gray;
            }
            if (color == "brown")
            {
                return Color.Brown;
            }
            return Color.White;
        }
    }
}
