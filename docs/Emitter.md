# Emitter Class
Namespace: ParticleEmitter

Represents an emitter that generates, tracks, updates, and renders it's own Particles.

```csharp
public class Emitter
```

# Constructors
| Signature | Description |
|---|---|
| Emitter(Vector2) | Initializes a new instance of the Emitter at the xy-coordinate location given |

# Properties
| Name | Type | Description |
|---|---|---|
| Particles | List<Particle> |  Gets the List<Particle> collection of all particles that have been emitted and are still alive. |
| Position | Vector2 | Gets or Sets the xy-coordinate location of the Emitter. |
| X | float | Gets or Sets the x-coordinate location of the Emitter. |
| Y | float | Gets or Sets the y-coordinate location of the Emitter. |

# Methods
| Signature | Description |
|---|---|
| AddParticle(Particle) | Adds a new particle to the Emitter. Particles added will tracked and managed starting with the next Update cycle. |
| Draw(SpriteBatch) | Renders the alive Particles emitted from this Emitter. |
| GenerateParticles(ParticleGeneratorOptions) | Generates particles from the Emitter based on the options provided. |
| RemoveParticle(Particle) | Removes a particle from the Emitter. Particles are removed during the next Update cycle. |
| Update(GameTIme) | Updates the Emitter. **Use this overload if you are passing the GameTime refernece down through your update calls.** |
| Update(float) |  Updates the Emitter. **Use this overload if you already have a delta time value calculated.** |


# Remarks
When creating a new Emitter, the position given to the constructor determines the location the emitter sits.  This location is where all particles generated from the emitter are emitted from.

While Particle instances can be added and removed directly using the `AddParticle(Particle)` and `RemoveParticle(Particle)` methods, it is recommended that you instead us the `GenerateParticles(ParticleGeneratorOptions)` method call instead.

# Example
The following example creates a new Emitter instance and tells it to generate 10 particles using the Burst EmissionType every 5 seconds;

```csharp
public class Game1 : Game
{

    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    //  Reference to our Emitter
    private Emitter _emitter;

    //  Reference to our ParticleGeneratorOptions
    private ParticleGeneratorOptions _options;

    //  Timer used to generate particles every 5 seconds
    private float _timer;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        //  Create a new Emitter at position {x:100, y:100}
        _emitter = new Emitter(new Vector2(100, 100));

        base.Initialize();
    }

    protected override void LoadContent() 
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        //  Create the options to generate the particles using the Burst EmissionType
        _options = new ParticleGeneratorOptions() 
        {
            //  Emissiontype.Burst will randomly make the x and/or y velocity positive or negative
            EmissionType = EmissionType.Burst,
            //  The texture used when rendering the particles
            Texture = Content.Load<Texture2D>("particleTexture"),
            //  The color mask to apply when rendering the particles.
            Color = Color.White,
            //  Particles should live betwen 1 and 1.5 seconds
            TimeToLive = new MinMaxFloat() { Minimum = 1.0f, Maximum = 1.5f },
            //  Only generate 10 particles
            Count = 10,
            //  Apply between 1.0 and 4.0 velocity to the x and y axis of the particles
            Velocity = new MinMaxFloat() { Minimum = 1.0f, Maximum = 4.0f },
            //  Apply between 1.0 and 2.0 velocity to the rotation of the particles.
            RotationVelocity = new MinMaxFloat() { Minimum = 1.0f, Maximum = 2.0f },
            //  Scall the particles along the x and y axis between 0.1 and 1.0
            Scale = new MinMaxFloat() { Minimum = 0.1f, Maximum = 1.0f },
            //  The particles should not fade out over the duration of the time they are alive.
            Fade = false
        };
    }

    protected override void Update(GameTime gameTime)
    {
        //  Calculate delta time
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        //  Update the emitter
        _emitter.Update(deltaTime);

        //  Increase our particle Timer
        _timer += deltaTime;

        //  If the timer is more than 5 seconds, reset timer and generate particles
        if(_timer >= 5.0f)
        {
            _timer = 0.0f;
            _emitter.GenerateParticles(_options);
        }

        base.UpdateGame(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        //  Begin the sprite batch
        spriteBatch.Begin();

        //  Draw the Emitter
        _emitter.Draw(spriteBatch);

        //  End the spriteBatch
        spriteBatch.End();

        base.Draw(gameTime);
    }


}
```



