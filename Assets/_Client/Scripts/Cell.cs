using UnityEngine;

namespace Ball_Radar
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private Transform _leftWall;
        [SerializeField]
        private Transform _rightWall;

        private float _wallOffset;

        [SerializeField]
        private BoxCollider _spawnZone;

        private float _cellHeight;
        public float CellHeight
        {
            get => transform.localScale.y;
            private set => _cellHeight = value;
        }

        public void ActivateCell(LevelGenerator lGen)
        {
            _wallOffset = Mathf.Abs(_leftWall.localPosition.x);

            int rCoins = Random.Range(0, 3);
            int rObstacles = Random.Range(0, 3);

            for (int i = 0; i < rCoins; i++)
            {
                Instantiate(lGen.GetCoin, GetRandomPointInsideCollider(_spawnZone), Quaternion.identity, lGen.transform);
            }

            for (int i = 0; i < rObstacles; i++)
            {
                Instantiate(lGen.GetObstacle, GetRandomPointInsideCollider(_spawnZone), Quaternion.identity, lGen.transform);
            }
        }

        private void Start()
        {
            Game.inst.e_movePos.AddListener(MoveLeftWall);
        }

        private void OnDestroy()
        {
            Game.inst.e_movePos.RemoveListener(MoveLeftWall);
        }

        public Vector3 GetRandomPointInsideCollider(BoxCollider boxCollider)
        {
            Vector3 extents = boxCollider.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
            );

            return boxCollider.transform.TransformPoint(point);
        }

        private void MoveLeftWall(float value)
        {
            _leftWall.localPosition = Vector3.right * (-_wallOffset + value);
            _rightWall.localPosition = Vector3.right * (_wallOffset - value);
        }

    }
}
