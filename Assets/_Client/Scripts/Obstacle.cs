using UnityEngine;

public class Obstacle : MonoBehaviour, iInteractive
{
    public void Deativate()
    {
        Destroy(gameObject);
    }
}
