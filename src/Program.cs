using System;
using SplashKitSDK;

namespace RathunterGame
{
    public class Program
    {
        public static void Main()
        {
            GameManager _game = new GameManager();
            Player p = new Player();

            new Window("Rat Hunter", 1000, 800);
            SplashKit.HideMouse();

            while(!SplashKit.WindowCloseRequested("Rat Hunter"))
            {
                SplashKit.ProcessEvents();
                _game.SpawnAnimals();

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                    _game.Remove(p, SplashKit.MousePosition());
                
                _game.Update();

                SplashKit.ClearScreen();

                _game.DrawBoss();
                _game.DrawBackground();
                _game.DrawObjects(); // Draws all entities in list
                _game.DrawPlayerDetails(p);

                if (p.Health <= 0)
                    _game.DrawFailScreen();

                SplashKit.RefreshScreen(60); // Game refresh rate
            }
        }
    }
}

