using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;

public class QuickSaveButtonUI : MonoBehaviour
{
    SavingWrapper savingWrapper;

    private void Start()
    {
        savingWrapper = FindObjectOfType<SavingWrapper>();
    }


    public void ButtonClicked()
    {
        if (savingWrapper != null)
        {
            savingWrapper.QuickSave();
        }
    }
}
