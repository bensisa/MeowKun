﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowKun
{
    public  class GUI
    {
        private int score;
        private int playerhp;
        private int lives;
        private int gameLevel;
        private int levelUpgrade = 200;
        //more to add on your choice!!

//====================================================================================================================================================================================================================

        public void Initialize(int Score, int HP,int Lives,int Level)
        {
            score = Score;
            playerhp = HP;
            lives = Lives;
            gameLevel = Level;
        }

//====================================================================================================================================================================================================================

        public int SCORE
        {
            get { return score; }
            set { this.score=value; }
        }

//====================================================================================================================================================================================================================
        public int PlayerHP
        {
            get { return playerhp; }
            set { this.playerhp = value; }
        }

//====================================================================================================================================================================================================================

        public int LIVES
        {
            get { return lives; }
            set { this.lives = value; }
        }

//====================================================================================================================================================================================================================

        public int LEVEL
        {
            get { return gameLevel; }
            set { this.gameLevel = value; }
        }

//====================================================================================================================================================================================================================

        public int LEVELUPGRADE
        {
            get { return levelUpgrade; }
            set { this.levelUpgrade = value; }
        }

//====================================================================================================================================================================================================================
    }
}
