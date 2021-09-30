using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kazegames
{
    [AddComponentMenu(Globals.MENU + "Simple Collision")]
    public class SimpleCollision : MonoBehaviour
    {
        [SerializeField] string Tag = "";

        [SerializeField] UnityEvent onEnter;
        [SerializeField] UnityEvent onExit;

        private void OnCollisionEnter(Collision collision)
        {
            if(string.IsNullOrEmpty(Tag))
            {
                onEnter?.Invoke();
            }
            else
            {
                if(collision.collider.CompareTag(Tag))
                {
                    onEnter?.Invoke();
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (string.IsNullOrEmpty(Tag))
            {
                onExit?.Invoke();
            }
            else
            {
                if (collision.collider.CompareTag(Tag))
                {
                    onExit?.Invoke();
                }
            }
        }

    }
}
