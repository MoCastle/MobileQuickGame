using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpeed : MonoBehaviour {

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * 3;
    }
}
