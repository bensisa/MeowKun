using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeowKun
{
    public class Player
    {
        // Animation representing the player
        public Animation PlayerAnimation;
        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;
        // State of the player
        public bool Active;
        // Amount of hit points that player has
        public int Health;


        //Hold the Viewport
        Vector2 graphicsInfo;
        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        //Mouse states used to track Mouse button press
        MouseState currentMouseState;
        MouseState previousMouseState;

        // A movement speed for the player
        float playerMoveSpeed;

        //Get the width and Height of the Ship
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }
        // Get the height of the player ship
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }
        public Vector2 PlayerPos
        {
            get { return Position; }
        }

        public void Initialize(Animation animation, Vector2 position, Vector2 grInfo)
        {
            PlayerAnimation = animation;

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;

            //Set the viewport
            graphicsInfo = grInfo;
            // Set a constant player move speed
            playerMoveSpeed = 4.0f;

        }
        public void Update(GameTime gameTime)
        {
            //Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            // Read the current state of the Mouse  and store it
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            //Get Mouse State then Capture the Button type and Respond Button Press
            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);


            //Get Thumbsticks Controls
            Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            Position.Y += currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            //Use the Keyboard/DPad
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                Position.Y += playerMoveSpeed;
            }


            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - Position;
                posDelta.Normalize();
                posDelta = posDelta * playerMoveSpeed;
                Position = Position + posDelta;
            }

            //Restrict the POsition to remain with the screen bandwidth
            Position.X = MathHelper.Clamp(Position.X, 0, 650);
            Position.Y = MathHelper.Clamp(Position.Y, 0, graphicsInfo.Y - Height - 60);

            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);


        }
        public void Draw(SpriteBatch spriteBatch)
        {

            PlayerAnimation.Draw(spriteBatch);

        }

    }
}