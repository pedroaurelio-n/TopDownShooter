using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
namespace TopDownShooter
{
    public class PlayerLivesUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI livesHud;

        public void UpdateLives(int playerHealth)
        {
            livesHud.text = "";

            for (int i = 0; i < playerHealth; i++)
            {
                livesHud.text += "X";
            }
        }
    }
}