using UnityEngine;


namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a gameobject as a spawnpoint in a scene.
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        public Sprite OldSprite;
        public Sprite NewSprite;

        private SpriteRenderer m_NewCheckPoint;
        private SpriteRenderer m_OldCheckPoint;

        void Start()
        {
            m_NewCheckPoint = GetComponent<SpriteRenderer>(); 
            
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            //Désactivation possible du GameObject, à voir dans le gameplay
            m_OldCheckPoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpriteRenderer>();
            
            GameObject.FindGameObjectWithTag("Respawn").tag = "Untagged";
            
            this.tag = "Respawn";

            m_OldCheckPoint.sprite = OldSprite;
            m_NewCheckPoint.sprite = NewSprite;

            m_NewCheckPoint.flipX = false;

            GameObject.FindGameObjectWithTag("Countdown").GetComponent<CountDown>().AddTime();
        }
    }
}