using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
[System.Serializable]
public class Dialogue {
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
