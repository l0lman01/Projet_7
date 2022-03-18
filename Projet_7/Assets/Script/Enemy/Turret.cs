using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float reloadTimeDay= 2;
    public float reloadTimeNight= 10;
   // public float destroyDelay= 4;
    public GameObject Projectile;  
    public IEnumerator TurretDay()
    {
        while (enabled)
        {
            StopCoroutine(TurretNight());
            yield return new WaitForSeconds(reloadTimeDay);
            Instantiate(Projectile, transform.position, Quaternion.identity);
        }
    }
    public IEnumerator TurretNight()
    {
        while (enabled)
        {
            StopCoroutine(TurretDay());
            yield return new WaitForSeconds(reloadTimeNight);
            Instantiate(Projectile, transform.position, Quaternion.identity);
        }
    }

    // A netoyer



    //public IEnumerator BulletDestroyTime()
    //{
    //    while (enabled)
    //    {
    //        StopCoroutine(BulletDestroyTime());
    //        yield return new WaitForSeconds(destroyDelay);
    //        Destroy(GameObject.FindGameObjectWithTag("Bullet"));
    //    }
    //}

}
