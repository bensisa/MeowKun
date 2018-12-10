using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace MeowKun
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

//====================================================================================================================================================================================================================
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Represents the player
        Player player= new Player();
        // TODO: Add your initialization logic here
    
        // Represents the player
        Texture2D playerTexure; // Player texture sheet
        Vector2 playerPosition; // Player World Position
        float scale = 1f; // Scale for the player texture

        // Image used to display the static background
        Texture2D mainBackground;
        Texture2D secondBackground;
        Texture2D thirdBackground;
        Rectangle rectBackground;
        Background bgLayer1, bgLayer2;
        Background2 bgLayer3;
        Background3 bgLayer4;
        SoundEffect soundE;
        Song song;

        int Level;

        //enemy
        Texture2D enemyTexture;
        EnemyManager Balloons = new EnemyManager();

        GraphicsDevice details;

        Vector2 graphicsInfo;

        //Laser details
        // texture to hold the laser.
        Texture2D laserTexture;
        // Controls all the laser beams
        LaserManager LazerBeams = new LaserManager();

        // texture to hold the EXPLOSIONS.
        Texture2D vfx;
        //Controls all the explosion
        ExplosionManager VFX = new ExplosionManager();

       

        //Supporting GUI
        SpriteFont guiFont, MenuFont;
        Texture2D legand;
        GUI guiInfo=new GUI();
        Texture2D playerGUI;


        // Code for GAME WRAPPER PART
        enum GameStates { TitleScreen,start,Playing,WaveComplete,GameOver};
        GameStates gameState = GameStates.TitleScreen;
        float gameOverTimer = 0.0f;
        float gameOverDelay = 5.0f;
        float waveCompleteTimer = 0.0f;
        float waveCompleteDelay = 4.0f;
        Texture2D titleScreen, gameover;

//====================================================================================================================================================================================================================
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

//====================================================================================================================================================================================================================
        protected override void Initialize()
        {
            //Background
            bgLayer1 = new Background();
            bgLayer2 = new Background();
            bgLayer3 = new Background2();
            bgLayer4 = new Background3();
            base.Initialize();
        }
    
//====================================================================================================================================================================================================================
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphicsInfo.X = GraphicsDevice.Viewport.Width;
            graphicsInfo.Y = GraphicsDevice.Viewport.Height;   
               
            // Load the parallaxing background
            mainBackground = Content.Load<Texture2D>("Background//bgLayer1");
            bgLayer1.LoadContent(Content);
            bgLayer2.LoadContent(Content);


            secondBackground = Content.Load<Texture2D>("Background//bgLayer3");
            bgLayer3.LoadContent(Content);

            thirdBackground = Content.Load<Texture2D>("Background//bgLayer4");
            bgLayer4.LoadContent(Content);


            playerTexure = Content.Load<Texture2D>("Graphics\\shipAnimation");
            enemyTexture = Content.Load<Texture2D>("Graphics\\mineAnimation");

            //LAZER
            laserTexture = Content.Load<Texture2D>("Graphics\\laser");   
               
            //Explosion Manager Constructor
            vfx = Content.Load<Texture2D>("Graphics\\explosion");

            //Supporting GUI elements
            guiFont = Content.Load<SpriteFont>("GUIFont");
            MenuFont = Content.Load<SpriteFont>("MenuFont");
            legand = Content.Load<Texture2D>("legand");
            playerGUI = Content.Load<Texture2D>("Graphics\\player"); 
                       
            //TITLESCREENS
            titleScreen=Content.Load<Texture2D>("Graphics\\mainMenu");
            gameover = Content.Load<Texture2D>("Graphics\\endMenu");
            soundE = Content.Load<SoundEffect>("Sounds\\fshfsh");
            song = Content.Load<Song>("Sounds\\gameMusic");

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

//====================================================================================================================================================================================================================
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>

//====================================================================================================================================================================================================================
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            bgLayer1.Update(gameTime);
            bgLayer2.Update(gameTime);
            bgLayer3.Update(gameTime);
            bgLayer4.Update(gameTime);

            //-------------------------------------------------------------------------------------------

            if (guiInfo.SCORE == 100)
            {
                Level = 1;
            }

            //-------------------------------------------------------------------------------------------

            if (guiInfo.SCORE == 300)
            {
                Level = 2;
            }

            //-------------------------------------------------------------------------------------------

            if (guiInfo.SCORE == 500)
            {
                Level = 0;
            }

            //-------------------------------------------------------------------------------------------

            switch (Level)
            {
                //---------------------------------------------------------------------------------------
                case 0:

                    if (Keyboard.GetState().IsKeyDown(Keys.L))
                    Level = 1;
                    break;
                //---------------------------------------------------------------------------------------
                case 1:

                    if (Keyboard.GetState().IsKeyDown(Keys.K))
                    Level = 2;
                    break;
                //---------------------------------------------------------------------------------------
                case 2:

                    if (Keyboard.GetState().IsKeyDown(Keys.J))
                    Level = 0;
                    break;
                 //-------------------------------------------------------------------------------------------
            }
            //-------------------------------------------------------------------------------------------

            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if(GamePad.GetState(PlayerIndex.One).Buttons.A==ButtonState.Pressed||
                        Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        StartNewGame();
                        gameState = GameStates.start;
                      
                    }
                    break;

                //-------------------------------------------------------------------------------------------

                case GameStates.start:
                        waveCompleteTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (waveCompleteTimer > waveCompleteDelay)
                        {
                            gameState = GameStates.Playing;
                            waveCompleteTimer = 0.0f;
                        }
                        break;

                //------------------------------------------------------------------------------------------- 
                
                case GameStates.Playing:
                        player.Update(gameTime);
                        Balloons.UpdateEnemies(gameTime, player, VFX, guiInfo);
                        LazerBeams.UpdateManagerLaser(gameTime, player, VFX, guiInfo, soundE);
                        VFX.UpdateExplosions(gameTime);

                        if (guiInfo.SCORE >= guiInfo.LEVELUPGRADE)
                        {
                            gameState = GameStates.WaveComplete;
                            guiInfo.LEVELUPGRADE = guiInfo.LEVELUPGRADE + (guiInfo.SCORE + 50);
                            guiInfo.LEVEL++;
                        }
                        break;

                //-------------------------------------------------------------------------------------------  

                case GameStates.WaveComplete:
                    waveCompleteTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(waveCompleteTimer>waveCompleteDelay)
                    {
                        StartNewLevel();
                        gameState = GameStates.Playing;
                        waveCompleteTimer = 0.0f;
                    }
                    break;

                //-------------------------------------------------------------------------------------------

                case GameStates.GameOver:
                    gameOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (gameOverTimer > gameOverDelay)
                    {
                        gameState = GameStates.TitleScreen;
                        gameOverTimer = 0.0f;
                    }
                    break;

                //-------------------------------------------------------------------------------------------
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
  
//====================================================================================================================================================================================================================
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing
            spriteBatch.Begin();

            switch (Level)
            {
                //-------------------------------------------------------------------------------------------

                case 0:
  
                    bgLayer3.Draw(spriteBatch);
                    break;

                //-------------------------------------------------------------------------------------------

                case 1:
                    
                   bgLayer1.Draw(spriteBatch);
                   bgLayer2.Draw(spriteBatch);
                   break;

                //-------------------------------------------------------------------------------------------

                case 2:

                    bgLayer4.Draw(spriteBatch);
                    break;

                //-------------------------------------------------------------------------------------------
            }

            //Draw the Main Background Texture
            spriteBatch.Draw(mainBackground, rectBackground, Color.White);
            // Draw the moving background
            //  bgLayer1.Draw(spriteBatch);
            //  bgLayer2.Draw(spriteBatch);
            //  bgLayer3.Draw(spriteBatch);

            //-------------------------------------------------------------------------------------------

            if (gameState==GameStates.TitleScreen)
            {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, titleScreen.Width, titleScreen.Height), Color.White);
            }

            //-------------------------------------------------------------------------------------------

            if (gameState == GameStates.start)
            {
                spriteBatch.DrawString(MenuFont, "Beginning Wave: " + guiInfo.LEVEL, new Vector2(300, 300), Color.White);
            }

            //-------------------------------------------------------------------------------------------

            if (gameState==GameStates.Playing|| gameState == GameStates.WaveComplete)
            {
                // Draw the Player
                player.Draw(spriteBatch);

                //Draw Enemies
                Balloons.DrawEnemies(spriteBatch);

                //Draw Lazers
                LazerBeams.DrawLasers(spriteBatch,soundE);

                //Draw Explosions
                VFX.DrawExplosions(spriteBatch);
                CheckPlayerDeath();

                //Drawing GUI elements
                spriteBatch.Draw(legand, new Vector2(0, 410), Color.White);
                spriteBatch.DrawString(guiFont, "Score: " + guiInfo.SCORE, new Vector2(10, 422), Color.White);
                spriteBatch.DrawString(guiFont, "Level:  " + guiInfo.LEVEL, new Vector2(10, 450), Color.White);
                spriteBatch.DrawString(guiFont, "HP: " + guiInfo.PlayerHP, new Vector2(200, 422), Color.White);

                for (int i=1;i<=guiInfo.LIVES;i++)
                {
                    spriteBatch.Draw(playerGUI, new Vector2(220 + i * 45, 395), Color.White);
                }
            }

            //-------------------------------------------------------------------------------------------

            if (gameState == GameStates.WaveComplete)
            {
                spriteBatch.DrawString(MenuFont, "Beginning Wave: " + guiInfo.LEVEL, new Vector2(300,300), Color.Yellow);
            }

            //-------------------------------------------------------------------------------------------

            if (gameState == GameStates.GameOver)
            {
                spriteBatch.Draw(gameover, new Rectangle(0, 0, titleScreen.Width, titleScreen.Height), Color.White);
            }

            //-------------------------------------------------------------------------------------------

            // Stop drawing
            spriteBatch.End();
            base.Draw(gameTime);
        }

//====================================================================================================================================================================================================================
        public void StartNewGame()
        {
            guiInfo.LEVEL = 0;
            guiInfo.SCORE = 0;
            guiInfo.LIVES = 3;
            guiInfo.LEVELUPGRADE = 200;
            guiInfo.PlayerHP = 100;        
            guiInfo.Initialize(0, 100, 3, 1);
            StartNewLevel();
        }

//====================================================================================================================================================================================================================
        public void StartNewLevel()
        {
            // START PLAYER DETAILS
            Animation playerAnimation = new Animation();
            playerAnimation.Initialize(playerTexure, Vector2.Zero, 115, 115, 4, 30, Color.White, scale, true);

            playerPosition = new Vector2(
                GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            player.Initialize(playerAnimation, playerPosition, graphicsInfo);

            details = GraphicsDevice;

            Balloons.Initialize(enemyTexture, details);

            LazerBeams.Initialize(laserTexture, details);

            VFX.Initialize(vfx, details);
        }

//====================================================================================================================================================================================================================
        private void CheckPlayerDeath()
        {
            if(guiInfo.LIVES<=0)
            {
                gameState = GameStates.GameOver;
            }
        }

//====================================================================================================================================================================================================================
    }
}

