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

    public Vector2 jump_vector = new Vector2(0, 2.0f);

    //Vettore per il movimento
    private Vector2 movement;
    //Ci prendiamo il component del player (in questo caso il rigidbody)
    private Rigidbody2D rigidbodyComponent;

    //variabile per il ritardo della distruzione dell'oggetto
    public float delay;

    //variabile riservata alla sezione animator di Unity
    public Animator animator;
    //bool striscia;
    //variabili riservate al proiettile
    public GameObject playerbullet;
    public GameObject proiettile;

    //-----------------------------
    public bool IsGrounded;
    public Transform grounder;
    public float radiuss;
    public LayerMask ground;
    //-------------------

    //bound per tenere il player nella scena
    public float bottomBound = -9.8F;
    public float upperBound = 10.4F;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(grounder.transform.position, radiuss, ground);

        if(transform.position.x <= bottomBound)
        {
            transform.position = new Vector3(bottomBound, transform.position.y, transform.position.z);
        }
        else if(transform.position.x >= upperBound)
        {
            transform.position = new Vector3(upperBound, transform.position.y, transform.position.z);
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

        //dichiaro funzioni
        Jump();
        Run();
        Striscia();

        //spara proiettile alla pressione della spacebar
        if (Input.GetKeyDown("space"))
        {
            //crea cloni proiettile al comando "spacebar"
            GameObject proiettile1 = (GameObject)Instantiate(playerbullet);
            //set posizione iniziale gameobject
            proiettile1.transform.position = proiettile.transform.position;
        }

        //Salviamo la reference del component nella variabile dichiarata sopra
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }

        //Diamo effettivamente movimento al player
        rigidbodyComponent.velocity = movement;


    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.W) && (IsGrounded == true) && animator.GetBool("striscia") == false)
        {
            //animator.SetBool("jump", true);
            rigidbodyComponent.AddForce(jump_vector);

            if (Input.GetKey(KeyCode.D))
            {
                Vector2 movement = new Vector2(0.5f, 0.5f);
                movement.y = rigidbodyComponent.velocity.y;
                rigidbodyComponent.velocity = movement;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector2 movement = new Vector2(-2.0f, 0.5f);
                movement.y = rigidbodyComponent.velocity.y;
                rigidbodyComponent.velocity = movement;
            }
        }
    }

    void Run()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("StartRunning", true);
            if (Input.GetKey(KeyCode.D))
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = 0;
                transform.rotation = Quaternion.Euler(rotationVector);
            }

            if (Input.GetKey(KeyCode.A))
            {
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y =180;
                transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            animator.SetBool("StartRunning", false);
        }
    }

    void Striscia()
    {
        if (Input.GetButtonDown("Vertical"))
            animator.SetBool("striscia", true);

        if (Input.GetButtonUp("Vertical"))
            animator.SetBool("striscia", false);
    }

    //metodo per gestire la transizione dei collider
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
    }
}








