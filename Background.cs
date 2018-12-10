using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MeowKun
{
    class Background
    {
        public Texture2D bgLayer1;
        public Vector2 bgpos1, bgpos2, bgpos3, bgpos4, bgpos5, bgpos6, bgpos7, bgpos8;
        public int speed1, speed2, speed3, speed4;
        public Background()
        {
            bgpos1 = new Vector2(0, 0);
            bgpos2 = new Vector2(1028, 0);
            bgpos3 = new Vector2(0, 0);
            bgpos4 = new Vector2(1028, 0);
            bgpos5 = new Vector2(0, 0);
            bgpos6 = new Vector2(1028, 0);
            bgpos7 = new Vector2(0, 0);
            bgpos8 = new Vector2(1028, 0);
            speed1 = 10;
            speed2 = 8;
            speed3 = 6;
            speed4 = 2;
        }
        public void LoadContent(ContentManager Content)
        {
            bgLayer1 = Content.Load<Texture2D>("Background//bgLayer1");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgLayer1, bgpos7, Color.White);
            spriteBatch.Draw(bgLayer1, bgpos8, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            bgpos1.X = bgpos1.X - speed1;
            bgpos2.X = bgpos2.X - speed1;
            bgpos3.X = bgpos3.X - speed2;
            bgpos4.X = bgpos4.X - speed2;
            bgpos5.X = bgpos5.X - speed3;
            bgpos6.X = bgpos6.X - speed3;
            bgpos7.X = bgpos7.X - speed4;
            bgpos8.X = bgpos8.X - speed4;
            //1-2
            if (bgpos1.X <= -1028)
            {
                bgpos1.X = 0;
                bgpos2.X = 1028;
            }
            //3-4
            if (bgpos3.X <= -1028)
            {
                bgpos3.X = 0;
                bgpos4.X = 1028;
            }
            //5-6
            if (bgpos5.X <= -1028)
            {
                bgpos5.X = 0;
                bgpos6.X = 1028;
            }
            //7-8
            if (bgpos7.X <= -1028)
            {
                bgpos7.X = 0;
                bgpos8.X = 1028;
            }
        }
    } 
}
