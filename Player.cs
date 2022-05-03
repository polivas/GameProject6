using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;


namespace GameProject6
{
    /// <summary>
    /// Texture states for the given spritesheet, used for animation.
    /// </summary>
    public enum TextureMode
    {
        Up = 0, 
        Right = 1,
        Down = 2,
        Left = 3,
        HitUp = 4,
        HitRight = 5,
        HitDown = 6,
        HitLeft = 7, 
        Dead = 14,
    
    }


    public class Player
    {       
  
        /// <summary>
        /// Texture of player
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// If button is currently being pressed, for idle animation
        /// </summary>
        private bool pressing = false;

        /// <summary>
        /// Timer used as clicker for animation of player sprite. Deafult = false
        /// </summary>
        private double animationTimer;

        /// <summary>
        /// Current frame in the animation, Deafult = 0
        /// </summary>
        private short animationFrame = 0;

        /// <summary>
        /// the direction of the hunter
        /// </summary>
        public TextureMode TextureMode;

        /// <summary>
        /// poition of the hunter
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Checks if the hunter is flipped, to flip the drawn sprite
        /// </summary>
        public bool Flipped;


        public Player(Vector2 position)
        {
            this.Position = position;
            
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("cloakandleather");

        }


        /// <summary>
        /// Updates the hunter sprite
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            var keyboardState = Keyboard.GetState();
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            pressing = false;

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                Position += new Vector2(0, -1);
                TextureMode = TextureMode.Up;
                pressing = true;
            }

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, 1);
                TextureMode = TextureMode.Down;
                pressing = true;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0);
                TextureMode = TextureMode.Left; 
                Flipped = true;
                pressing = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Position += new Vector2(1, 0);
                TextureMode = TextureMode.Right;
                Flipped = false;
                pressing = true;
            }

            if ((keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Space)) && (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)))
            {
                Position.X += 1;
            }
            if ((keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Space)) && (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)))
            {
                Position.X -= 1;
            }

            if ((keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Space)) && (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)))
            {
                Position.Y -= 1;
            }
            if ((keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Space)) && (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)))
            {
                Position.Y += 1;
            }

        }


        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
     

            //Update animation Timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > 0.3 )
            {
                animationFrame++;
                if (animationFrame > 1) animationFrame = 0;
                animationTimer -= 0.3;
            }
           if (animationTimer > 0.3 ) animationTimer -= 0.3;



            var source = new Rectangle(animationFrame * 16, (int)TextureMode * 16, 16, 16);
          
            if (!pressing)
            {
                source = new Rectangle(animationFrame * 16, 32, 16, 16);
                spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(16, 16), 1.5f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(16, 16), 1.5f, SpriteEffects.None, 0);
            }



        }

    }
}
