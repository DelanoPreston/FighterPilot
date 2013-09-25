using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GameLibrary
{
    public class InputHandlerMouse : Microsoft.Xna.Framework.GameComponent
    {
        #region Field
        static MouseState mouseState;
        static MouseState lastMouseState;
        #endregion

        #region Property
        public static MouseState MouseState
        {
            get { return mouseState; }
        }
        public static MouseState LastMouseState
        {
            get { return lastMouseState; }
        }
        #endregion

        #region Constructor
        public InputHandlerMouse(Game game)
            : base(game)
        {
            mouseState = Mouse.GetState();
        }
        #endregion

        #region XNA methods
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
            base.Update(gameTime);
        }
        #endregion

        #region General Method
        public static void Flush()
        {
            lastMouseState = mouseState;
        }
        #endregion

        #region State Methods
        public static bool LeftButtonReleased(Keys key)
        {
            return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }
        public static bool LeftButtonPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
        }
        public static bool LeftButtonDown(Keys key)
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }
        public static bool RightButtonReleased(Keys key)
        {
            return mouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed;
        }
        public static bool RightButtonPressed(Keys key)
        {
            return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;
        }
        public static bool RightButtonDown(Keys key)
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }
        #endregion
    }
}