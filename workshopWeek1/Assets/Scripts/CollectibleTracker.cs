using UnityEngine;

public static class CollectibleTracker
{
    private static int ducks = 0;

    public static int enoughDucks = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void collectduck()
    {
        ducks++;
    }

    public static bool isItEnough()
    {
        return ducks > enoughDucks;
    }
}
