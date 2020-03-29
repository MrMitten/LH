using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaultable : MonoBehaviour
{
    public Transform vaultLeft;
    public Transform vaultRight;
    public float vaultSpeed = 0f;

    public string type;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.tag = "Vaultable";
    }
}
