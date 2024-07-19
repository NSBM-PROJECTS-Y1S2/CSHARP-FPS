using System Collections;
using System Collections Generic; 
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;


    public Sprite emptySlot;
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


    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bullets PerBurst}";
            totalAmmoUI.text $"{activeWeapon.magazineSize / activeWeapon.bullets PerBurst}";

            Weapon WeaponModel model = activeWeapon thisWeapon Model;
            ammoTypeUI sprite = GetAmmoSprite(model);

            activeWeaponUI.sprite = GetWeapon Sprite(model);

            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon thisWeaponModel);
            }
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            ammoTypeUI.sprite = emptySlot;

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }

    }

    private Sprite GetWeaponSprite(Weapon Weapon Model model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Instantiate(Resources.Load<GameObject>("Pistol1911_Weapon")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M16:
                return Instantiate(Resources.Load<GameObject>("M16_Weapon")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }
    }

    private Sprite GetAmmoSprite(Weapon WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Instantiate(Resources.Load<GameObject>("Pistol1911_Ammo")).GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M16:
                return Instantiate(Resources.Load<GameObject>("M16_Ammo")).GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
        }
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlot)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        //this will never happen,but we need to return something
        return null;
    }
}
