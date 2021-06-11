using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragScript : MonoBehaviour
{
    public AudioSource collision;
    private bool isDragging;
    private bool isleft;
    public float spacing;
    private float offset = -3 ;
    private Vector3 startingPoint;
    public GameObject Remaining;
    private GameObject player;
    public string direction;
    private SpriteRenderer spriteRenderer;

    void Awake ()
    {
      startingPoint = transform.position;
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      player = GameObject.FindGameObjectWithTag("Player");
      isleft = false;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if ( isleft == false )
        { return; }
        if (col.gameObject.tag == "Ground" && isDragging == false )
        {
            collision.Play();
            player.GetComponent<PlayerController>().realease(direction);
            isleft = false;
            gameObject.transform.position = startingPoint;
        }
    }

    public void ResetKeys()
    {
        gameObject.transform.position = startingPoint;
    }

    private void reCenter()
    {
        if (Vector3.Distance(transform.position, startingPoint) <= 0.5f)
        {
            player.GetComponent<PlayerController>().realease(direction);
            isleft = false;
            gameObject.transform.position = startingPoint;
        }
    }

    public void OnMouseDown()
    {
        isDragging = true;
        isleft = true;
        Remaining.GetComponent<MoveRemaining>().listener(false);
 
    }

    public void OnMouseUp()
    {
        reCenter();
        isDragging = false;
        Remaining.GetComponent<MoveRemaining>().listener(true);
        if ( isleft )
        {
            player.GetComponent<PlayerController>().block(direction);
            Physics2D.IgnoreCollision(player.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            gameObject.tag = "Tangible";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(direction) && !UIScript.gameIsPaused)
        {
            StartCoroutine(Change());
        }

        if (isDragging && Remaining.GetComponent<MoveRemaining>().GetComponent<Image>().fillAmount > 0f ) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            spriteRenderer.color = Color.red;
            transform.Translate(mousePosition); 
        }
        if ( isleft == false ) {
            gameObject.transform.position = new Vector3(player.transform.position.x + spacing, player.transform.position.y + offset, 0f);
            Physics2D.IgnoreCollision(player.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            startingPoint = transform.position;
        }
    }

    IEnumerator Change()
    {
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
}
