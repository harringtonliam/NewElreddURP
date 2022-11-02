using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.Stats
{

    public class LevelDisplay : MonoBehaviour
    {

        BaseStats baseStats;

        void Awake()
        {
            baseStats = PlayerSelector.GetFirstSelectedPlayer().GetComponent<BaseStats>();
        }

        private void Update()
        {
            GetComponent<Text>().text = baseStats.GetLevel().ToString();
        }
    }

}
