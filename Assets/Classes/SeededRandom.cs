
using System;

public class SeededRandom
{

    public Random rng;

    public SeededRandom(int seed)
    {
        rng = new Random(seed);
    }


    public int Range(int min, int max)
    {
        return rng.Next(min, max);
    }

   public float Range(float min, float max)
    {
        return (float)(min + rng.NextDouble() * (max - min));
    }

    public bool Bool()
    {
        return rng.NextDouble() > 0.5;
    }

}
