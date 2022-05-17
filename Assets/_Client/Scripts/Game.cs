using UnityEngine;
using UnityEngine.Events;

namespace Ball_Radar
{
    public class Game : MonoBehaviour
    {
        public static Game inst;

        public UnityEvent<bool> e_startGame;

        public UnityEvent<float> e_LeftWallMove;
        public UnityEvent<float> e_RIghtWallMove;

        public UnityEvent<int> e_updateScore;
        public UnityEvent<int> e_updateLifes;

        public UnityEvent<float> e_movePos;

        private int _bestScore;
        public int BestScore
        {
            get => _bestScore;
            private set => _bestScore = value;
        }

        [SerializeField] private bool _movableWalls;
        public void MoveWalls(bool value)
        {
            _movableWalls = value;
        }

        private bool _gameStarted;

        private void Awake()
        {
            //Init Singleton
            if (inst == null) inst = this;
            else if (inst == this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (!_gameStarted) return;
            if (_movableWalls)
            {
                MoveWalls(Mathf.PingPong(Time.time, 1));
            }
        }

        #region Events
        private void MoveWalls(float offset)
        {
            e_movePos.Invoke(offset);
        }

        public void StartGame(bool value)
        {
            _gameStarted = value;
            e_startGame?.Invoke(value);
        }

        public void UpdateBestScore(int score)
        {
            BestScore = score;
        }

        public void UpdateScores(int value)
        {
            e_updateScore?.Invoke(value);
        }

        public void UpdateLifes(int value)
        {
            e_updateLifes?.Invoke(value);
        }
        #endregion
    }
}
