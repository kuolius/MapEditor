using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MapEditor
{
    class ESTileBar
    {
        ContentManager Content;
        GraphicsDeviceManager graphics;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        GameWindow Window;

        Texture2D tileset;
        Texture2D header;
        Texture2D arrow;
        Texture2D highlight;
        Texture2D highlightCol;

        List<Rectangle> tiles;
        MouseState mouseState;
        MouseState prevState;
        int scrollWidth;
        int scrollIndex;
        int selected;
        bool collision;
        SpriteFont font;


        public ESTileBar(ContentManager Content, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameWindow Window)
        {
            this.Content = Content;
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
            this.Window = Window;
        }

        public void Initialize()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            scrollIndex = 0;
            tiles = new List<Rectangle>();
        }

        public void LoadContent()
        {
            tileset = Content.Load<Texture2D>("tileset");
            arrow = Content.Load<Texture2D>("arrow");
            font = Content.Load<SpriteFont>("font");

            header = new Texture2D(graphicsDevice, Window.ClientBounds.Width, 50);
            Color[] data = new Color[Window.ClientBounds.Width * 50];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.LightCyan;
            header.SetData(data);

            highlight = new Texture2D(graphicsDevice, 20, 20);
            data = new Color[22 * 22];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Gold;
            highlight.SetData(data);

            highlightCol = new Texture2D(graphicsDevice, 22, 22);
            data = new Color[26 * 26];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.CadetBlue;
            highlightCol.SetData(data);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 5; j++)
                {
                    tiles.Add(new Rectangle(new Point(18 * i, 18 * j), new Point(18, 18)));
                }

            scrollWidth = 28 * 40- Window.ClientBounds.Width;
        }

        public void Update(out int selected,out bool collision)
        {
            mouseState = Mouse.GetState();
            
            // TODO: Add your update logic here
            if (mouseState.X > Window.ClientBounds.Width - 30 && mouseState.X < Window.ClientBounds.Width && mouseState.Y > 0 && mouseState.Y < 50 && scrollIndex < scrollWidth)
                scrollIndex += 5;

            if (mouseState.X > 0 && mouseState.X < 30 && mouseState.Y > 0 && mouseState.Y < 50 && scrollIndex > 2)
                scrollIndex -= 5;

            for (int i = 0; i < Window.ClientBounds.Width / 28 + 1; i++)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X > 10f + 28 * i - scrollIndex % 28 && mouseState.X < 10 + 28 * i - scrollIndex % 28 + 18 && mouseState.Y > 9 && mouseState.Y < 41 && this.selected!= i + scrollIndex / 28)
                {
                    this.selected = i + scrollIndex / 28;
                    this.collision = false;
                }
                if (mouseState.RightButton == ButtonState.Pressed && mouseState.X > 10f + 28 * i - scrollIndex % 28 && mouseState.X < 10f + 28 * i - scrollIndex % 28 + 18 && mouseState.Y > 9 && mouseState.Y < 41 && this.selected == i + scrollIndex / 28 && prevState!=mouseState)
                {
                    if (this.collision)
                        this.collision = false;
                    else
                        this.collision = true;
                }
            }
            selected = this.selected;
            collision = this.collision;
            prevState = mouseState;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(header, Vector2.Zero, Color.White);
            for (int i = 0; i < Window.ClientBounds.Width/28+1; i++)
            {
                if (i + scrollIndex / 28 >= 40)
                    break;
                if (i + scrollIndex / 28 == selected)
                {
                    if (collision)
                        spriteBatch.Draw(highlightCol, new Vector2(3 + 28 * i - scrollIndex % 28, 7), Color.White);
                    spriteBatch.Draw(highlight, new Vector2(4 + 28 * i - scrollIndex %28, 8), Color.White);
                }

                spriteBatch.Draw(tileset, new Vector2(5 + 28 * i - scrollIndex % 28, 9), tiles[i + scrollIndex / 28], Color.White);
            }
            if (scrollIndex < scrollWidth)
                spriteBatch.Draw(arrow, new Rectangle(new Point(Window.ClientBounds.Width - 30, 0), new Point(30, 50)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            if (scrollIndex > 0)
                spriteBatch.Draw(arrow, new Rectangle(new Point(0, 0), new Point(30, 50)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.End();
        }
    }
}
