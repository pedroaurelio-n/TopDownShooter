using UnityEngine;
using TMPro;
 
namespace PedroAurelio.TopDownShooter
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreHud;

        private int _score;

        private void Awake()
        {
            _score = 0;
            UpdateScoreHud();
        }

        public void AddScore(int scoreToAdd)
        {
            _score += scoreToAdd;
            UpdateScoreHud();
        }

        private void UpdateScoreHud() => scoreHud.text = _score.ToString("000000");
    }
}