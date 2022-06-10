using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace RathunterGame
{
    public class Animal : GameEntity
    {
        private const int Speed = 8; // Default speed for small rat

        private AnimalType _animal;
        private double _speed;
        private bool _spawnedLeftSide;

        public Animal()
        {
            _animal = (AnimalType)SplashKit.Rnd(5); // Randomises animal type
            _speed = Speed;
            _spawnedLeftSide = false;
        }

        private Bitmap AnimalBitmap() // Maps images to correct enum element
        {
            switch (_animal)
            {
                case AnimalType.smallRat:
                    return SplashKit.LoadBitmap("Small rat", "Smallrat.png");
                case AnimalType.mediumRat:
                    return SplashKit.LoadBitmap("Medium rat", "Mediumrat.png");
                case AnimalType.largeRat:
                    return SplashKit.LoadBitmap("Large rat", "Largerat.png");
                case AnimalType.dangerousRat:
                    return SplashKit.LoadBitmap("Dangerous Rat", "Dangerousrat.png");
                case AnimalType.hamster:
                    return SplashKit.LoadBitmap("Hamster", "Hamster.png");
                default:
                    return SplashKit.LoadBitmap("Small rat", "Smallrat.png");
            }
        }

        public void Assign() // Updates the local variables depending upon the enumerator
        {
            switch (_animal)
            {
                case AnimalType.mediumRat:
                    _scoreImpact = DefaultScoreImpact * 2;
                    _speed = Speed * 0.85;
                    _health = 3;
                    break;
                case AnimalType.largeRat:
                    _scoreImpact = DefaultScoreImpact * 3;
                    _speed = Speed * 0.7;
                    _health = 5;
                    break;
                case AnimalType.dangerousRat:
                    _scoreImpact = 0;
                    _speed = Speed * 1.1;
                    _healthImpact = -1;
                    break;
                case AnimalType.hamster:
                    _scoreImpact = -50;
                    break;
                default:
                    break;
            }
        }

        public void IdentifySpawn()
        {
            if (_position.X == -150) // Left edge of screen
                _spawnedLeftSide = true;
        }

        public override void Draw() // Draws itself
        {
            SplashKit.DrawBitmap(AnimalBitmap(), _position.X, _position.Y);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.BitmapPointCollision(AnimalBitmap(), _position, pt);
        }

        public override void Update() // Moves itself to other side of screen
        {
            if (_spawnedLeftSide == true)
                _position.X += _speed;
            else 
                _position.X -= _speed;
        }
    }
}
