using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace RathunterGame
{
    public abstract class GameObject
    {
        private protected Point2D _position;
        private protected int _health;

        public GameObject()
        {
            _position = SplashKit.ScreenCenter();
            _health = 1;
        }

        public abstract void Draw();

        public abstract void Update();

        public abstract bool IsAt(Point2D pt);

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
    }
}
