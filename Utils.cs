using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Utils{
    static class Mechanics{

        public static int goblinSpawnBuffer = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 3; 
        public static bool deathTextAssigned = false;

        public static int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public static bool isPaused = false;

        public static bool splashScreenActive = true;
        public static string getDeathText(){
            int random = new Random().Next(1,5);
            switch (random){
                case 1:
                return "You're Dead lol";

                case 2:
                return "Ripperoni";

                case 3:
                return "Wasted.";

                case 4:
                return "R.I.P.";

                default:
                return "lol";
            }
                
        }
        
    }

    class Calculations {
        // calculate distance to make reach distance for player
        public static double calcDistance(Vector2 p1, Vector2 p2){
            return Math.Sqrt(Math.Pow(p2.X-p1.X,2)+Math.Pow(p2.Y-p1.Y,2));
        }

        public static Rectangle[] calcAllowedSpawnRegion(){
            return (
                [new Rectangle(0,0, (int) Player.position.X - Mechanics.goblinSpawnBuffer, Mechanics.screenHeight),
                 
                 new Rectangle(0, (int) Player.position.X - Player.texture.Width - Mechanics.goblinSpawnBuffer,
                 Mechanics.screenWidth, (int) Player.position.X - Player.texture.Width - Mechanics.goblinSpawnBuffer),
                
                 new Rectangle(0, (int) Player.position.X + Mechanics.goblinSpawnBuffer, Mechanics.screenWidth, 
                 (int) Player.position.X + Mechanics.goblinSpawnBuffer),

                 new Rectangle((int) Player.position.X + Player.texture.Width + Mechanics.goblinSpawnBuffer, 0, 
                 Mechanics.screenWidth - (int) Player.position.X, Mechanics.screenHeight)
                ]
            );
        }
    }

}