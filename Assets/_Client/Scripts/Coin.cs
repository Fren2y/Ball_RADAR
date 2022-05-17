using UnityEngine;

namespace Ball_Radar
{
    public class Coin : MonoBehaviour, iInteractive
    {
        public void Deativate()
        {
            Destroy(gameObject);
        }
    }
}
