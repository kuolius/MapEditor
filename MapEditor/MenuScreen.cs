using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class MenuScreen
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        GameWindow Window;
        SpriteFont menuFont;
        ContentManager Content;
        MouseState mouseState;
        int magnifyIndex;
        int sepSpace;
        List<String> menuItems;
        newMap NewMap;
        

        public MenuScreen(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameWindow Window, ContentManager Content,newMap NewMap)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.Window = Window;
            this.Content = Content;
            this.NewMap = NewMap;
        }

        public void Initialize()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            magnifyIndex = -1;
            sepSpace = 50;
            menuItems = new List<string>(new string[] { "NewMap", "LoadMap", "Exit" });
        }

        public void LoadContent()
        {
            menuFont = Content.Load<SpriteFont>("font");
        }

        public void Update(out State state, out newMap NewMap,EditScreen editScreen)
        {
            mouseState = Mouse.GetState();
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            state = State.menuScreen;
            NewMap = this.NewMap;
           
                for(int i=0;i<menuItems.Count;i++)
                {
                    if(mouseState.X> Window.ClientBounds.Width / 2 - menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.X< Window.ClientBounds.Width / 2 + menuFont.MeasureString(menuItems[i]).X/2 && mouseState.Y>150+i*sepSpace && mouseState.Y<150+i*sepSpace+ menuFont.MeasureString(menuItems[i]).Y)
                    {
                        magnifyIndex = i;
                        break;
                    }
                    else
                    {
                        magnifyIndex = -1;
                    }
                }

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (mouseState.X > Window.ClientBounds.Width / 2 - menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.X < Window.ClientBounds.Width / 2 + menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.Y > 150 + i * sepSpace && mouseState.Y < 150 + i * sepSpace + menuFont.MeasureString(menuItems[i]).Y && menuItems[i] == "Exit" && mouseState.LeftButton == ButtonState.Pressed)
                {
                    state = State.exit;
                }
                else if (mouseState.X > Window.ClientBounds.Width / 2 - menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.X < Window.ClientBounds.Width / 2 + menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.Y > 150 + i * sepSpace && mouseState.Y < 150 + i * sepSpace + menuFont.MeasureString(menuItems[i]).Y && menuItems[i] == "NewMap" && mouseState.LeftButton == ButtonState.Pressed)
                {
                    state = State.newMap;
                    this.NewMap = new newMap();
                    NewMap = this.NewMap;
                    NewMap.Show();
                }

                else if (mouseState.X > Window.ClientBounds.Width / 2 - menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.X < Window.ClientBounds.Width / 2 + menuFont.MeasureString(menuItems[i]).X / 2 && mouseState.Y > 150 + i * sepSpace && mouseState.Y < 150 + i * sepSpace + menuFont.MeasureString(menuItems[i]).Y && menuItems[i] == "LoadMap" && mouseState.LeftButton == ButtonState.Pressed)
                {
                     
                    openFileDialog.ShowDialog();
                }

            }

            if(openFileDialog.FileName!="")
            {
                state = State.editScreen;
                Stream stream = File.Open(openFileDialog.FileName, FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();
                var obj = (object[])bin.Deserialize(stream);
                var map = (int[][][][][])(obj[0]);
                editScreen.map = map;
                editScreen.InitializeMap();
                stream.Close();
            }
        }

        public void Draw()
        {
            spriteBatch.Begin();

            for(int i=0;i<menuItems.Count;i++)
            {
                if (magnifyIndex == i)
                    spriteBatch.DrawString(menuFont, menuItems[i], new Vector2(Window.ClientBounds.Width / 2 - (float)(menuFont.MeasureString(menuItems[i]).X / 2 * 1.2), 150+i*sepSpace), Color.Black, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 1f);
                else
                    spriteBatch.DrawString(menuFont, menuItems[i], new Vector2(Window.ClientBounds.Width / 2 - menuFont.MeasureString(menuItems[i]).X / 2, 150+sepSpace*i), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }

            spriteBatch.End();
        }
    }
}
