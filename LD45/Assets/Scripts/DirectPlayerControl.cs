using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectPlayerControl : MonoBehaviour
{
    Movement move;
    private void Start()
    {
        move = GetComponent<Movement>();

    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        move.input = input;
    }
}
