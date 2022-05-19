using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps powerUps; // singleton list keeping track of the upgrades that the player has collected

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // on awake, check to make sure this is the only instance of this class
    private void Awake()
    {
        if (powerUps != null && powerUps != this)
        {
            Destroy(this);
        }
        else
        {
            powerUps = this;
        }
    }
}
