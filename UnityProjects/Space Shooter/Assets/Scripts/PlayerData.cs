using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string UseID { get; set; }

    private void Start()
    {
        int count = FindObjectsOfType<PlayerData>().Length;
        if(count > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
