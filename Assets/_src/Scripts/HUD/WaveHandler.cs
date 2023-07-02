using UnityEngine;
using TMPro;
 
namespace PedroAurelio.TopDownShooter
{
    public class WaveHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveHud;

        public void UpdateCurrentWave(int currentWave)
        {
            waveHud.text = currentWave.ToString();
        }
    }
}