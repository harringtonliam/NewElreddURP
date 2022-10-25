using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI.Menus;

namespace RPG.UI.Menus
{
    public interface ISavedGameUI
    {
        public void Setup(string savedGameName, string savedGameTime);
    }
}


