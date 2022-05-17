using UnityEngine;
using UnityEngine.UI;

namespace Ball_Radar
{
    public class UI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _menuWindow;
        [SerializeField]
        private Text _scoreText;
        [SerializeField]
        private Text _lifesText;

        private void Start()
        {
            Game.inst.e_startGame.AddListener(StartGame);
            Game.inst.e_updateScore.AddListener(UpdateScores);
            Game.inst.e_updateLifes.AddListener(UpdateLifes);
        }

        private void OnDestroy()
        {
            Game.inst.e_startGame.RemoveListener(StartGame);
            Game.inst.e_updateScore.RemoveListener(UpdateScores);
            Game.inst.e_updateLifes.RemoveListener(UpdateLifes);
        }

        private void StartGame(bool value)
        {
            _menuWindow.SetActive(!value);
        }

        private void UpdateScores(int value)
        {
            _scoreText.text = "Scores: " + value.ToString();
        }

        private void UpdateLifes(int value)
        {
            _lifesText.text = "Lifes: " + value.ToString();
        }

        #region Public

        public void PlayBtn()
        {
            Game.inst.StartGame(true);
        }

        #endregion
    }
}
