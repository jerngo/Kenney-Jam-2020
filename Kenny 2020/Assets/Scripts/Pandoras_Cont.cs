using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pandoras_Cont : MonoBehaviour
{
    [SerializeField]
    float treasure = 1000;
    public GameObject LightBlock;
    public Material green;
    public Material red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public float checkTreasure() {
        return treasure;
    }

    public void takeTreasure(float value) {
        treasure -= value;
        if (treasure <= 0)
        {
            changeColor(red);
        }
        else
        {
            changeColor(green);
        }
    }

    void changeColor(Material mats) {
        LightBlock.GetComponent<MeshRenderer>().material = mats;
    }
}
