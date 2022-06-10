using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace RathunterGame
{
    public class Boss : GameEntity
    {

        public Boss()
        {
            _position.X = 100; // Spawns boss in the center of the screen
            _position.Y = 0;
            _health = 100;
            _scoreImpact = 1000;
        }

        private Bitmap BossBitmap() // Maps images to each enumerator
        {
            return SplashKit.LoadBitmap("Rat King", "RatKing.png");
        }

        public override void Draw() // Draws boss and it's health bar
        {
            SplashKit.DrawBitmap(BossBitmap(), _position.X, _position.Y);
            SplashKit.DrawText("Boss HP: ", Color.White, "Algerian", 32, 250, 70);
            SplashKit.FillRectangle(Color.Red, 350, 65, _health * 3.5, 20);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.BitmapPointCollision(BossBitmap(), _position, pt);
        }

        public override void Update()
        {
        }
    }
}
