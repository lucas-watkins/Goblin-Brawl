using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Goblin_Fight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utils;
public class Goblin{
    // Goblin Texture
    public static Texture2D texture;

    // Goblin Speed
    public float speed = 100f; 

    // Goblin Position
    public Vector2 position;

    // Goblin Rectangle
    public Rectangle rect;

    //Goblin Health
    public int health = 75;

    // alive indicator
    public bool alive = true;

    // constuctor
    public Goblin(){
        Random r = new Random();
        Rectangle candidateSpawnRegion = Calculations.calcAllowedSpawnRegion()[r.Next(0,4)];
        Vector2 spawnPoint = candidateSpawnRegion.Location.ToVector2(); 
        int xVal = r.Next(0, candidateSpawnRegion.Width); 
        int yVal = r.Next(0, candidateSpawnRegion.Height); 

        position = new Vector2(spawnPoint.X + xVal, spawnPoint.Y + yVal);
    }


    // Move goblin based on player position
    public void move(GameTime gameTime){
        if (health > 0){
            if (Player.position.X < position.X){
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Player.position.X > position.X){
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Player.position.Y > position.Y){
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Player.position.Y < position.Y){
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            
            // make sure that goblin does not intersect with other goblins
            foreach (Goblin g in Game1.goblins){
                if (rect.Intersects(g.rect)){
                    if (position.X < g.position.X || position.X > g.position.X || position.X == g.position.X){
                        position.X += 200;
                        g.position.X -= 200;
                    }
                    if (position.Y < g.position.Y || position.Y > g.position.Y || position.Y == g.position.Y){
                        position.Y += 200;
                        g.position.Y -= 200;
                    }
                }
            }
        }
        else{
            if (alive){
                alive = !alive;
                // move hitbox out of play area
                rect = new Rectangle(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height + 100, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width + 100, texture.Bounds.Width, texture.Bounds.Height);
                position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height + 100, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width + 100);

                // give player 5 health for killing Goblin if health less than 100% or fill to 100%
                if (Player.health <= 95){
                    Player.health += 5;
                }
                else{
                    Player.health += (100 - Player.health);
                }
            }
        }
    }
}