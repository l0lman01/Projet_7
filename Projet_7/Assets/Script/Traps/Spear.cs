using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [Header("Spear Trigger")]
    [SerializeField] private float activationDelay; //Délai d'activation
    [SerializeField] private float activeTime; //Pendant combien temps il reste actif

    private Animator anim;
    private SpriteRenderer m_spriteRenderer;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(DaySpear());
            }
            else
                StartCoroutine(NightSpear());
            if (active)
                collision.GetComponent<PlayerController>().EnteredDeathZone();
        }
    }

    private IEnumerator DaySpear()
    {
        //Active le piège
        triggered = true;
        yield return new WaitForSeconds(activationDelay);
        active = true;
        anim.SetBool("triggerBool", true);

        //Reset
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("triggerBool", false);
    }

    private IEnumerator NightSpear()
    {
        triggered = true;
        active = true;
        yield return new WaitForSeconds(activationDelay);
        //changer l'animation
        //anim.runtimeAnimatorController.
    }
}
