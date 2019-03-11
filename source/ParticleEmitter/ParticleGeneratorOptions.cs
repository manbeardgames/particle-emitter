using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEmitter
{
    public struct ParticleGeneratorOptions
    {
        /// <summary>
        ///     The Texture to use for the particles
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        ///     The total number of particles to generate
        /// </summary>
        public int Count;

        /// <summary>
        ///     The color mask to use when rendering the particles
        /// </summary>
        public Color Color;

        /// <summary>
        ///     The minimum and maximum velocity allowed for the particles
        /// </summary>
        public MinMaxFloat Velocity;

        /// <summary>
        ///     The type of emission the particles will follow
        /// </summary>
        public EmissionType EmissionType;

        /// <summary>
        ///     The minimum and maximum rotation velocity allowed for the particles
        /// </summary>
        public MinMaxFloat RotationVelocity;

        /// <summary>
        ///     The minimum and maximum to scale the render of the particles
        /// </summary>
        public MinMaxFloat Scale;

        /// <summary>
        ///     The minimum and maximum time allowed for particles to live
        /// </summary>
        public MinMaxFloat TimeToLive;

        /// <summary>
        ///     Should the particles fade out over time before being destoryed
        /// </summary>
        public bool Fade;
    }

    /// <summary>
    ///     A simple struct to represent minimum and maximum float values
    /// </summary>
    public struct MinMaxFloat
    {
        public float Minimum;
        public float Maximum;
    }
}
