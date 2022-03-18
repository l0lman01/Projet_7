using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    public Turret turret;

    public IEnumerator NightEnnemy()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(turret.TurretNight());
        


    }

    public IEnumerator DayEnnemy()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(turret.TurretDay());

    }
}
