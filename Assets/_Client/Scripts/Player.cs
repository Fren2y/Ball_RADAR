using UnityEngine;

namespace Ball_Radar
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [System.Serializable]
        public struct PlayerSpeedUpgrade
        {
           public int reqForUpgrade;
           public float acceleration;
        }

        private Rigidbody _ballRB;
        private int _currentScore;
        private int _currentLifes;

        [SerializeField]
        private float _ballSpeed;

        private float _curBallSpeed;
        private float _nextBallSpeed;

        [SerializeField]
        private float _pushFromWallSpeed;

        [SerializeField]
        private float _ballSpeedAcceleration;

        private float _sidePos;

        private bool _active;

        [SerializeField] private PlayerSpeedUpgrade[] mySpeedUpgrades;
        private int _curSpeedUpgrade;

        private void Awake()
        {
            _ballRB = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Game.inst.e_startGame.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            Game.inst.e_startGame.RemoveListener(StartGame);
        }

        private void FixedUpdate()
        {
            if (!_active) return;

            _curBallSpeed = Mathf.Lerp(_curBallSpeed, _nextBallSpeed, Time.deltaTime * _ballSpeedAcceleration);

            _ballRB.velocity = Vector3.up * _curBallSpeed;
        }

        public void MoveSide(float dir)
        {
            _ballRB.AddForce(Vector3.right * dir, ForceMode.VelocityChange);
        }

        public void PushFromWall(float dir)
        {
            _ballRB.position = new Vector3(0, _ballRB.position.y, 0);
        }

        private System.Collections.IEnumerator BallHitAnimation()
        {
            for (int i = 0; i < 10; i++)
            {
                _ballRB.GetComponent<MeshRenderer>().material.SetColor("_Color", i % 2 == 0 ? Color.red : Color.white);
                yield return new WaitForSeconds(Mathf.Clamp(0.25f - (float)i / 40, 0.01f, 1));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                AddScore();
            }
            else if (other.TryGetComponent(out Obstacle obst))
            {
                LooseLife();
            }
            else if (other.TryGetComponent(out Wall wall))
            {
                LooseLife();
                PushFromWall(transform.position.x - other.transform.position.x);
            }

            if (other.TryGetComponent(out iInteractive inter))
            {
                inter.Deativate();
            }
        }

        private void AddScore()
        {
            _currentScore++;

            if (_curSpeedUpgrade < mySpeedUpgrades.Length)
            {
                if (_currentScore >= mySpeedUpgrades[_curSpeedUpgrade + 1].reqForUpgrade)
                {
                    _curSpeedUpgrade++;
                    _nextBallSpeed = _ballSpeed * mySpeedUpgrades[_curSpeedUpgrade].acceleration;
                }
            }

            Game.inst.UpdateScores(_currentScore);
        }

        private void LooseLife()
        {
            _currentLifes--;

            if (_currentLifes <= 0)
            {
                Game.inst.StartGame(false);
            }
            else
            {
                StartCoroutine(BallHitAnimation());
            }

            Game.inst.UpdateLifes(_currentLifes);
        }

        private void StartGame(bool value)
        {
            _active = value;
            _ballRB.isKinematic = !value;
           
            if (value)
            {
                _nextBallSpeed = _ballSpeed * mySpeedUpgrades[_curSpeedUpgrade].acceleration;

                _curSpeedUpgrade = 0;
                _currentLifes = 3;
                _currentScore = 0;

                Game.inst.UpdateScores(_currentScore);
                Game.inst.UpdateLifes(_currentLifes);

                _ballRB.position = Vector3.zero;
            }
        }
    }
}
