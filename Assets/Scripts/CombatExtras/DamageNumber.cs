using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rigidbody;
    public TextMesh text;

    [SerializeField] float minXVel;
    [SerializeField] float maxXVel;
    [SerializeField] float minYVel;
    [SerializeField] float maxYVel;

    void Start()
    {
        Vector2 velocity = new Vector2(Random.Range(minXVel, maxXVel), Random.Range(minYVel, maxYVel));
        rigidbody.velocity = velocity;
    }
}
