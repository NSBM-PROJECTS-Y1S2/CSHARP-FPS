using UnityEngine;
using System.Collections;

public class Throwables : MonoBehaviour
{
    // Updates on throwables script


// damage to enemy

if (objectInRange.gameObject.GetComponent<Enemy>())
{
        objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(100);
}
    