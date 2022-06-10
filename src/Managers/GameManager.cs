using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace RathunterGame
{
    public class GameManager
    {
        private const int MaxSpawns = 50; // Maximum number of total animals

        private List<GameObject> _gameObjects;
        private bool _enableSpawns;
        private bool _enableBoss;
        private int _maxCount;

        private int _numberOfSpawnsPerBatch; // The maximum number of spawns per 'batch'
        private int _maxSpawns; 

        private Timer _timeBetweenSpawnsTimer = SplashKit.CreateTimer("Spawn Timer"); 
        private Timer _numberOfSpawnsPerBatchTimer = SplashKit.CreateTimer("SpawnDifficulty"); // Gradually increases spawn amount
        private Timer _rockTimer = SplashKit.CreateTimer("RockThrow");

        public GameManager()
        {
            _gameObjects = new List<GameObject>();
            _enableSpawns = true;
            _enableBoss = false;
            _numberOfSpawnsPerBatch = 2;
            _maxSpawns = MaxSpawns;
            _timeBetweenSpawnsTimer.Start();
            _numberOfSpawnsPerBatchTimer.Start();
        }

        private Font f()
        {
            return SplashKit.LoadFont("Pixel", "FFFFORWA.TTF");
        }

        private Bitmap CrosshairBitmap()
        {
            return SplashKit.LoadBitmap("Crosshair", "Crosshair.png");
        }

        private Bitmap Background()
        {
            return SplashKit.LoadBitmap("Sewer", "Sewer.png");
        }

        public void DrawBackground()
        {
            SplashKit.DrawBitmap("Sewer.png", 0, 0);
        }

        public void DrawObjects() // Draws all game entities on the screen
        {
            foreach (GameObject obj in _gameObjects)
            {
                obj.Draw();
            }
        }

        public void DrawPlayerDetails(Player p) // Draws player statistics onto game screen
        {
            SplashKit.DrawText("Health: " + p.Health, Color.White,  "Pixel" ,32, 5, 10);
            SplashKit.DrawText("Score: " +  p.Score, Color.White, "Algerian", 32, 100, 10);
            SplashKit.DrawBitmap("Crosshair.png", SplashKit.MouseX() - 195, SplashKit.MouseY() - 182, SplashKit.OptionScaleBmp(0.3, 0.3));
            SplashKit.DrawText("Animals left: " + (Math.Max(0, _maxSpawns - _maxCount)), Color.White, "Algerian", 32, 850, 10);
        }

        public void Update() // Updates entities
        {
            foreach (GameObject obj in _gameObjects)
            {
                obj.Update();
            }

            // Increases draw rate of entities, as game progresses
            if(_numberOfSpawnsPerBatchTimer.Ticks >= 5000)
            {
                // Add line to display that difficulty has increased
                _numberOfSpawnsPerBatch++;
                _numberOfSpawnsPerBatchTimer.Reset();
            }

            if(_rockTimer.Ticks >= 5000)
            {
                Rock r = new Rock();
                _gameObjects.Add(r);
                _rockTimer.Reset();
            }
        }

        public void DrawWinScreen()
        {
            SplashKit.DrawText("You have defeated the Rat King. Congratulations.", Color.White, "Algerian", 32, SplashKit.ScreenWidth() / 2, SplashKit.ScreenHeight() / 2);
        }
        
        public void DrawFailScreen()
        {
            SplashKit.DrawText("You have failed to stop the rats.", Color.White, "Algerian", 32, SplashKit.ScreenWidth() / 2, SplashKit.ScreenHeight() / 2);
        }

        public void DrawBoss()
        {
            switch (_enableBoss)
            {
                case true:
                    Boss b = new Boss();
                    _gameObjects.Add(b);
                    _rockTimer.Start(); // Starts timer
                    _enableBoss = false;
                    break;
                default:
                    break;
            }
        }

        public void SpawnAnimals()
        {
            if (_enableSpawns)
                if (_timeBetweenSpawnsTimer.Ticks >= 3000) // Can only spawn new animals once the timer has reached certain value
                {
                    for (int i = 0; i <= SplashKit.Rnd(0, _numberOfSpawnsPerBatch); i++) // Loops for a specified amount of times
                    {
                        Animal a = new Animal();
                        a.Assign(); // Updates variables corresponding to enumerators
                        a.IdentifySpawn();
                        _gameObjects.Add(a);
                        _maxCount++;

                        _timeBetweenSpawnsTimer.Reset(); ; // Resets time between each set of spawned animals
                    }
                }
                if (_maxCount >= _maxSpawns) // Stops game from spawning animals once a certain amount have spawned
                {
                    _enableSpawns = false;
                    _enableBoss = true;
                }
        }

        public void Remove(Player p, Point2D pt) // Removes objects at specific position
        {
            List<GameObject> _remove = new List<GameObject>();

            foreach(GameObject obj in _gameObjects)
            {
                if (obj.IsAt(pt))
                {
                    obj.Health -= 1; // Removes one health point from the entity clicked

                    if(obj.Health <= 0) // Once the object's health drops to 0 it is removed
                    {
                        _remove.Add(obj);
                    }
                }
            }

            foreach(GameObject obj in _remove) // Removes object from screen
            {
                if (obj is GameEntity)
                {
                    GameEntity entity = (GameEntity)obj;
                    p.Score += entity.ScoreImpact;
                    p.Health += entity.HealthImpact;
                }

                _gameObjects.Remove(obj);
            }
        }
    }
}
