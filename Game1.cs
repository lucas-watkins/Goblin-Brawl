using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Goblin_Fight;


public class Game1 : Game
{
    public static List<Goblin> goblins = new List<Goblin>();
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Textures
   

    // Positions
    

    // Speeds
   
    // Fonts
    public static SpriteFont deathFont;
    public static SpriteFont statsFont;

    //death text
    string deathText;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        
        _graphics.ToggleFullScreen();
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        // initalize positions 
        Player.position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 32, _graphics.PreferredBackBufferHeight / 2 - 32);

        Window.Title = "Goblin Attack";
        base.Initialize();
    }

    protected override void LoadContent()
    {   
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        
        // Load Textures
        Goblin.texture = Content.Load<Texture2D>("ball");
        
        Player.texture = Content.Load<Texture2D>("square");

        deathFont = Content.Load<SpriteFont>("deathFont");

        statsFont = Content.Load<SpriteFont>("statsFont");

        Player.swordTex = Content.Load<Texture2D>("sword");
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if (!Utils.Mechanics.splashScreenActive){
            // Handle Player Input
            Player.handleInput(gameTime);

            // Handle Player Colisions
            Player.handlePlayerColisions();

            // Move Goblins
            foreach (Goblin g in goblins){
                if (g.alive){
                g.move(gameTime);
                }
            }

            // wave management

            // if no goblins are on screen spawn next wave
            int goblinsAlive = 0; 
            foreach (Goblin goblin in goblins){
                if (goblin.alive){
                    goblinsAlive++;
                }
            }
            if(goblinsAlive == 0){
                for(int i = 0; i < WaveCounter.calcGoblins(WaveCounter.wave); i++){
                    goblins.Add(new Goblin());
                }
                WaveCounter.wave++;
            }
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        // Draws Textures from first mentioned at bottom to last at top
        _spriteBatch.Begin();

        // draw splash screen if necessary
        if (Utils.Mechanics.splashScreenActive){
            Vector2 stringLen = deathFont.MeasureString("Goblin Brawl");
            _spriteBatch.DrawString(deathFont, "Goblin Brawl", new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - stringLen.X/2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - stringLen.Y/2 - 250), Color.Black);   
            
            //TODO: Draw Play Button and make it work
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                Utils.Mechanics.splashScreenActive = false;
            }
        }
        else{

            // Draw Goblin if alive
            foreach (Goblin g in goblins){
                if (g.alive){
                    _spriteBatch.Draw(Goblin.texture, g.position, Color.White);
                }
            }
            // Draw Player if alive
            if (Player.health >= 0){
                _spriteBatch.Draw(Player.texture, Player.position, Color.White);

            }
            else{
                // draw death text
                if (!Utils.Mechanics.deathTextAssigned){

                    deathText = Utils.Mechanics.getDeathText();
                    Utils.Mechanics.deathTextAssigned = true;
                }

                Vector2 stringLen = deathFont.MeasureString(deathText);   
                _spriteBatch.DrawString(deathFont, deathText, new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - stringLen.X/2, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - stringLen.Y/2), Color.Black);   
                
            }

            // draw health and waves
            _spriteBatch.DrawString(statsFont, "Score: " + Player.score.ToString(), new Vector2(50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 150), Color.Black);
            if (Player.health > 0){
                _spriteBatch.DrawString(statsFont, "Wave: " + (WaveCounter.wave-1).ToString(), new Vector2(50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 50), Color.Black);
                _spriteBatch.DrawString(statsFont, "Health: " + Player.health.ToString() + "%", new Vector2(50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100), Color.Black);
            }
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
