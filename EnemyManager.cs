using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeowKun
{
    class EnemyManager
    {
        //Enemies Description
        Texture2D enemyTexture;
        static public List<Enemy> enemiesType1=new List<Enemy>();
        
        //Handle the graphics info
        GraphicsDeviceManager graphics;
        //Rate at which the enemies will appear
        TimeSpan enemySpawnTime=TimeSpan.FromSeconds(1.0f);//Use to determine how fast enemy respawns;
        TimeSpan previousSpawnTime=TimeSpan.Zero;//SET THE TIME KEEPTERS TO ZERO;;
        //A random number generator for position to appear
        Random random=new Random();

        //Handle Graphics info
        Vector2 graphicsInfo;
        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
            enemyTexture = texture;
            enemiesType1.Clear();
        }
        private void AddEnemy()
        {
            //Create the animation object
            Animation enemyAnimation = new Animation();
            //Initialise the animation with the correct animation details
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 100, 100, 2, 30, Color.White, 1f, true);
            //Randomly generate the position of the enemy
            int newY = (int)graphicsInfo.Y-100;
            Vector2 position = new Vector2(
                graphicsInfo.X + enemyTexture.Width / 2, random.Next(50, newY - 50));
            //create an enemy
            Enemy enemy = new Enemy();
            enemy.Initialize(enemyAnimation, position);
            //add enemy in the list of enemies
            enemiesType1.Add(enemy);
        }
        public static void UpdateColission(Player player, ExplosionManager VFX, GUI guiInfo)
        {
            //use the Rectangle's build-in interscect function to determine if
            //two objects are overlapping
            Rectangle rect1, rect2;

            //Only create the rectangle once for the player
            rect1 = new Rectangle(
                (int)player.Position.X,
                (int)player.Position.Y,
                player.Width, player.Height);

            //Do the collision between the player and the enemies
            for (int i=0;i<enemiesType1.Count;i++)
            {
                rect2 = new Rectangle(
                    (int)enemiesType1[i].Position.X,
                    (int)enemiesType1[i].Position.Y,
                    enemiesType1[i].Width,
                    enemiesType1[i].Height);
                //Now determine if the two objects collide with each other
                if(rect1.Intersects(rect2))
                {
                    //Subtract the health from the player based on the enemy damage
                    player.Health -= enemiesType1[i].Damage;
                    //Since the enemy collided with the player destroy it
                    enemiesType1[i].Health = 0;
                    /// Show the explosion where the enemy was...
                    VFX.AddExplosion(enemiesType1[i].LocationEnemy);

                    guiInfo.SCORE = guiInfo.SCORE + 10;
                    //if the player health is less than zero then player must be destroyed
                    guiInfo.PlayerHP -= 25;

                    if (guiInfo.PlayerHP == 0 && guiInfo.LIVES > 0)
                    {
                        player.Active = false;
                        guiInfo.LIVES -= 1;
                        guiInfo.PlayerHP = 100;
                    }


                }

            }

        }
        public void UpdateEnemies(GameTime gameTime, Player player,ExplosionManager VFX, GUI guiInfo)
        {
            //spawn a new enemy every 1.5 sec
            if(gameTime.TotalGameTime-previousSpawnTime>enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                AddEnemy();
            }

            UpdateColission(player,VFX, guiInfo);
            //Update enemies
            for(int i=(enemiesType1.Count-1);i>=0;i--)
            {
                enemiesType1[i].Update(gameTime);
                if(enemiesType1[i].Active==false)
                { enemiesType1.RemoveAt(i); }
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            for(int i=0;i<enemiesType1.Count;i++)
            { enemiesType1[i].Draw(spriteBatch); }
        }

       /* public void ClearList()
        {
            enemiesType1.Clear();
        }*/
    }
}
