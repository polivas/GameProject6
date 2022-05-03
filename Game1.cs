using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Common;


namespace GameProject6
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Player _player;

        /// <summary>
        /// Enemeies in the game
        /// </summary>
        private List<Enemy> _enemies;

        /// <summary>
        /// Game Background
        /// </summary>
        private Background _background;

        /// <summary>
        /// The game world
        /// </summary>
        private World world;

        /// <summary>
        /// Player Health information 
        /// </summary>

        private int maxHealth = 10;
        private int currentHealth;


        private Texture2D heartTexture;
        public Texture2D[] hearts;

        private Texture2D fullHeart;
        private Texture2D emptyHeart;

        MouseState _priorMouse;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = Constants.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Constants.GAME_HEIGHT;

            graphics.ApplyChanges();

            
        }

        protected override void Initialize()
        {
            System.Random rand = new System.Random();
            //World Creation
            world = new World();
            world.Gravity = Vector2.Zero;

            var top = 0;
            var bottom = Constants.GAME_MAX_HEIGHT;
            var left = 0;
            var right = Constants.GAME_MAX_WIDTH;

            var edges = new Body[]{
                world.CreateEdge(new Vector2(left, top), new Vector2(right, top)),
                world.CreateEdge(new Vector2(left, top), new Vector2(left, bottom)),
                world.CreateEdge(new Vector2(left, bottom), new Vector2(right, bottom)),
                world.CreateEdge(new Vector2(right, top), new Vector2(right, bottom))
             };

            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }

            //Create Enemies
            System.Random random = new System.Random();
            _enemies = new List<Enemy>();

            for (int i = 0; i < 5; i++) // Creates 5 enemies
            {
                var radius = random.Next(1, 2);
                var position = new Vector2(
                    random.Next(radius, Constants.GAME_MAX_WIDTH- radius),
                    random.Next(radius, Constants.GAME_MAX_HEIGHT - radius)
                    );

                //Adding rigid body
                var body = world.CreateCircle(radius, 1, position, BodyType.Dynamic);

                _enemies.Add(new Enemy(Content.Load<Texture2D>("ghost"), position, 150 ,radius, body));
            }

            //Creates  player
            Vector2 pos = (new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));           
            _player = new Player(pos);


            _background = new Background();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            Rectangle heartFull = new Rectangle(0,0, 48, 48);
            Rectangle heartEmpty = new Rectangle(0, 48, 48, 48);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            _player.LoadContent(Content);
            _background.LoadContent(Content);

            heartTexture = Content.Load<Texture2D>("health");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState currentMouse = Mouse.GetState();
            Vector2 mousePosition = new Vector2(currentMouse.X, currentMouse.Y);

            _player.Update(gameTime);


            //foreach (var ghost in _enemies) ghost.Update(gameTime, _player);

            _background.Update(gameTime, _player, _enemies);

            //Update hearts
 //           foreach (var heart in hearts)

            ///Attacking mechanics, needs work to be succesfful
            //Switsh Effect, may implement as its own class
            //
            if (currentMouse.LeftButton == ButtonState.Pressed && _priorMouse.LeftButton == ButtonState.Released)
            {
                Vector2 currClick = new Vector2(currentMouse.X, currentMouse.Y);
                if(currClick.X - _player.Position.X > 0)
                {

                }else if (currClick.Y - _player.Position.Y > 0)
                {

                }
                else
                {

                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _background.Draw(gameTime, spriteBatch, _player, _enemies);

            spriteBatch.Begin();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
