using TMPro;
using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class PlayerAmmoUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoHud;

        public void UpdateAmmo(int currentAmmo)
        {
            if (currentAmmo == -1)
                ammoHud.text = "inf";
            else
                ammoHud.text = currentAmmo.ToString();
        }
    }
}