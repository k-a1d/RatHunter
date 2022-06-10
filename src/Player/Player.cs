using System;
using System.Collections.Generic;
using System.Text;

namespace RathunterGame
{
    public class Player
    {
        private int _health;
        private int _score;
        private string _finalGrade;

        public Player()
        {
            _health = 5;
            _score = 0;
            _finalGrade = null;
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public string FinalGrade
        {
            get { return _finalGrade; }
            set { _finalGrade = value; }
        }
    }
}
