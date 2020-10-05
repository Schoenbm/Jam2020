using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foxes : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    public   Transform DestTransform;
    private  Vector3 Destination;
    private  Vector3 Origin;

    // Start is called before the first frame update
    void Start()
    {
        Destination = DestTransform.position;
        Origin = this.transform.position;

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
        if((this.transform.position - Destination).magnitude < 0.6)
        {
            Vector3 temp = Destination;
            Destination = Origin;
            Origin = temp;

            if(Destination.x - Origin.x > 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
