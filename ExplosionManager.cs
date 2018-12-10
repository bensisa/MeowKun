using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MeowKun
{
    public class ExplosionManager
    {
        // Collections of explosions
        List<Explosion> explosions;
        //Texture to hold explosion animation.
        Texture2D explosionTexture;
        //Handle Graphics info
        Vector2 graphicsInfo;

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
            // init our collection of explosions.
            explosions = new List<Explosion>();
            explosionTexture = texture;
            explosions.Clear();
        }
        public void AddExplosion(Vector2 enemyPosition)
        {
            Animation explosionAnimation = new Animation();

            explosionAnimation.Initialize(explosionTexture,
                enemyPosition,
                120,
                116,
                6,
                30,
                Color.White,
                1.0f,
                true);

            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, enemyPosition);

            explosions.Add(explosion);
            
        }

        public void UpdateExplosions(GameTime gameTime)
        {
            for (var e = 0; e < explosions.Count; e++)
            {
                explosions[e].Update(gameTime);

                if (!explosions[e].Active)
                    explosions.Remove(explosions[e]);
            }
        }

        public void DrawExplosions(SpriteBatch spriteBatch)
        {
            // draw explosions
            foreach (var e in explosions)
            {
                e.Draw(spriteBatch);
            }
        }

      /*  public void ClearList()
        {
            explosions.Clear();
        }*/
    }
}
