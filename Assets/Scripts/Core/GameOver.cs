using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace RPG.Core
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] UnityEvent gameOverActions;

        public void GameOverActions()
        {
            gameOverActions.Invoke();
        }

    }

}


