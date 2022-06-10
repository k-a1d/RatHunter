using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace RathunterGame
{
    public class Rock : GameEntity
    {
        public Rock()
        {
            _position.X = 100; 
            _position.Y = 0;
        }

        private Bitmap RockBitmap() // Maps images to each enumerator
        {
            return SplashKit.LoadBitmap("Rock", "Rock.png");
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(RockBitmap(), _position.X, _position.Y, SplashKit.OptionScaleBmp(0.3, 0.3));
        }

        public override void Update()
        {
            SplashKit.DrawBitmap(RockBitmap(), _position.X, _position.Y);
            Console.WriteLine("test"); // Test
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.BitmapPointCollision(RockBitmap(), _position, pt);
        }
    }
}
