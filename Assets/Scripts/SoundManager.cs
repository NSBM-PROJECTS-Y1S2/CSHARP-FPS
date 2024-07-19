using System Collections; 
using System.Collections.Generic; 
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; set; }
    public AudioSource shootingChannel;

    public AudioClip P1911Shot;
    public AudioClip M16Shot;

    public AudioSource reloadingSoundM16;
    public AudioSource reloadingSound1911;
    public AudioSource emptyManagizeSound1911;
   
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel1 weapon)
    {
        switch (weapon)
        {
            case WeaponModel1.Pistol1911:
                shootingChannel.PlayOneShot(P1911Shot);
                break;
            case WeapoModel.M16:
                shootingChannel.PlayOneShot(M16Shot);
               break;
        }
    }
    public void PlayReloadsound(WeaponModel1 weapon)
    {
        switch (weapon)
        {
            case WeaponModel1.Pistol1911:
                reloadingSound1911.Play();
                break;
            case WeapoModel.M16:
               reloadingSoundM16.Play();
               break;
        }
    }
}      
