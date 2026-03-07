using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUI : MonoBehaviour
{

    [SerializeField] Option gridChangeOption;


    private void Awake()
    {
        gridChangeOption.Initalize(SpawnGrid);
    }

    void SpawnGrid(int Index)
    {
        DemoGridManager.SpawnGridWithIndex(Index);
    }


}
