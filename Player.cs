using System;
using System.Threading;
using Goblin_Fight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Player{
    // Player Texture
    public static Texture2D texture;

    // Player Speed
    public static float speed = 200f; 

    // Player Position
    public static Vector2 position;

    // Player Rectangle
    public static Rectangle rect;

    // attackcooldown
    public static bool canAttack = true;

    // health
    public static int health = 100;

    // can take damage cooldown
    public static bool canTakeDamage = true;

    // Player score
    public static int score = 0;

    // attack reach
    private static int reach = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 5;

    // https://www.newgrounds.com/art/view/unrealomega7057/pixel-sprites-iron-sword attack sword texture
    public static Texture2D swordTex;

    // Constructor
    public Player(){
        rect = new Rectangle((int)position.X, (int)position.Y, texture.Bounds.Width, texture.Bounds.Height);
    }

    public static void handleInput(GameTime gameTime){
        // get keyboard state
        KeyboardState kstate = Keyboard.GetState();
        MouseState mstate = Mouse.GetState();
        if (health > 0){
            // Handle W Key
            if (kstate.IsKeyDown(Keys.W)){
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Handle S Key
            if (kstate.IsKeyDown(Keys.S)){
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            } 

            // Handle A Key
            if (kstate.IsKeyDown(Keys.A)){
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Handle D Key
            if (kstate.IsKeyDown(Keys.D)){
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            // if mouse is hovering over goblin set cursor to be sword and if within player reach
            foreach(Goblin g in Game1.goblins){
                if(g.rect.Contains(mstate.Position) && Utils.Calculations.calcDistance(position, g.position) <= reach){
                    Mouse.SetCursor(MouseCursor.FromTexture2D(swordTex,0, 0));
                    break;
                }
                else{
                    Mouse.SetCursor(MouseCursor.Crosshair);
                }
            } 




            // Handle Mouse and if click on goblin, damage goblin
            if (mstate.LeftButton == ButtonState.Pressed){
                foreach (Goblin g in Game1.goblins){
                    if(g.rect.Contains(new Point(mstate.X, mstate.Y)) && 
                    Utils.Calculations.calcDistance(g.position, position) <= reach){
                        if (canAttack){
                        canAttack = !canAttack;
                        g.health -= 25;
                        score += 25;
                        Thread t = new Thread(new ThreadStart(flipCanAttack));
                        t.Start();
                        }
                    }
                }
            }

            // Disallow leaving the world
            if (position.Y > Game1._graphics.PreferredBackBufferHeight - 64){
                position.Y = Game1._graphics.PreferredBackBufferHeight - 64;
            }
            if (position.Y < 0){
                position.Y = 0;
            }
            if (position.X < 0 ){
                position.X = 0;
            }
            if (position.X > Game1._graphics.PreferredBackBufferWidth-64){
                position.X = Game1._graphics.PreferredBackBufferWidth-64;
            }
        }
    }

    public static void handlePlayerColisions(){
        Rectangle rect = new Rectangle((int)position.X, (int)position.Y, texture.Bounds.Width, texture.Bounds.Height);
        foreach (Goblin g in Game1.goblins){
            g.rect = new Rectangle((int)g.position.X, (int)g.position.Y, 64, 64);
            if (rect.Intersects(g.rect)){
                // On colision code
                if (canTakeDamage){
                    health -= 20;
                    canTakeDamage = !canTakeDamage;
                    Thread t = new Thread(new ThreadStart(flipCanTakeDamage));
                    t.Start();
                }
            }
        }
    }

    // flip can attack boolean
    private static void flipCanAttack(){
        // if you can't attack you can now attack after 1 second
        Thread.Sleep(1000);
        canAttack = !canAttack;
    }

    // flip can take damage boolean
    private static void flipCanTakeDamage(){
        Thread.Sleep(1000);
        canTakeDamage = !canTakeDamage;
    }

}