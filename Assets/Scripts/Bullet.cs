using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPooledObject
{
    private ObjectPooler objectPooler;
    
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Interface Start method when object is spawned
    public void OnObjectSpawn()
    {
    } 
    


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            print("hit" + collision.gameObject.name + "!");
            //Destroy(gameObject);
            objectPooler.ReturnToPool("Bullet", this.gameObject);
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");
            //Destroy(gameObject);
            objectPooler.ReturnToPool("Bullet", this.gameObject);
        }
    }
}
