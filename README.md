# MonoGame 2D Particle Emitter
This is a simple 2D particle emitter that can be used in a MonoGame project

# Installtion
To install you can clone the source form this github repository, or download the release .dll and reference that in your project.

## Cloning source
If you choose to clone the source, after cloning it, do the following
1. Right-Click the Solution in the Solution Explorer window in your of your MonoGame project
2. Choose "Add Existing Project"
3. Navigate to and choose the ParticleEmitter.csproj in the "source/ParticleEmitter/ directory of the cloned repository
4. Right-Click your MonoGame project in the Solution Explorer
5. Choose "Add > Reference"
6. Expand Project on the left pane, click "Solution" and then check the ParticleEmitter, then click ok

## Downloading ParticleEmitter.dll
If you choose to download the .dll, perform teh following after downloading
1. Right-click the MonoGame project in the Solution Explorer window
2. Choose "Add > Reference"
3. Choose "Browse" on the left side panel
4. Click the Browse button at the bottom
5. Navigate to and select the ParticleEmitter.dll you downloaded
6. Click Ok


# Usage
To use the particle emitter first add the using namespace

```csharp
using ParticleEmitter;
```

Then create a new particle emitter object giving it a position to be located at

```csharp
Vector2 position = new Vector2(100, 100);
ParticleEmitter emitter = new ParticleEmitter(position);
```


Call the update for the emitter in your Update method

```csharp
emitter.Update(gameTime);
```

Call the draw for the emitter in your Draw method

```csharp
emitter.Draw(spriteBatch);
```

When generating particles, you'll need to supply the emitter with a set of `ParticleGeneratorOptions`. Below is an example of these

```csharp
//  Create the generator options for our particle emitter
ParticleGeneratorOptions generatorOptions = new ParticleGeneratorOptions()
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
```

Then when ready to create particles, tell the emitter to genreate them like so

```csharp
emitter.GenerateParticles(generatorOptions);
```



