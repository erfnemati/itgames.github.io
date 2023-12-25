using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScreenManager : MonoBehaviour
{

    public delegate void TheEnd();
    public static event TheEnd EndLevel;
    // Start is called before the first frame update
    private void Start()
    {
        EndLevel();
    }


}
