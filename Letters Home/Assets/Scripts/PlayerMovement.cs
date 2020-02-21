using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_dead;
    Rigidbody me;
    public float MoveSpeed = 1.0f;
    Vector3 MoveVector = new Vector3();
    public bool attached;
    public bool crouch;
    public bool crawl;
    public bool Up;
    public bool isVaulting;
    public float totalVaultTime;
    float vaulttimer;
    public List<Vector2> CapsuleSizes;
    public CapsuleCollider mine;
    Animator Anim;

    private bool canVault;
    private Transform VaultPos;
    private float timerS;
    private float timerW;
    private float doubleTapTimer = 0.35f;
    public static int Lane = 0;
    public static bool CanChangeLanes = true;

    private bool swapped = false;

    [HideInInspector]
    public float climbSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        mine = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attached)
        {
            crawl = false;
            crouch = false;
            transform.Translate(new Vector3(0,Input.GetAxis("Vertical"), 0) * Time.deltaTime * climbSpeed);
        }
        
        if (!m_dead && !UI_InvFinder.me.Dialogue)
        {
            MoveVector = Vector3.zero;

            MoveVector += new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, 0, Input.GetAxis("Vertical")*MoveSpeed);

            me.transform.position += new Vector3(MoveVector.x, MoveVector.y, MoveVector.z) * Time.deltaTime;



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
            /*if (CanChangeLanes && !attached && (Input.GetButtonDown("DownLevel")))
            {
                if (Lane == 1)
                {
                    this.transform.position = new Vector2(transform.position.x, transform.position.y - 0.55f);
                }
                Lane = 0;
                
            }
           

            if(CanChangeLanes && !attached && (Input.GetButtonDown("UpLevel")))
            {
                if (Lane == 0)
                {
                    this.transform.position = new Vector2(transform.position.x, transform.position.y + 0.55f);
                }
                Lane = 1;
                
            }*/

            if (Input.GetButtonDown("Crawl"))
            {
                timerS = Time.time;
                ToggleCrawl();
            }
            else if(Input.GetButtonDown("Crouch"))
            {
                timerW = Time.time;
                ToggleCrouch();
            }
            

            if (canVault && (Input.GetButtonDown("Vault")))
            {
                if(transform.localScale.x == 1)
                    transform.position = VaultPos.position;
                else
                    transform.position = new Vector3(VaultPos.position.x - 5, VaultPos.position.y, VaultPos.position.z);
                canVault = false;
            }

            

            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                Anim.SetBool("Walkin", true);
            }
            else if ((Input.GetAxis("Horizontal") > 0) || Input.GetAxis("Vertical") < 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                Anim.SetBool("Walkin", true);
            }
            else
                Anim.SetBool("Walkin", false);


            if(crawl && mine.center.y != CapsuleSizes[2][0])
            {
                mine.center = new Vector3(0, CapsuleSizes[2][0], 0);
                mine.height = CapsuleSizes[2][1];
                mine.direction = 0;


                Anim.SetBool("Crawlin", true);
                Anim.SetBool("Crouch", false);
            }
            else if (crouch && mine.center.y != CapsuleSizes[1][0])
            {
                mine.center = new Vector3(0, CapsuleSizes[1][0], 0);
                mine.height = CapsuleSizes[1][1];
                mine.direction = 1;
                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", true);
                //print("Crouching!!");
               
            }
            else if (!crouch && !crawl && mine.center.y != CapsuleSizes[0][0])
            {
                mine.height = CapsuleSizes[0][1];
                mine.center = new Vector3(0, CapsuleSizes[0][0], 0);
                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", false);
                print("Not Crouching!!");
                mine.direction = 1;
            }
            //Condition for the crawling
        }
        else
        {
            if (m_dead == true)
            {
                Anim.SetBool("Crawlin", true);
            }
            else
            {
                Anim.SetBool("Crawlin", false);
            }
            Anim.SetBool("Walkin", false);
            Anim.SetBool("Crouch", false);
        }
    }

    public void SetSpeed(int nes)
    {
        MoveSpeed = nes;   
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Vaultable") {
            canVault = true;
            print("found1");
            VaultPos = col.gameObject.GetComponent<Vaultable>().VaultEndpoint;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Vaultable")
        {
            print("found1");
            canVault = false;
        }
    }


    void ToggleCrawl() {
        crawl = !crawl;
        crouch = false;

    }

    void ToggleCrouch()
    {
        crouch = !crouch;
        crawl = false;
    }


}
