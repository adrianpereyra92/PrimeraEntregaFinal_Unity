using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform weaponMuzzle;

    [Header ("General")]
    [SerializeField] private LayerMask hittableLayers;
    private Transform cameraPlayerTransform;
    [SerializeField] private GameObject bulletHolePrefab;

    [Header ("Shoot Parameters")]
    [SerializeField] private float fireRange = 200f;
    [SerializeField] private float recoilForce = 4f;
    [SerializeField] private float fireRate = 0.6f;
    [SerializeField] private int maxAmmo = 8;

    [Header ("Reload Parameters")]
    [SerializeField] float reloadTime = 1.5f;
    private int currentAmmo;
    private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header ("Sounds & Visuals")]
    [SerializeField] private GameObject flashEffect;
    public GameObject owner {get; set;}

    private void Awake() 
    {
        currentAmmo = maxAmmo;
    }
    
    void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);

        if (Input.GetButtonDown("Fire1"))
        {
           TryShoot();  
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private bool TryShoot()
    {
        if (lastTimeShoot + fireRate < Time.time)
        {
            if (currentAmmo >= 1)
            {
                HundleShoot();
                currentAmmo -= 1;
                return true;
            }
        }
        return false;
    }
    private void HundleShoot()
    {
       

        GameObject flashClone = Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
        Destroy(flashClone, 1f);

        AddRecoil();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(cameraPlayerTransform.position, cameraPlayerTransform.forward, fireRange, hittableLayers);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != owner)
            {
                GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleClone, 15f);
            }
        }
        
        lastTimeShoot = Time.time;
    }

    private void AddRecoil()
    {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce/50f);
    }
    
    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded...");
    }
    
}
