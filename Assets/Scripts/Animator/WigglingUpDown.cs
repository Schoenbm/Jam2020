using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigglingUpDown : MonoBehaviour
{
    public int speed;
    public int width;

    private float yPos;
    public Rigidbody2D rb;

    private void Start()
    {
        yPos = this.transform.position.y;
        Up();
    }

    void Up()
    {
        rb.AddForce(new Vector2(0,speed));
        while (this.transform.position.y <= width + yPos)
        { }

    }

    void Down()
    {
        rb.AddForce(new Vector2(0, -speed));
        if (this.transform.position.y >= width + yPos)
            Down();
        else
            Up();
    }
}
