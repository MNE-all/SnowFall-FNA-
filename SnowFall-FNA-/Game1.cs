using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SnowFall_FNA_
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, snowflake;
        private bool n = true;
        int pos = 0;

        List<SnowClass> snows = new List<SnowClass>();

        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "..\\..\\Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Window.Title = "SnowFall";

        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            InitSnowflakes();
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = TextureLoader.Load("background", Content);
            snowflake = TextureLoader.Load("snow", Content);


        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            StopStart();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            //Update the things FNA handles for us underneath the hood:
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear(Color.Purple);

            UpdatePositions();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            DrawSnowflakes();
            spriteBatch.End();

            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }


        private void StopStart()
        {
            if (Input.MouseLeftClicked())
            {
                if (n)
                {
                    n = false;
                }
                else
                {
                    n = true;
                }
            }
        }

        private void DrawSnowflakes()
        {
            foreach(SnowClass snow in snows)
            {
                spriteBatch.Draw(snowflake, new Vector2(snow.PosX, snow.PosY), null, Color.White, (float)snow.Rotation, Vector2.Zero, snow.Severity * 0.1f, SpriteEffects.None, 1);
                //spriteBatch.Draw(snowflake, new Vector2(snow.PosX, snow.PosY), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
            }
            
        }


        private void InitSnowflakes()
        {
            Random random = new Random();
            for(int i = 0; i < 300; i++)
            {
                snows.Add(new SnowClass
                (
                    random.Next(graphics.PreferredBackBufferWidth),
                    random.Next(graphics.PreferredBackBufferHeight),
                    random.Next(1, 20)
                ));
            }
        }


        private void UpdatePositions()
        {
            Random random = new Random();
            for(int i = 0; i < snows.Count(); i++)
            {
                snows[i].PosY += snows[i].Severity + 2;
                snows[i].Rotation += 0.1;

                if (snows[i].PosY > graphics.PreferredBackBufferHeight)
                {
                    snows[i].PosY = 0;
                    snows[i].PosX = random.Next(graphics.PreferredBackBufferWidth);
                    snows[i].Severity = random.Next(1, 20);
                    snows[i].Rotation = random.Next(1, 20);
                }
            }
        }

    }
}
