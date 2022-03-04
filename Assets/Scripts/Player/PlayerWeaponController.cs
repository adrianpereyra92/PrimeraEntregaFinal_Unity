using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<WeaponController> startingWapons = new List<WeaponController>();
    [SerializeField] private Transform weaponParentSocket;
    [SerializeField] private Transform defaultWeaponPosition;
    [SerializeField] private Transform aimingPosition;
    [SerializeField] private int activeWeaponIndex { get; set; }
    private WeaponController[] weaponSlots = new WeaponController[3];
    void Start()
    {
        activeWeaponIndex = -1;
        foreach (WeaponController startingWeapons in startingWapons)
        {
            AddWeapon(startingWeapons);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Alpha1))
       {
           SwitchWeapon(0);
       } 
    }
    private void SwitchWeapon(int p_weaponIndex)
    {
        if (p_weaponIndex != activeWeaponIndex && p_weaponIndex >= 0)
        {
            weaponSlots[p_weaponIndex].gameObject.SetActive(true);
            activeWeaponIndex = p_weaponIndex;
        }
    }

    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;
        for (int i = 0; i<weaponSlots.Length; i++)
        {
           if (weaponSlots[i] == null)
           {
               WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
               weaponClone.owner = gameObject;
               weaponClone.gameObject.SetActive(false);
               weaponSlots[i] = weaponClone;
               return;
           } 
        }
    }
}
