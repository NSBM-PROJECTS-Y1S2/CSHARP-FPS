using UnityEngine;
using System.Collections;

//edited texts in weapon cs


public int weaponDamage;

//fire method method

Bullet bul = bullet.GetComponent<Bullet>();
bul.bulletDamage = weaponDamage;