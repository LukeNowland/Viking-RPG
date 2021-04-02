using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weaponPickup = null;
        [SerializeField] float respawnTime = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weaponPickup);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool isShown)
        {
            GetComponent<Collider>().enabled = isShown;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isShown);
            }
        }
    }
}
