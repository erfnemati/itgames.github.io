using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opengate : MonoBehaviour
{
    [SerializeField] GameObject openinggate;
   
    // Start is called before the first frame update
    void Start()
    {
        openinggate.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
