using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.playMusic(SoundManager.gameMusic1);
    }
}
