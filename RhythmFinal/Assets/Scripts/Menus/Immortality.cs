using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Object.DontDestryOnLoad script

// This script will preserve an object across scene loads, making it "immortal"
// Attach this script to any GameObject you want to persist between scenes
// Note: Only one instance of this script should exist in the scene to avoid duplicates
// All the children of the GameObject with this script will also be preserved across scenes
// Tag the GameObject with "immortality" to ensure it is recognized as the immortal object

public class Immortality : MonoBehaviour
{
   void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("immortality");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
