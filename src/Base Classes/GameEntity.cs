using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace RathunterGame
{
    public abstract class GameEntity : GameObject
    {
        public const int DefaultScoreImpact = 10;

        private protected int _scoreImpact;
        private protected int _healthImpact;

        private int[] _spawn = new int[] { -150, 900 };
        Random _rnd = new Random();

        public GameEntity() // Randomises entity spawn position
        {
            _position.X = _spawn[_rnd.Next(_spawn.Length)]; // Allows entities to spawn from either right side or left side of window
            _position.Y = SplashKit.Rnd(SplashKit.ScreenHeight() - 200); // Randomises entity spawn height
            _scoreImpact = DefaultScoreImpact;
            _healthImpact = 0;
        }

        public int ScoreImpact
        {
            get { return _scoreImpact; }
        }

        public int HealthImpact
        {
            get { return _healthImpact; }
        }
    }
}
