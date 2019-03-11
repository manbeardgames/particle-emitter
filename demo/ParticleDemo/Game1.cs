using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//  Add reference to ParticleEmitter
using ParticleEmitter;

namespace ParticleDemo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //  Reference to our particle emitter
        private Emitter _emitter;

        //  Reference to our emiiter genrator options
        private ParticleGeneratorOptions _generatorOptions;

        //  For the demo, the width and height of screen is
        private int _width = 1280;
        private int _height = 720;

        //  Used for keyboard input in update method
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //  Set screen width and height
            this.graphics.PreferredBackBufferWidth = this._width;
            this.graphics.PreferredBackBufferHeight = this._height;
            this.graphics.ApplyChanges();

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

            //  Instantiate the emitter. For demo, placing it in middle of screen
            this._emitter = new Emitter(new Vector2(this._width, this._height) * 0.5f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //  Create the generator options for our particle emitter
            this._generatorOptions = new ParticleGeneratorOptions()
            {
                //  EmiisionType determines how the xy-velocity of the particles are calculated.  
                //  Emissiontype.Burst will randomly make the x and/or y velocity positive or negative
                //  EmissionType.UpOnly will only calcuate the y veolocity so that the particles will move up from the emitter
                //  EmissionType.DownOnly will only calculate the y velocity so that the particles move down from the emitter
                //  EmissionType.LeftOnly will only calculate the x velocity so that the particles move left from the emitter
                //  EmissionType.RightOnly will only calculate the x velocity so that the particles move right from the emitter
                EmissionType = EmissionType.Burst,

                //  Texture is the Texture2D used to render the particles that are generated with these
                //  options
                Texture = Content.Load<Texture2D>("demoParticle"),

                //  Color is the color mask used to render the particles
                Color = Color.White,

                //  Time to live is how long the particles should live. It randomly chooses between the
                //  min and the max value.  If all particles should live the same amount of time, set
                //  both min and max to the same value
                TimeToLive = new MinMaxFloat() { Minimum = 1.0f, Maximum = 1.5f },

                //  Count is the total number of particles to generate
                Count = 30,

                //  Velocity is the xy-velocity that is applied to the particles. It randomly chooses between
                //  the min and max value. If all particles should move at the same rate, then set min and max
                //  to the same value
                Velocity = new MinMaxFloat() { Minimum = 1.0f, Maximum = 4.0f },

                //  RotationVelocity is the velocity that is applied to the rotation of the of the particles.
                //  It randomly chooses between the min and max value.  If all particles shoudl rotate with
                //  the same velocity, set min and max to the same value
                RotationVelocity = new MinMaxFloat() { Minimum = 1.0f, Maximum = 2.0f },

                //  Scale is the xy scale that is applied to the particles when rendered. It randomly choose
                //  between the min and max values.  If all particles should scale the same, set min and max
                //  to the same value
                Scale = new MinMaxFloat() { Minimum = 0.1f, Maximum = 1.0f },

                //  Fade sets if the particles should fade out over the duration of the time they are alive.
                //  Set true to fade, or false to not fade
                Fade = false
            };


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //  Update the emitter
            _emitter.Update(gameTime);


            //  Keyboard state managing
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            //  When the user press Space on keyboard, generate particles based on our options
            if(_currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                _emitter.GenerateParticles(_generatorOptions);
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

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _emitter.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
