using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public float lanesDone;
    public static BossManager instance;
    public AudioSource bossMusic;

    public void Start()
    {
        instance = this;
        lanesDone = 0;
    }

    public void Update()
    {
    }
}

