using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{

    public class PlayerSelector : MonoBehaviour, IRaycastable
    {

        [SerializeField] Transform selectedVisual;

        private bool isSelected;

        public bool IsSelected { get { return isSelected; } }

        public static GameObject GetFirstSelectedPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                if (player.GetComponent<PlayerSelector>().IsSelected)
                {
                    return player;
                }
            }
            return players[0];
        }

        public static List<GameObject> GetAllSelectedPlayers()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            List<GameObject> selectedPlayers = new List<GameObject>();
            foreach (var player in players)
            {
                if (player.GetComponent<PlayerSelector>().IsSelected)
                {
                    selectedPlayers.Add(player);
                }
            }
            return selectedPlayers;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void SetSelected(bool selected)
        {
            isSelected = selected;
            if (selectedVisual != null)
            {
                selectedVisual.GetComponent<MeshRenderer>().enabled = selected;
            }

        }

        public CursorType GetCursorType()
        {
            return CursorType.SelectPlayer;
        }

        public RaycastableReturnValue HandleRaycast(PlayerSelector playerController)
        {

            return RaycastableReturnValue.FirstPlayerCharacter;
        }

        public void HandleActivation(PlayerSelector playerController)
        {
            //TODO.  Set follow cammera on this player
        }
    }
}


