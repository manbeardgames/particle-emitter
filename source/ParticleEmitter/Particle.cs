using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEmitter
{
    public class Particle
    {
        /// <summary>
        ///     The texure used when rendering the particle
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        ///     The xy-coordinate location of the particle
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        ///     The xy-velocity of the particle
        /// </summary>
        public Vector2 Velocity { get; private set; }

        /// <summary>
        ///     The rotation angle of the particle
        /// </summary>
        public float Rotation { get; private set; }

        /// <summary>
        ///     The rotation velocity of the particle
        /// </summary>
        public float RotationVelocity { get; private set; }

        /// <summary>
        ///     The xy scale of the particle render
        /// </summary>
        public float Scale { get; private set; }

        /// <summary>
        ///     The color mask used when rendering
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        ///     The alpha (opacity) of the particle when rendering
        /// </summary>
        public float Alpha { get; private set; }

        /// <summary>
        ///     The amount of time the particle is allowed to live
        ///     (e.g. 1.0f = 1 second)
        /// </summary>
        public float TimeToLive { get; private set; }

        /// <summary>
        ///     The total amount of time the particle has been
        ///     alive (e.g. 1.0f = 1 second)
        /// </summary>
        public float TotalTimeAlive { get; private set; }

        //  The origin used when rendering the particle
        private Vector2 _origin;

        //  Should the particle fade out over time
        private bool _fade;


        //  The emitter responsible for emitting and tracking the particle
        private Emitter _emitter;


        /// <summary>
        ///     Creates a new intance of a particle
        /// </summary>
        /// <param name="texture">The texture used to render</param>
        /// <param name="position">The xy-coordinate starting position of the particle</param>
        /// <param name="velocity">The xy-velocity of the particle</param>
        /// <param name="rotation">The rotation angle of the particle</param>
        /// <param name="rotationVelocity">The rotation velocity of the particle</param>
        /// <param name="scale">The xy scale used when rendering</param>
        /// <param name="timeToLive">The amount of time the particle can live (e.g. 1.0f = 1 second)</param>
        /// <param name="color">The color mask used when rendering the particle</param>
        /// <param name="fade">Should the particle fade out over time</param>
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float rotation, float rotationVelocity, float scale, float timeToLive, Color color, bool fade)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            Scale = scale;
            Color = color;
            Alpha = 1.0f;
            TimeToLive = timeToLive;
            TotalTimeAlive = 0.0f;
            this._origin = new Vector2(Texture.Width, Texture.Height) * 0.5f;
            this._fade = fade;
        }


        /// <summary>
        ///     Updates the particle. Use overload if you are passing GameTime
        ///     down through your update calls
        /// </summary>
        /// <param name="gameTime">The GameTime refernece passed through updates</param>
        public void Update(GameTime gameTime)
        {
            Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        ///     Updates the particle. Use this overload if you already have
        ///     delta tiem calculated
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            //  Increment the total time alive
            TotalTimeAlive += deltaTime;

            //  Check if been alive longer than the time to live
            //  if so, tell the emitter to remove this particle
            if (TotalTimeAlive >= TimeToLive)
            {
                this._emitter.RemoveParticle(this);
            }

            //  Update the position
            Position += Velocity;

            //  Update the Rotation
            Rotation += RotationVelocity;

            //  Check if we are supposed to fade out the particle
            if (this._fade)
            {
                Alpha = MathHelper.LerpPrecise(1.0f, 0.0f, TotalTimeAlive / TimeToLive);
            }
        }


        /// <summary>
        ///     Renders the partcile
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: null,
                color: Color * Alpha,
                rotation: Rotation,
                origin: this._origin,
                scale: Scale,
                effects: SpriteEffects.None,
                layerDepth: 0.0f);
        }

        /// <summary>
        ///     Informs the particle that it's been added
        ///     to a particle emitter
        /// </summary>
        /// <param name="emitter">The Emitter it was added to</param>
        public void AddedToEmitter(Emitter emitter)
        {
            this._emitter = emitter;
        }

        /// <summary>
        ///     Informs the particle that is has been removed
        ///     from a particle emitter
        /// </summary>
        /// <param name="emitter">The emitter it was removed from</param>
        public void RemovedFromEmitter(Emitter emitter)
        {
            this._emitter = null;
        }
    }
}
