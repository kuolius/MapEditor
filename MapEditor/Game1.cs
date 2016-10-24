﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MapEditor
{
    public enum State { editScreen, menuScreen ,exit,newMap,loadMap}

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EditScreen editScreen;
        MenuScreen menuScreen;
        newMap NewMap;
        public State state;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
       

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Window.Position = new Point(100, 100);
            IsMouseVisible = true;
            graphics.ApplyChanges();
           
            editScreen = new EditScreen(Content, graphics, GraphicsDevice, spriteBatch, Window);
            menuScreen = new MenuScreen(spriteBatch, GraphicsDevice, Window, Content,NewMap);

            state = State.menuScreen;
            editScreen.Initialize();
            menuScreen.Initialize();
            
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            editScreen.LoadContent();
            menuScreen.LoadContent();


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           
            
            switch (state)
            {
                case State.editScreen:
                    editScreen.Update(out state,IsActive);
                    break;
                case State.menuScreen:
                    menuScreen.Update(out state,out NewMap,editScreen);
                    break;
                case State.newMap:
                    NewMap.update(out state,editScreen);
                    break;
                case State.exit:
                    Exit();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            switch (state)
            {
                case State.editScreen:
                    editScreen.Draw();
                    break;
                case State.menuScreen:
                    menuScreen.Draw();
                    break;
            }

           base.Draw(gameTime);
        }
    }
}
