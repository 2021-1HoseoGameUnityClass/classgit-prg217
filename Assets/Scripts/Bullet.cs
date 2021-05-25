using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float direction = 1f;
  
    // Update is called once per frame
    void Update()
    {
        float speed = moveSpeed * Time.deltaTime * direction;
        Vector3 vector = new Vector3(speed, 0, 0);
        transform.Translate(vector);
    }

    public void InstantiateBullet(float _direction)
    {
        direction = _direction;
        Vector3 vector = new Vector3(_direction, 1, 1);
        transform.localScale = vector;
        Destroy(gameObject, 2f);
    }
}
