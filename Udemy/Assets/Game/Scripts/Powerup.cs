using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = tripple shot 1 = speed boost, 2 = shields
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);  

        if (transform.position.y < -4f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
               
                if (powerupID == 0)
                {
                    //Enable Triple shot
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    //Enable speed boost here
                    player.SpeedPowerupOn();
                }
                else if (powerupID == 2)
                {
                    //Enable shields here
                    player.ShieldPowerupOn();
                }
                
            }

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            //Destroy ourselves
            Destroy(this.gameObject);
        }
    }
}
