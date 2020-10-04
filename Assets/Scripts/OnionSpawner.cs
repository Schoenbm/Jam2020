using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionSpawner : MonoBehaviour
{
    public GameObject onionPrefab;
    [SerializeField] float onionRespawnCd;

    private float actualCd;
    private bool alive;
    private GameObject onion;
    // Start is called before the first frame update
    void Start()
    {
        onion = Instantiate(onionPrefab, this.transform);
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onion == null && alive)
        {
            alive = false;
            actualCd = onionRespawnCd;
        }
        else if(!alive)
        {
            actualCd -= Time.deltaTime;
        }

        if(!alive && actualCd < 0)
        {
            alive = true;
            onion = Instantiate(onionPrefab, this.transform);
        }
    }
}
