using UnityEngine;

namespace Ball_Radar
{
    public class PlayerInput : MonoBehaviour
    {
        Player _player;

        private Vector2 _oldMousePos;

        private bool _gameStarted;

        private void Start()
        {
            _player = GetComponent<Player>();
            Game.inst.e_startGame.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            Game.inst.e_startGame.RemoveListener(StartGame);
        }

        private void StartGame(bool value)
        {
            _gameStarted = value;
        }
        private void Update()
        {
            if (!_gameStarted) return;

#if UNITY_EDITOR
            Vector2 pos = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                _oldMousePos = pos;
            }

            if (Input.GetMouseButton(0))
            {
               
                Vector2 dir =  pos - _oldMousePos;
                _player.MoveSide(dir.x);
                _oldMousePos = pos;
            }
#elif UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _player.MoveSide(Input.GetTouch(0).deltaPosition.x);
                }
            }
#else
             Vector2 pos = Input.mousePosition;

            Vector2 dir =  pos - _oldMousePos;

            if (Input.GetMouseButton(0))
            {
                _player.MoveSide(dir.x);
            }

            _oldMousePos = pos;
#endif
        }
    }
}
