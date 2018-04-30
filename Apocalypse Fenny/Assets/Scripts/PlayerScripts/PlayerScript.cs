using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    //Inizializzo il vettore velocità
    //Variabile pubblica per poter cambiare la velocità dall'editor Unity
    public Vector2 speed = new Vector2(2, 2);
    public Vector2 jump_vector;

    //Vettore per il movimento
    private Vector2 movement;

    //Ci prendiamo il component del player (in questo caso il rigidbody)
    private Rigidbody2D rigidbodyComponent;

    //variabile per il ritardo della distruzione dell'oggetto
    public float delay;

    //variabile riservata alla sezione animator di Unity
    Animator animator;


    //variabili riservate al proiettile
    public GameObject playerbullet;
    public GameObject playerbullet2;
    public GameObject playerbullet3;
    public GameObject proiettile;
    public GameObject proiettile2;
    //Indica se la direzone in cui sparare i proiettili è verso destra o sinistra
    public static bool direction;
    //Indica quale arma ha il player
    public static int weap_;
    //Indica quando il player passa sopra ad un'arma o comunque la sta tenendo
    public static bool hover;
    public static int munizioni;

    //-----------------------------
    //True se tocca il suolo, false se non lo tocca
    public bool IsGrounded;
    public Transform grounder;
    public float radiuss;
    public LayerMask ground;
    public LayerMask rubble;
    //-------------------

    //Bound per tenere il player nella scena
    public float bottomBound = -9.8F;
    public float upperBound = 10.4F;

    //Riferimento alla vita del player
    private PlayerHealth playerHealth;

    void Start()
    {
        direction = true;		//indice direzione proiettile default
        hover = false;
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        munizioni = 0;

        //Salviamo la reference del component nella variabile dichiarata sopra
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(grounder.transform.position, radiuss, ground);

        //Se il player si trova attaccato alla parte sinistra della camera o alla destra subisce un danno
        if (transform.position.x <= bottomBound)
        {
            transform.position = new Vector3(bottomBound, transform.position.y, transform.position.z);
            playerHealth.TakeDamage(0.5F);
        }
        else if (transform.position.x >= upperBound)
        {
            transform.position = new Vector3(upperBound, transform.position.y, transform.position.z);
            playerHealth.TakeDamage(0.5F);
        }
        //Se il player è morto visualizziamo l'animazione.
        if (playerHealth.currentHealth <= 0)
        {
            animator.SetBool("Dead", true);
            IsGrounded = false;
        }
    }

    void FixedUpdate()
    {
        //Prendiamo le informazioni riguardanti gli spostamenti, in questo caso solo sull'asse X
        float inputX = Input.GetAxis("Horizontal");

        //Salviamo il movimento del player
        movement = new Vector2(
            speed.x * inputX,
            speed.y * 0);

        //Diamo effettivamente movimento al player
        rigidbodyComponent.velocity = movement;

        //Dichiaro funzioni
        Jump();
        Run();
        Striscia();

        //Spara proiettile alla pressione della spacebar, se non sta strisciando, se non è morto e se ha munizioni
        if (Input.GetKeyDown("space") && !animator.GetBool("striscia") && !animator.GetBool("Dead") && munizioni > 0)
        {
            //Se l'arma che ha è la pistola
            if (weap_ == 1)
            {
                munizioni--;
                //Se la direzione dello sparo è verso destra
                if (direction == true)
                {
                    //crea cloni proiettile al comando "spacebar"
                    GameObject proiettile1 = (GameObject)Instantiate(playerbullet);
                    //set posizione iniziale gameobject
                    proiettile1.transform.position = proiettile.transform.position;

                }
                //Se verso sinistra
                else if (direction == false)
                {
                    GameObject proiettile_2 = (GameObject)Instantiate(playerbullet);
                    proiettile_2.transform.position = proiettile2.transform.position;
                }
            }
            //Se l'arma è il mitra
            else if (weap_ == 2)
            {
                munizioni--;
                if (direction == true)
                {
                    //crea cloni proiettile al comando "spacebar"
                    GameObject proiettile1 = (GameObject)Instantiate(playerbullet2);
                    //set posizione iniziale gameobject
                    proiettile1.transform.position = proiettile.transform.position;

                }
                else if (direction == false)
                {
                    GameObject proiettile_2 = (GameObject)Instantiate(playerbullet2);
                    proiettile_2.transform.position = proiettile2.transform.position;
                }

            }
            //Se l'arma è il bazooka
            else if (weap_ == 3)
            {
                munizioni--;
                if (direction == true)
                {
                    //crea cloni proiettile al comando "spacebar"
                    GameObject proiettile1 = (GameObject)Instantiate(playerbullet3);
                    //set posizione iniziale gameobject
                    proiettile1.transform.position = proiettile.transform.position;

                }
                else if (direction == false)
                {
                    GameObject proiettile_2 = (GameObject)Instantiate(playerbullet3);
                    proiettile_2.transform.position = proiettile2.transform.position;
                }
            }
        }
    }

    //Metodo per saltare
    void Jump()
    {
        //Se il player è a terra e non sta strisciando
        if (Input.GetKey(KeyCode.W) && (IsGrounded == true) && animator.GetBool("striscia") == false)
        {
            //Imprimiamo una forza al rigidbody per il salto
            rigidbodyComponent.AddForce(jump_vector);
            //Se mentre sta saltando viene premuto il tasto D o A il player si muove verso quella direzione
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 movement = new Vector2(2.0f, 2.0f);
                movement.y = rigidbodyComponent.velocity.y;
                rigidbodyComponent.velocity = movement;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector2 movement = new Vector2(2.0f, 2.0f);
                movement.y = rigidbodyComponent.velocity.y;
                rigidbodyComponent.velocity = movement;
            }
        }
    }

    //Metodo per correre
    void Run()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("StartRunning", true);
            if (Input.GetKey(KeyCode.D))
            {
                direction = true;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = 0;
                transform.rotation = Quaternion.Euler(rotationVector);
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction = false;
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = 180;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            animator.SetBool("StartRunning", false);
        }
    }

    //Metodo per strisciare
    void Striscia()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            animator.SetBool("striscia", true);
            SetColliderForSprite(1);
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetButtonUp("Vertical"))
        {
            animator.SetBool("striscia", false);
            SetColliderForSprite(0);
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    //Metodo per gestire la transizione dei collider
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(grounder.transform.position, radiuss);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            Destroy(collision.gameObject, delay);
        }

        if (collision.gameObject.tag == "Pistol")
        {
            Destroy(collision.gameObject);
            hover = true;
            weap_ = 1;
            munizioni = 10;
        }

        else if (collision.gameObject.tag == "mitra")
        {
            Destroy(collision.gameObject);
            hover = true;
            weap_ = 2;
            munizioni = 20;
        }

        else if (collision.gameObject.tag == "bazooka")
        {
            Destroy(collision.gameObject);
            hover = true;
            weap_ = 3;
            munizioni = 3;
        }
    }
}



