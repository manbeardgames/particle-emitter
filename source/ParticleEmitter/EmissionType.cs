using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleEmitter
{
    public enum  EmissionType
    {
        /// <summary>
        ///     Particles will burst from emitter in random directions
        /// </summary>
        Burst,

        /// <summary>
        ///     Particles will exit emitter going up only
        /// </summary>
        UpOnly,

        /// <summary>
        ///     Particles will exit going down only
        /// </summary>
        DownOnly,

        /// <summary>
        ///     Particles will exit emmiter going left only
        /// </summary>
        LeftOnly,

        /// <summary>
        ///     Particles will exit emiiter going right only
        /// </summary>
        RightOnly
    }
}
