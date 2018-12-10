using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace MeowKun
{
    public class LaserManager
    {
        //Enemies Description
        static Texture2D laserTexture;
        static Rectangle laserRectangle;
        static public List<Laser> laserBeams;
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 200f;
        // govern how fast our laser can fire.
        static TimeSpan laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousLaserSpawnTime;
        Song song;
        SoundEffect sound_E;

        //Handle the graphics info
        GraphicsDeviceManager graphics;
        //Handle Graphics info
        static Vector2 graphicsInfo;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

//====================================================================================================================================================================================================================

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            laserBeams = new List<Laser>();
            previousLaserSpawnTime = TimeSpan.Zero;
            laserTexture = texture;
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
            laserBeams.Clear();
        }

//====================================================================================================================================================================================================================

        private static void FireLaser(GameTime gameTime, Player p)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime)
            {
                previousLaserSpawnTime = gameTime.TotalGameTime;
                // Add the laser to our list.
                AddLaser(p);


            }
        }

//====================================================================================================================================================================================================================

        private static void AddLaser(Player p)
        {
            Animation laserAnimation = new Animation();
            // initlize the laser animation
            laserAnimation.Initialize(laserTexture, p.Position,
                46,
                30,
                1,
                30,
                Color.White,
                1f,
                true);
            Laser laser = new Laser();
            // Get the starting postion of the laser.
            var laserPostion = p.Position;
            // Adjust the position slightly to match the muzzle of the cannon.
            laserPostion.Y += 37;
            laserPostion.X += 70;
            // init the laser
            laser.Initialize(laserAnimation, laserPostion);
            laserBeams.Add(laser);
            /* todo: add code to create a laser. */
            // laserSoundInstance.Play();
        }

//====================================================================================================================================================================================================================

        public void UpdateManagerLaser(GameTime gameTime, Player p, ExplosionManager vfx, GUI guiInfo, SoundEffect effect)
        {
            //Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                FireLaser(gameTime, p);
                //effect.Play();
            }

            // update laserbeams
            for (var i = 0; i < laserBeams.Count; i++)
            {
                laserBeams[i].Update(gameTime);
                // Remove the beam when its deactivated or is at the end of the screen.
                if (!laserBeams[i].Active || laserBeams[i].Position.X > graphicsInfo.X)
                {
                    laserBeams.Remove(laserBeams[i]);
                }
            }

            // detect collisions between the player and all enemies.
            foreach (Enemy e in EnemyManager.enemiesType1)
            {
                //create a retangle for the enemy
                Rectangle enemyRectangle = new Rectangle(
                    (int)e.Position.X,
                    (int)e.Position.Y,
                    e.Width,
                    e.Height);

                // now see if this enemy collide with any laser shots
                foreach (Laser L in LaserManager.laserBeams)
                {
                    // create a rectangle for this laserbeam
                    laserRectangle = new Rectangle(
                    (int)L.Position.X,
                    (int)L.Position.Y,
                    L.Width,
                    L.Height);

                    // test the bounds of the laser and enemy
                    if (laserRectangle.Intersects(enemyRectangle))
                    {


                        // Show the explosion where the enemy was...
                        vfx.AddExplosion(e.Position);

                        // kill off the enemy
                        e.Health = 0;
                        guiInfo.SCORE = guiInfo.SCORE + 10;
                        //record the kill
                        //TBA

                        // kill off the laserbeam
                        L.Active = false;

                        // record your score
                        //TBA
                    }
                }
            }
        }


        public void DrawLasers(SpriteBatch spriteBatch, SoundEffect effect)
        {
            // Draw the lasers.
            foreach (var l in laserBeams)
            {
                l.Draw(spriteBatch);
                effect.Play();
            }
        }

       /* public void ClearList()
        {
            laserBeams.Clear();
        }*/
  
    }
}
