using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foxes : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    public Vector3 Destination;
    public Vector3 Origin;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Origin;

        if (Destination.x - Origin.x > 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1); //make it turn right if he goes to his right
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += ((Destination - Origin).normalized) * speed * Time.deltaTime;
        Debug.Log((Destination - Origin).normalized);
        if((this.transform.position - Destination).magnitude < 0.2)
        {
            Debug.Log("oui");
            Vector3 temp = Destination;
            Destination = Origin;
            Origin = temp;

            if(Destination.x - Origin.x > 0)
            {
                Debug.Log("go right");
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                Debug.Log("Go left");
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
