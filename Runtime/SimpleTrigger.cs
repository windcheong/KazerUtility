using UnityEngine;
using UnityEngine.Events;

namespace Kazegames
{
    [AddComponentMenu(Globals.MENU + "Simple Trigger")]
    public class SimpleTrigger : MonoBehaviour
    {
        [SerializeField] string Tag = "";

        [SerializeField] UnityEvent onEnter;
        [SerializeField] UnityEvent onExit;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(string.IsNullOrEmpty(Tag))
            {
                onEnter?.Invoke();
            }
            else
            {
                if(other.CompareTag(Tag))
                {
                    onEnter?.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (string.IsNullOrEmpty(Tag))
            {
                onExit?.Invoke();
            }
            else
            {
                if (other.CompareTag(Tag))
                {
                    onExit?.Invoke();
                }
            }
        }

    }
}
