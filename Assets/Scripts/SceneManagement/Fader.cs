using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{


    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;


        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {

            canvasGroup.alpha = 0;
        }

        public void FadeOutImmediate()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1;
            }
        }


        public IEnumerator FadeOut(float time)
        {
            return Fade(1f, time);
        }

        public IEnumerator Fade (float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            yield return currentActiveFade;
        }


        public IEnumerator FadeIn(float time)
        {
            return Fade(0f, time);
        }

        private IEnumerator FadeRoutine(float target,  float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target,  Time.deltaTime / time);
                yield return null; //wait one frame 
            }
        }
    }

}
