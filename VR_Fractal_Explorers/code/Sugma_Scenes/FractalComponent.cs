using Sandbox;

namespace SugmaScenes;

public sealed class FractalComponent : Component
{
    [Property] public GameObject FractalObject { get; set; }
    [Property] public float Size { get; set; } = 100.0f;
    [Property, Range(1,4)] public int Depth { get; set; } = 3;

    private GameObject Root = new GameObject( true, "Generated Fractal" );

    protected override void OnStart()
    {
        base.OnStart();

        SpawnFractal();
    }

    public void SpawnFractal( Vector3 position, float size, int depth )
    {
        // Create a new cube at the given position
        FractalObject.Clone( Root, position, Rotation.Identity, size / Size );

        // If we've reached the maximum depth, stop recursing
        if ( depth <= 0 ) return;

        // Calculate the size and position for the smaller cubes
        float newSize = size / 3;
        float offset = newSize;

        // Spawn smaller cubes in each corner of the current cube
        for ( int x = -1; x <= 1; x += 2 )
        {
            for ( int y = -1; y <= 1; y += 2 )
            {
                for ( int z = -1; z <= 1; z += 2 )
                {
                    Vector3 newPosition = position + new Vector3( x, y, z ) * offset;
                    SpawnFractal( newPosition, newSize, depth - 1 );
                }
            }
        }
    }

    public void SpawnFractal()
    {
        // Spawn a fractal cube at the center of the map
        SpawnFractal(Transform.Position, Size, Depth );
    }
}