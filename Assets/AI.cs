using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public float fitness; 
    public DNA dna;
    
    public float energySpent;
    public float collections;

    private int currentIteration;
    public static int maxIterations = 1000;
    private static float secondsPerIteration = 0.5f;

    public Rigidbody2D rb;

    public bool computing = true;

    public Transform spawn;

    public GameObject point;

    private Vector2 lastPosition;
    private Vector2 lastVelocity;
    private float lastAngularVelocity;

    private List<GameObject> collectedPoints;

	// Use this for initialization
	void Start () {
        //Time.timeScale = 5f;
        currentIteration = 0;
        energySpent = 0;
        collections = 0;

        collectedPoints = new List<GameObject>();

        //rb.MovePosition(new Vector3(-43, 14, 0));


        StartCoroutine(Simulate());
    }

    // Update is called once per frame
    void Update()
    {
        
	}

    void FixedUpdate()
    {
        lastPosition = transform.position;
        lastVelocity = GetComponent<Rigidbody2D>().velocity;
        lastAngularVelocity = GetComponent<Rigidbody2D>().angularVelocity;
    }

    IEnumerator Simulate()
    {
        while (currentIteration < maxIterations)
        {
            float x = dna.forceVectors[currentIteration].x;
            float y = dna.forceVectors[currentIteration].y;

            if (x != 0 || y != 0)
                energySpent += Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) / 50;

            //Debug.Log(x + ", " + y);
            rb.AddForce(new Vector2(x, y));

            yield return new WaitForSeconds(secondsPerIteration);

            rb.velocity = new Vector2(0, 0);

            currentIteration++;
            //Debug.Log(currentIteration);
        }

        computing = false;
        fitness = 5 * collections - energySpent;
    }

    public void InitAI(DNA _dna)
    {
        dna = _dna;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "point" && collectedPoints.Contains(collision.gameObject))
        {
            //Debug.Log("detected");
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            transform.position = lastPosition;
            GetComponent<Rigidbody2D>().velocity = lastVelocity;
            GetComponent<Rigidbody2D>().angularVelocity = lastAngularVelocity;
        }
        else if (collision.gameObject.tag == "point")
        {
            collectedPoints.Add(collision.gameObject);
            collections++;
            Debug.Log("Points: " + collections);

            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            transform.position = lastPosition;
            GetComponent<Rigidbody2D>().velocity = lastVelocity;
            GetComponent<Rigidbody2D>().angularVelocity = lastAngularVelocity;
        }

    }
}
