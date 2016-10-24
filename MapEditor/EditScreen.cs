using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
   public class EditScreen
    {
        ContentManager Content;
        GraphicsDeviceManager graphics;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        GameWindow Window;

        ESTileBar ESTtileBar;
        MapLoader mapLoader;
       

        
        public int[][][][][] map ;
        int selected;
        bool collision;
      



        public EditScreen(ContentManager Content, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, GameWindow Window)
        {
            this.Content = Content;
            this.graphics = graphics;
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
            this.Window = Window;
        }

        public void Initialize()
        {
            ESTtileBar = new ESTileBar(Content, graphics, graphicsDevice, spriteBatch, Window);
            mapLoader = new MapLoader(spriteBatch, graphicsDevice, map, Content,Window);

            ESTtileBar.Initialize();
            mapLoader.Initialize();
        }

        public void LoadContent()
        {
            ESTtileBar.LoadContent();
            mapLoader.LoadContent();
        }

        public void Update(out State state,bool isActive)
        {
            ESTtileBar.Update(out selected,out collision);
            mapLoader.Update(selected,collision,out state,isActive);


        }

        public void Draw()
        {
            mapLoader.Draw();
            ESTtileBar.Draw();
            

            
        }

        public void InitializeMap()
        {
            mapLoader.map = map;
            mapLoader.LoadShaders();
        }
    }
}
