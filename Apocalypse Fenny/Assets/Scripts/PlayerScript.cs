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

    public Vector2 jump_vector= new Vector2(0, 2.0f);

	//Vettore per il movimento
	private Vector2 movement;
	//Ci prendiamo il component del player (in questo caso il rigidbody)
	private Rigidbody2D rigidbodyComponent;

    //variabile per il ritardo della distruzione dell'oggetto
    public float delay;

	//variabile riservata alla sezione animator di Unity
	public Animator animator;
	bool striscia;
	//variabili riservate al proiettile
	public GameObject playerbullet;
	public GameObject proiettile;

    //-----------------------------
    public bool IsGrounded;
    public Transform grounder;
    public float radiuss;
    public LayerMask ground;
    //-------------------

	public Transform meteor;

	public int xMax = 6;
	public int xMin = -6;

	public int yMax = 16;
	public int yMin = 7;
	private int rand;// Le meteore inizieranno ad arrivare con un delay minimo dall'inizio del gioco compreso tra 100 e 500 frames
	private int n_meteor;// Saranno generate massimo 3 meteore alla volta

	void Start()
	{
		animator = GetComponent<Animator>();
		rand = Random.Range(100, 500);
		n_meteor = Random.Range(0, 3);
	}


	void Update()
	{
        IsGrounded = Physics2D.OverlapCircle(grounder.transform.position, radiuss, ground);	
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

        if (rand < 0)
        {
            rand = Random.Range(100, 500);
        }

        //Salviamo la reference del component nella variabile dichiarata sopra
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }

        //Diamo effettivamente movimento al player
        rigidbodyComponent.velocity = movement;

        for (int i = 0; i < n_meteor; i++)
		{
			if (rand == 0)
			{
				Instantiate(meteor, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
			}
		}

		rand--;
	}

	//funzione che permette di saltare 
	void Jump()
	{
        if (Input.GetButtonDown("Jump") && (IsGrounded == true) && animator.GetBool("striscia") == false)
        {
            //animator.SetBool("jump", true);
            rigidbodyComponent.AddForce(jump_vector);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbodyComponent.AddForce(new Vector2(2.0f,0));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbodyComponent.AddForce(new Vector2(-2.0f, 0));
            }          
        }
		//conclude il ciclo di transizione al rilascio della stessa
		if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("jump", false);
        }
    }

	//funzione che permette di correre
	void Run()
	{
		if (Input.GetButtonDown("Horizontal"))
			animator.SetBool("StartRunning", true);

		if (Input.GetButtonUp("Horizontal"))
			animator.SetBool("StartRunning", false);
	}

	void Striscia()//toggle per attivare e disattivare la modalità strisciare
	{
        if (Input.GetButtonDown("Vertical"))
        {
            animator.SetBool("striscia", true);
        }

        if (Input.GetButtonUp("Vertical"))
        {
            animator.SetBool("striscia", false);
        }
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
        if(collision.gameObject.tag == "Meteor")
        {
            Destroy(collision.gameObject,delay);
        }
    }
}








