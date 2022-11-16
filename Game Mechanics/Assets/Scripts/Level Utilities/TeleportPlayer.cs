using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(Collider2D))]
public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] Vector2 _destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var controller = collision.GetComponent<CharacterController2D>();
            controller.TeleportAndReset(_destination);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_destination, 0.1f);
    }
}
