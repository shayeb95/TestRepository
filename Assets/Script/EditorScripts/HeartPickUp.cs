using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{

    public AudioClip noSound;
    public AudioClip pickupSound;
    public GameObject graphic;
    AudioSource src;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.attachedRigidbody.GetComponent<Player>())
            {
                Player player = collision.attachedRigidbody.GetComponent<Player>();
                if (!player.IsMaxHearts())
                {
                    player.AddHeart();
                    graphic.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    if(src && pickupSound)
                    {
                        src.PlayOneShot(pickupSound);
                    }
                }
                else if (src && noSound)
                {
                    src.PlayOneShot(noSound);
                }

            }
        }

    }
}
