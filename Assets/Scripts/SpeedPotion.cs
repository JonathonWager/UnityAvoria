using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    public float speedBuff;
    public float duration;
    private GameObject player;
    private bool beenPickedUp =false;

    void DestroyPotion(){
        if(!beenPickedUp){
            Destroy(this.gameObject);
        }
    }
    void ResetBuff(){
        player.gameObject.GetComponent<characterStats>().movementBuff -= speedBuff;
        Destroy(this.gameObject);
    }
      public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            beenPickedUp = true;
            player = other.gameObject;
            other.gameObject.GetComponent<characterStats>().movementBuff += speedBuff;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();

            // Disable the SpriteRenderer and CircleCollider2D if they exist
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;

            if (circleCollider != null)
                circleCollider.enabled = false;
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

// Invoke the ResetBuff function after the specified duration
            Invoke("ResetBuff", duration);
        }
    }
      public void Start(){
       Invoke("DestroyPotion",15f);
    }
}
