using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class preanarstan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Foo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Foo()
    {
        StartCoroutine(TemporarilyDeactivate(11.0f));
    }

    private IEnumerator TemporarilyDeactivate(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("Anarestan");
        Debug.Log("endtimer");

    }



}
