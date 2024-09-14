using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndFollowPlayer : MonoBehaviour
{
    public Transform player;
    float followDistance = 10.0f;
    [SerializeField]protected float speed;
    

    // Update is called once per frame
    void Update()
    {
        Follow();
    }


    void Follow()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= followDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    
}
