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
    class MapLoader
    {

        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        ContentManager Content;
        GameWindow Window;

        public int[][][][][] map;
        public int[][] shaders;
        int coordX;
        int coordY;
        MouseState mouseState;
        KeyboardState keyboard,prevKeyboard;
        int scrollSpeed;
        int scrollWidth;
        string name;
        bool responded;
        int drawSize;

        Texture2D tileset,shader;
        SpriteFont font;
        SaveFile saveFile;
        State state;

        public MapLoader(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice,int[][][][][] map,ContentManager Content,GameWindow Window)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.map = map;
            this.Content = Content;
            this.Window = Window;
            
        }


        public void LoadShaders()
        {
            shaders = new int[map.Length  * 36][];
            for (int i = 0; i < shaders.Length; i++)
                shaders[i] = new int[map[0].Length * 36];
            updateShaders();
        }

        public void Initialize()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            saveFile = new SaveFile();

            drawSize = 1;
            coordX = 0;
            coordY = 0;
            scrollSpeed = 4;
            scrollWidth = 50;
            state = State.editScreen;
            responded = true;
        }

        public void LoadContent()
        {
            tileset = Content.Load<Texture2D>("tileset");
            font = Content.Load<SpriteFont>("font");
            shader = new Texture2D(graphicsDevice,5,5);

            Color[] data = new Color[25];
            for (int i = 0; i < 25; i++)
                data[i] = Color.Black;

            shader.SetData(data);
        }

        public void updateShaders()
        {
            for (int i = 0; i < shaders.Length; i++)
                for (int j = 0; j < shaders[0].Length; j++)
                {
                    if (j == 0)
                        shaders[i][j] = 0;
                    
                    else
                    {

                        
                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 1)
                            shaders[i][j] = shaders[i][j - 1] +5;


                       else  if ((i + 1) * 5 / 180 < map.Length)
                            if (map[((i + 1) * 5) / 180][(j * 5) / 180][(((i + 1) * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 1)
                                shaders[i][j] = shaders[i][j - 1] + 5;


                        int o = 0;
                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                        {
                            if (map[(i * 5) / 180][((j - 1) * 5) / 180][((i * 5) % 180) / 18][(((j - 1) * 5) % 180) / 18][2] == 0)
                                o++;
                            
                        }
                         if ((i + 1) * 5 / 180 < map.Length)
                        {
                            if (map[((i + 1) * 5) / 180][(j * 5) / 180][(((i + 1) * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                                if (map[((i + 1) * 5) / 180][((j - 1) * 5) / 180][(((i + 1) * 5) % 180) / 18][(((j - 1) * 5) % 180) / 18][2] == 0)
                                    o++;
                               
                        }
                        if (o == 2 )
                                shaders[i][j] = shaders[i][j - 1];
                            







                    }
                }

            
            for (int i = 0; i < shaders.Length; i++)
                for (int j = shaders[0].Length-1; j >=0 ; j--)
                {



                    if (j != shaders[0].Length - 1)
                    {


                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 1)
                            if (shaders[i][j + 1] + 5< shaders[i][j])
                                shaders[i][j] = shaders[i][j + 1] + 5;


                        else if ((i + 1) * 5 / 180 < map.Length)
                            if (map[((i + 1) * 5) / 180][(j * 5) / 180][(((i + 1) * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 1)
                                if(shaders[i ][j+1] + 5 < shaders[i][j])
                                    shaders[i][j] = shaders[i][j + 1] + 5;


                        int o = 0;
                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                        {
                            if (map[(i * 5) / 180][((j + 1) * 5) / 180][((i * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 0)
                                o++;

                        }
                        if ((i + 1) * 5 / 180 < map.Length)
                        {
                            if (map[((i + 1) * 5) / 180][(j * 5) / 180][(((i + 1) * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                                if (map[((i + 1) * 5) / 180][((j + 1) * 5) / 180][(((i + 1) * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 0)
                                    o++;

                        }
                        if (o == 2)
                            shaders[i][j] = shaders[i][j + 1];








                    }
                }
            
            for (int i = 0; i < shaders.Length; i++)
                for (int j = 0; j < shaders[0].Length; j++)
                {
                   // if (i == 0)
                     //   shaders[i][j] = 0;

                  //  else
                  if(i!=0)
                 {


                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 1 )
                        {
                            if (shaders[i - 1][j] + 5 < shaders[i][j])
                                shaders[i][j] = shaders[i - 1][j] + 5;

                        }
                        else if ((j + 1) * 5 / 180 < map[0].Length)
                        {
                            if (map[(i * 5) / 180][((j + 1) * 5) / 180][((i * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 1)
                                if (shaders[i - 1][j] + 5 < shaders[i][j])
                                    shaders[i][j] = shaders[i - 1][j] + 5;
                        }

                        int o = 0;

                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                        {
                            if (map[((i - 1) * 5) / 180][(j * 5) / 180][(((i - 1) * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                                o++;
                        }
                         if ((j + 1) * 5 / 180 < map[0].Length)
                        {
                            if (map[(i * 5) / 180][((j + 1) * 5) / 180][((i * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 0)
                                if (map[((i - 1) * 5) / 180][((j + 1) * 5) / 180][(((i - 1) * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 0)
                                    o++;
                        }

                        if (o ==2 )
                                shaders[i][j] = shaders[i - 1][j];






                    }
               }
            
            for (int i = shaders.Length-1; i >=0; i--)
                for (int j = 0; j < shaders[0].Length; j++)
                {



                    if (i != shaders.Length - 1)
                    {


                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 1)
                        {
                            if (shaders[i + 1][j] + 5 < shaders[i][j])
                                shaders[i][j] = shaders[i + 1][j] + 5;

                        }
                        else if ((j + 1) * 5 / 180 < map[0].Length)
                        {
                            if (map[(i * 5) / 180][((j + 1) * 5) / 180][((i * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 1)
                                if (shaders[i + 1][j] +5 < shaders[i][j])
                                    shaders[i][j] = shaders[i + 1][j] + 5;
                        }

                        int o = 0;

                        if (map[(i * 5) / 180][(j * 5) / 180][((i * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                        {
                            if (map[((i + 1) * 5) / 180][(j * 5) / 180][(((i + 1) * 5) % 180) / 18][((j * 5) % 180) / 18][2] == 0)
                                o++;
                        }
                        if ((j + 1) * 5 / 180 < map[0].Length)
                        {
                            if (map[(i * 5) / 180][((j + 1) * 5) / 180][((i * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 0)
                                if (map[((i + 1) * 5) / 180][((j + 1) * 5) / 180][(((i + 1) * 5) % 180) / 18][(((j + 1) * 5) % 180) / 18][2] == 0)
                                    o++;
                        }

                        if (o == 2)
                            shaders[i][j] = shaders[i + 1][j];





                    }
                }
                
        }

        public void Update(int selected,bool collision,out State state,bool isActive)
        {
            mouseState = Mouse.GetState();
            keyboard = Keyboard.GetState();
            if (isActive)
            {
                if ((mouseState.X < Window.ClientBounds.Width && mouseState.X > Window.ClientBounds.Width - scrollWidth && mouseState.Y > 50 && mouseState.Y < Window.ClientBounds.Height || keyboard.IsKeyDown(Keys.Right)) && coordX <= map.Length * 180 - Window.ClientBounds.Width - scrollSpeed)
                    coordX += scrollSpeed;
                if ((mouseState.X > 0 && mouseState.X < scrollWidth && mouseState.Y > 50 && mouseState.Y < Window.ClientBounds.Height || keyboard.IsKeyDown(Keys.Left)) && coordX >= scrollSpeed)
                    coordX -= scrollSpeed;
                if ((mouseState.Y < Window.ClientBounds.Height && mouseState.Y > Window.ClientBounds.Height - scrollWidth && mouseState.X > 0 && mouseState.X < Window.ClientBounds.Width || keyboard.IsKeyDown(Keys.Down)) && coordY <= map[0].Length * 180 - Window.ClientBounds.Height - scrollSpeed + 50)
                    coordY += scrollSpeed;
                if ((mouseState.Y > 50 && mouseState.Y < 50 + scrollWidth && mouseState.X > 0 && mouseState.X < Window.ClientBounds.Width || keyboard.IsKeyDown(Keys.Up)) && coordY >= scrollSpeed)
                    coordY -= scrollSpeed;

                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.Y > 50 && mouseState.Y < Window.ClientBounds.Height && mouseState.X >= 0 && mouseState.X < Window.ClientBounds.Width)
                {
                    for (int i = 0; i < drawSize; i++)
                    {
                        if ((mouseState.X + coordX + 18 * i) / 180 >= map.Length)
                            break;
                        for (int j = 0; j < drawSize; j++)
                        {
                            if ((mouseState.Y - 50 + coordY + 18 * j) / 180 >= map[0].Length)
                                break;
                            map[(mouseState.X + coordX + 18 * i) / 180][(mouseState.Y - 50 + coordY + 18 * j) / 180][(((mouseState.X + coordX + 18 * i) % 180) / 18) % 10][(((mouseState.Y - 50 + coordY + 18 * j) % 180) / 18) % 10][0] = selected / 5;
                            map[(mouseState.X + coordX + 18 * i) / 180][(mouseState.Y - 50 + coordY + 18 * j) / 180][(((mouseState.X + coordX + 18 * i) % 180) / 18) % 10][(((mouseState.Y - 50 + coordY + 18 * j) % 180) / 18) % 10][1] = selected % 5;
                            if (collision)
                                map[(mouseState.X + coordX + 18 * i) / 180][(mouseState.Y - 50 + coordY + 18 * j) / 180][(((mouseState.X + coordX + 18 * i) % 180) / 18) % 10][(((mouseState.Y - 50 + coordY + 18 * j) % 180) / 18) % 10][2] = 1;
                            else
                                map[(mouseState.X + coordX + 18 * i) / 180][(mouseState.Y - 50 + coordY + 18 * j) / 180][(((mouseState.X + coordX + 18 * i) % 180) / 18) % 10][(((mouseState.Y - 50 + coordY + 18 * j) % 180) / 18) % 10][2] = 0;

                        }
                    }
                    
                }

            }

            if (keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.S))
            {
                saveFile = new SaveFile();
                saveFile.Show();
                responded = false;
            }
            if(!responded)
                saveFile.update(out name,out responded);

            if(name!="" && name!=null)
            {
                Stream stream = File.Open(name + ".bin", FileMode.Create);
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, new object[2] { map, shaders });
                stream.Close();
                saveFile.Dispose();
                name = "";

            }

            if(keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.E))
            {
                System.Windows.Forms.DialogResult messageBox =System.Windows.Forms.MessageBox.Show("Are you sure, you want to exit?", "Exit", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (messageBox==System.Windows.Forms.DialogResult.Yes)
                {
                    this.state = State.exit;
                }

            }

            if(keyboard.IsKeyUp(Keys.S) && prevKeyboard.IsKeyDown(Keys.S))
                updateShaders();

            if (keyboard.IsKeyUp(Keys.Add)&& prevKeyboard.IsKeyDown(Keys.Add))
                drawSize += 2;
            if (keyboard.IsKeyUp(Keys.Subtract) && prevKeyboard.IsKeyDown(Keys.Subtract))
                drawSize -= 2;





            prevKeyboard = keyboard;
            state = this.state;
        }

        public void Draw()
        {
            int squaresX = (Window.ClientBounds.Width + coordX % 180 - 180) / 180;
            int squaresY = (Window.ClientBounds.Height-50 + coordY % 180- 180) / 180;

            if ((Window.ClientBounds.Width + coordX % 180 - 180) % 180 != 0)
                squaresX++;
            if ((Window.ClientBounds.Height-50 + coordY % 180 - 180) % 180 != 0)
                squaresY++;

            if (180 - coordX % 180 > 0) squaresX++;
            if(180- coordY % 180 > 0) squaresY++;
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);

            for (int i = 0; i < squaresX; i++)
                for (int j = 0; j < squaresY; j++)
                    for (int k = 0; k < 10; k++)
                        for (int l = 0; l < 10; l++)
                        {
                            //if(map[i + coordX / 180][(j + coordY / 180)][k][l][2]==0)
                            
                            spriteBatch.Draw(tileset, new Vector2(180 * i + 18 * k - coordX % 180,180 * j + 18 * l + 50 - coordY % 180), new Rectangle(new Point(map[i + coordX / 180][(j + coordY / 180)][k][l][0] * 18, map[i + coordX / 180][(j + coordY / 180)][k][l][1] * 18), new Point(18, 18)), Color.White);
                            //spriteBatch.Draw(shader, new Vector2(180 * i + 18 * k - coordX % 180, 180 * j + 18 * l + 50 - coordY % 180),Color.Black*((100- map[i + coordX / 180][(j + coordY / 180)][k][l][3])/(float)100));
                            /* else
                                 spriteBatch.Draw(tileset, new Vector2(180 * i + 18 * k - coordX % 180, 180* j + 18 * l + 50 - coordY % 180), new Rectangle(new Point(map[i + coordX / 180][(j + coordY / 180)][k][l][0] * 18, map[i + coordX / 180][(j + coordY / 180)][k][l][1] * 18), new Point(18, 18)), new Color(255 * map[i + coordX / 180][(j + coordY / 180)][k][l][3], 255 * map[i + coordX / 180][(j + coordY / 180)][k][l][3], 255 * map[i + coordX / 180][(j + coordY / 180)][k][l][3]));
                                 */
                        }
            spriteBatch.DrawString(font, Convert.ToString(drawSize), new Vector2(100, 100), Color.Black);
            

            


            spriteBatch.End();

           

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            for (int i = 0; i < Window.ClientBounds.Width / 5; i++)
                for (int j = 0; j < (Window.ClientBounds.Height - 50) / 5; j++)
                {

                    spriteBatch.Draw(shader, new Vector2(i * 5, j * 5 + 50), new Color(Color.White , shaders[i + coordX / 5][j + coordY / 5] ));

                }
            spriteBatch.End();
        }
    }
}
