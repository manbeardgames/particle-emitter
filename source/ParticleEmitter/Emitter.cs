using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ParticleEmitter
{
    public class Emitter
    {
        /// <summary>
        ///     Gets or Sets the xy-coordinate position of the emitter
        /// </summary>
        public Vector2 Position
        {
            get { return this._position; }
            set { this._position = value; }
        }
        private Vector2 _position;

        /// <summary>
        ///     Gets or Sets the x-coordinate position of the emitter
        /// </summary>
        public float X
        {
            get { return this._position.X; }
            set { this._position.X = value; }
        }

        /// <summary>
        ///     Gets or Sets the y-coordinate position of the emitter
        /// </summary>
        public float Y
        {
            get { return this._position.Y; }
            set { this._position.Y = value; }
        }

        /// <summary>
        ///     List of all active particles
        /// </summary>
        public List<Particle> Particles { get; private set; }

        //  The particles to add to master list
        private List<Particle> _toAdd;

        //  THe particles to remove from master list
        private List<Particle> _toRemove;

        //  Random refernece for generator method
        private Random _random;

        /// <summary>
        ///     Creates a new emitter instance
        /// </summary>
        public Emitter(Vector2 position)
        {
            Position = position;
            Particles = new List<Particle>();
            this._toAdd = new List<Particle>();
            this._toRemove = new List<Particle>();
            this._random = new Random();
        }

        /// <summary>
        ///     Updates the emitter. Use this overload if you are
        ///     passing refrence of GameTime throughout updates
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        ///     Updats the emitter. Use this overload if you
        ///     have already calculated delta time
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            //  Update lists first always
            UpdateLists();

            //  Iterate all alive particles
            for (int i = 0; i < Particles.Count; i++)
            {
                //  Update them
                Particles[i].Update(deltaTime);

                // Check for any that need to die
                if (Particles[i].TotalTimeAlive >= Particles[i].TimeToLive)
                {
                    RemoveParticle(Particles[i]);
                }
            }
        }

        /// <summary>
        ///     Updates the particle listss
        /// </summary>
        private void UpdateLists()
        {
            //  If there are any particles to add, add them to master list
            if (this._toAdd.Count > 0)
            {
                for (int i = 0; i < this._toAdd.Count; i++)
                {
                    Particles.Add(this._toAdd[i]);
                    this._toAdd[i].AddedToEmitter(this);
                }
            }

            //  Clear to add list
            this._toAdd.Clear();

            //  If there are any particles to remove, remove them from master list
            if (this._toRemove.Count > 0)
            {
                for (int i = 0; i < this._toRemove.Count; i++)
                {
                    Particles.Remove(this._toRemove[i]);
                    this._toRemove[i].RemovedFromEmitter(this);
                }
            }

            //  Clear remove list
            this._toRemove.Clear();
        }

        /// <summary>
        ///     Adds a new particle to the emitter
        /// </summary>
        /// <param name="particle">The particle to add</param>
        public void AddParticle(Particle particle)
        {
            this._toAdd.Add(particle);
        }

        /// <summary>
        ///     Removes a particle from the emitter
        /// </summary>
        /// <param name="particle">The particle to remove</param>
        public void RemoveParticle(Particle particle)
        {
            this._toRemove.Add(particle);
        }

        /// <summary>
        ///     Renders the particles that are alive
        /// </summary>
        /// <param name="spriteBatch">Reference to your sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //  Iterate all alive particles and render them
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        ///     Generate new particles for the emitter
        /// </summary>
        /// <param name="options">The options to use when generating the particles</param>
        public void GenerateParticles(ParticleGeneratorOptions options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                //Vector2 velocity = new Vector2()
                //{
                //    X = MathHelper.Clamp((float)this._random.NextDouble() * options.Velocity.Maximum - 1,
                //        options.Velocity.Minimum, options.Velocity.Maximum),
                //    Y = MathHelper.Clamp((float)this._random.NextDouble() * options.Velocity.Maximum - 1,
                //        options.Velocity.Minimum, options.Velocity.Maximum)
                //};
                Vector2 velocity = Vector2.Zero;
                switch (options.EmissionType)
                {
                    case EmissionType.Burst:
                        velocity = GenerateBurstVelocity(options.Velocity);
                        break;
                    case EmissionType.UpOnly:
                        velocity = GenerateUpOnlyVelocity(options.Velocity);
                        break;
                    case EmissionType.DownOnly:
                        velocity = GenerateDownOnlyVelocity(options.Velocity);
                        break;
                    case EmissionType.LeftOnly:
                        velocity = GenerateLeftOnlyVelocity(options.Velocity);
                        break;
                    case EmissionType.RightOnly:
                        velocity = GenerateRightOnlyVelocity(options.Velocity);
                        break;
                    default:
                        velocity = GenerateBurstVelocity(options.Velocity);
                        break;
                }

                float rotation = 0.0f;

                float rotationVelocity = 0.1f * MathHelper.Clamp((float)this._random.NextDouble() * options.RotationVelocity.Maximum - 1,
                    options.RotationVelocity.Minimum, options.RotationVelocity.Maximum);

                float scale = MathHelper.Clamp((float)this._random.NextDouble() * options.Scale.Maximum,
                    options.Scale.Minimum, options.Scale.Maximum);

                float timeToLive = MathHelper.Clamp((float)this._random.NextDouble() * options.TimeToLive.Maximum,
                    options.TimeToLive.Minimum, options.TimeToLive.Maximum);

                Particle particle = new Particle(options.Texture, Position, velocity, rotation, rotationVelocity,
                    scale, timeToLive, options.Color, options.Fade);

                AddParticle(particle);
            }
        }

        private Vector2 GenerateBurstVelocity(MinMaxFloat velocity)
        {
            Vector2 value = new Vector2()
            {
                X = MathHelper.Clamp((float)this._random.NextDouble() * velocity.Maximum - 1,
                    velocity.Minimum, velocity.Maximum),
                Y = MathHelper.Clamp((float)this._random.NextDouble() * velocity.Maximum - 1,
                    velocity.Minimum, velocity.Maximum)
            };

            //  random.next(2) * 2 -1 will produce randomly either -1 or 1, to allow us
            //  to randomly set if the velocity of x and/or y is positive or negative
            value.X *= this._random.Next(2) * 2 - 1;
            value.Y *= this._random.Next(2) * 2 - 1;
            return value;
        }

        private Vector2 GenerateUpOnlyVelocity(MinMaxFloat velocity)
        {
            Vector2 value = new Vector2()
            {
                X = 0,
                Y = -MathHelper.Clamp((float)this._random.NextDouble() * velocity.Maximum - 1,
                    velocity.Minimum, velocity.Maximum)
            };
            return value;
        }

        private Vector2 GenerateDownOnlyVelocity(MinMaxFloat velocity)
        {
            Vector2 value = new Vector2()
            {
                X = 0,
                Y = MathHelper.Clamp((float)this._random.NextDouble() * velocity.Maximum - 1,
                    velocity.Minimum, velocity.Maximum)
            };
            return value;
        }

        private Vector2 GenerateLeftOnlyVelocity(MinMaxFloat velocity)
        {
            Vector2 value = new Vector2()
            {
                X = -MathHelper.Clamp((float)this._random.NextDouble() * velocity.Maximum - 1,
                    velocity.Minimum, velocity.Maximum),
                Y = 0
            };
            return value;
        }

        private Vector2 GenerateRightOnlyVelocity(MinMaxFloat velocity)
        {
            Vector2 value = new Vector2()
            {
                X = MathHelper.Clamp((float)this._random.NextDouble() * velocity.Maximum - 1,
                    velocity.Minimum, velocity.Maximum),
                Y = 0
            };
            return value;
        }


    }
}
