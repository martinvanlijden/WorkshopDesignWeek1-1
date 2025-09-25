using UnityEngine;

public class Swarmbait : MonoBehaviour
{
    public static Vector3 LeaderPosition { get; private set; }

    [SerializeField] private static int health { get; set; } = 100;
    void Update()
    {
        // Keep updating the leader's position
        LeaderPosition = transform.position;
    }

    public static void decreaseHealth()
    {
        health--;
    }
}
