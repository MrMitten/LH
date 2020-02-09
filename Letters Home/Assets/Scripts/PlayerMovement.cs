using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_dead;
    Rigidbody2D me;
    public float MoveSpeed = 1.0f;
    Vector2 MoveVector = new Vector2();
    public bool duck;
    public bool crawl;
    public bool Up;
    public List<Vector2> BoxSizes;
    public BoxCollider2D mine;
    Animator Anim;

    private bool canVault;
    private Transform VaultPos;
    private float timerS;
    private float timerW;
    private float doubleTapTimer = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        mine = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_dead)
        {
            MoveVector = Vector2.zero;

            MoveVector += new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, 0);

            me.transform.position += new Vector3(MoveVector.x, MoveVector.y, me.transform.position.z) * Time.deltaTime;



            //Double tap code.
            /*if (timerS < Time.time && Input.GetKeyDown(KeyCode.S))
            {
                timerS  = Time.time + doubleTapTimer;
            }
            else if (timerS > Time.time && Input.GetKeyDown(KeyCode.S))
            {
                timerS = Time.time;
                ToggleDown();
            }

            if (timerW < Time.time && Input.GetKeyDown(KeyCode.W))
            {
                timerW = Time.time + doubleTapTimer;
            }
            else if (timerW > Time.time && Input.GetKeyDown(KeyCode.W))
            {
                timerW = Time.time;
                ToggleUp();
            }*/

            if (Input.GetKeyDown(KeyCode.S))
            {
                timerS = Time.time;
                ToggleDown();
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                timerW = Time.time;
                ToggleUp();
            }

            if (canVault && Input.GetKeyDown(KeyCode.F))
            {
                transform.position = VaultPos.position;
                canVault = false;
            }

            

            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                Anim.SetBool("Walkin", true);
            }
            else if ((Input.GetAxis("Horizontal") > 0))
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                Anim.SetBool("Walkin", true);
            }
            else
                Anim.SetBool("Walkin", false);


            if(crawl && mine.offset.y != -4f)
            {
                mine.offset = new Vector2(mine.offset.x, -.4f);
                mine.size = BoxSizes[2];


                Anim.SetBool("Crawlin", true);
                Anim.SetBool("Crouch", false);
            }
            else if (duck && mine.offset.y != -.1f)
            {
                mine.offset = new Vector2(mine.offset.x, (BoxSizes[1][1] - (BoxSizes[0][1] - 0.1f )) / 2);
                mine.size = BoxSizes[1];

                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", true);
                //print("Crouching!!");
               
            }
            else if (!duck && !crawl && mine.offset.y != .17f)
            {
                mine.size = BoxSizes[0];
                mine.offset = new Vector2(mine.offset.x, .17f);
                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", false);
                print("Not Crouching!!");
            }
            //Condition for the crawling
        }
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Vaultable") {
            canVault = true;
            VaultPos = col.gameObject.GetComponent<Vaultable>().VaultEndpoint;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Vaultable")
        {
            canVault = false;
        }
    }


    void ToggleDown() {

        if (duck == false && crawl == false)
        {
            duck = true;
        }else if(duck = true && crawl == false)
        {
            crawl = true;
            duck = false;
        }

    }

    void ToggleUp()
    {

        if (duck == false && crawl == true)
        {
            duck = true;
            crawl = false;
        }
        else if (duck = true && crawl == false)
        {
            crawl = false;
            duck = false;
        }

    }


}
