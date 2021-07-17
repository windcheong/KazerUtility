using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Kazegames.Utility
{
    [AddComponentMenu(Globals.MENU + "Delay Action")]
    public class DelayAction : MonoBehaviour
    {
        [SerializeField] float delayTime;

        [SerializeField] UnityEvent onAct;

        public void StartTask()
        {
            StartTask(delayTime);
        }

        public void StartTask(float delayTime)
        {
            StartCoroutine(Delay(delayTime));
        }

        IEnumerator Delay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            onAct?.Invoke();
        }
    }
}
