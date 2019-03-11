# MinMaxFloat Struct
Namespace: ParticleEmitter

Stores values that represent the minimum and maximum values for a property as floats

```csharp
public struct MinMaxFloat
```

# Constructors
| Signature | Description |
|---|---|
| MinMaxFloat() | Initializes a new instance of the ParticleGeneratorOptions |

# Properties
| Name | Type | Description |
|---|---|---|
| Maximum | float | The maximum represented value. |
| Minimum | float | The minimum represented value. |


# Example
The following is an example of creating a new MinmaxFloat instance 

```csharp
MinMaxFloat minMax = new MinMaxFloat() { Minimum = 1.0f, Maximum = 2.0f },
```



