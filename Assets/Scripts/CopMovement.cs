﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CopMovement : MonoBehaviour
//{


//    public float speed = .1f;
//    private bool move = true;
//    private float interval = 1f;
//    // Use this for initialization
//    void Start()
//    {

//        InvokeRepeating("ToggleMove", 0.0f, interval);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (move)
//        {
//            transform.Translate(Vector2.down * speed);
//        }

//    }

//    void ToggleMove()
//    {
//        move = !move;
//        interval = Random.Range(2.0f, 6.0f);
//    }

//    void OnBecameInvisible()
//    {
//        Destroy(gameObject);
//    }
//}
public class CopMovement : MonoBehaviour
{


    [SerializeField]
    private GameObject player;
    private Rigidbody2D body;
    public Vector2 velocity = Vector2.zero;
    public float speed = .25f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 toTarget = player.transform.position - transform.position;


        transform.Translate(toTarget * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}