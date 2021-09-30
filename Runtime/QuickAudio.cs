using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kazegames
{
    [AddComponentMenu(Globals.MENU + "Quick Audio")]
    public class QuickAudio : MonoBehaviour
    {
        public void PlayOneShot(AudioClip clip)
        {
            AudioSource src = GetComponent<AudioSource>();
            
            if(src == null)
            {
                src = gameObject.AddComponent<AudioSource>();
                src.volume = 1;
                src.pitch = 1;
                src.panStereo = 0;
            }

            src.clip = clip;
            src.loop = false;

            src.Play();
        }

        public void LoopPlay(AudioClip clip)
        {
            AudioSource src = GetComponent<AudioSource>();

            if (src == null)
            {
                src = gameObject.AddComponent<AudioSource>();
                src.volume = 1;
                src.pitch = 1;
                src.panStereo = 0;
            }

            src.clip = clip;
            src.loop = true;

            src.Play();
        }
    }
}
