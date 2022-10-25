using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Attributes
{
    public class CharacterSheet : MonoBehaviour
    {
        [SerializeField] string characterName = "No Name";
        [SerializeField] string rank = "No Rank";
        [SerializeField] Sprite portrait = null;


        public string CharacterName { get { return characterName; } }
        public Sprite Portrait { get { return portrait; } }

        public string Rank { get { return rank; } }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


