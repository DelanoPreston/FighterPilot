using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    public static class UtilityFunctions
    {
        public static float CalculateDistance(Vector2 inLocation1, Vector2 inLocation2)
        {
            inLocation1 = new Vector2(Math.Abs(inLocation1.X), Math.Abs(inLocation1.Y));//absolute values
            inLocation2 = new Vector2(Math.Abs(inLocation2.X), Math.Abs(inLocation2.Y));

            float yDiff, xDiff, distance;

            yDiff = inLocation1.Y - inLocation2.Y;
            xDiff = inLocation1.X - inLocation2.X;

            distance = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            return distance;
        }
        public static float CalculateAngle(Vector2 fromLocation, Vector2 toLocation)
        {
            float angle = (float)(Math.Atan2((double)(toLocation.Y - fromLocation.Y), (double)(toLocation.X - fromLocation.X)));
            return angle + (float)(Math.PI / 2);
        }
        public static Vector2 GetFrameCenter(Texture2D texture)
        {
            int frameWidth = (int)GetFrameDimensions(texture).X;
            int frameHeight = (int)GetFrameDimensions(texture).Y;
            Rectangle textureSize = new Rectangle(0, 0, frameWidth, frameHeight);
            return new Vector2(frameWidth / 2, frameHeight / 2);
        }
        public static Rectangle GetTexSizeRec(Texture2D texture)
        {
            return new Rectangle(0, 0, texture.Width, texture.Height);
        }
        public static Vector2 GetFrameDimensions(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
    }
}
