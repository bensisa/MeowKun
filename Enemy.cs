using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeowKun
{
    public class Enemy
    {
        public Animation EnemyAnimation;
        public Vector2 Position;
        public bool Active;
        public int Health;
        public int Damage;
        public int Value;
        float enemyMoveSpeed;

        public int Width
        {
            get { return EnemyAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return EnemyAnimation.FrameHeight; }
        }
        public Vector2 LocationEnemy
        {
            get { return Position; }
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            EnemyAnimation = animation;
            Position = position;
            Active = true;
            Health = 10;
            Damage = 10;
            enemyMoveSpeed = 6f;
            Value = 100;
        }
        public void Update(GameTime gameTime)
        {
            Position.X -= enemyMoveSpeed;
            EnemyAnimation.Position = Position;
            EnemyAnimation.Update(gameTime);
            if(Position.X<-Width||Health<=0)
            { Active = false; }
 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyAnimation.Draw(spriteBatch);
        }
    }
}
