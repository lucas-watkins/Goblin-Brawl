using Microsoft.Xna.Framework;
using System;

namespace Utils{
    static class Mechanics{
        public static bool deathTextAssigned = false;

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
}