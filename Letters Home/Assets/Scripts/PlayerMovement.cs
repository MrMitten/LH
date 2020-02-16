using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_dead;
    Rigidbody2D me;
    public float MoveSpeed = 1.0f;
    Vector2 MoveVector = new Vector2();
    public bool attached;
    public bool crouch;
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
    public static int Lane = 0;
    public static bool CanChangeLanes = true;
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
        if (attached)
        {
            crawl = false;
            crouch = false;
            transform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime * 10);
        }
        print(Lane);
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
            if (CanChangeLanes && crouch == false && crawl == false && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
            {
                Lane = 0;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                timerS = Time.time;
                ToggleDown();
            }

            if(CanChangeLanes && crouch == false && crawl == false && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                Lane = 1;
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                timerW = Time.time;
                ToggleUp();
            }
            

            if (canVault && Input.GetKey(KeyCode.F))
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
            else if (crouch && mine.offset.y != -.1f)
            {
                mine.offset = new Vector2(mine.offset.x, (BoxSizes[1][1] - (BoxSizes[0][1] - 0.1f )) / 2);
                mine.size = BoxSizes[1];

                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", true);
                //print("Crouching!!");
               
            }
            else if (!crouch && !crawl && mine.offset.y != .17f)
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

    public void SetSpeed(int nes)
    {
        MoveSpeed = nes;   
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
        if (crouch == false && crawl == false)
        {
            crouch = true;
        }else if(crouch = true && crawl == false)
        {
            crawl = true;
            crouch = false;
        }

    }

    void ToggleUp()
    {

        if (crouch == false && crawl == true)
        {
            crouch = true;
            crawl = false;
        }
        else if (crouch = true && crawl == false)
        {
            crawl = false;
            crouch = false;
        }

    }


}
