using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
/* Modified to be scaleable and extendable
 */

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject {
    public Script[] scripts;
}

[System.Serializable]
public class Script {
    public string name;

    [TextArea(3, 10)]
    public string sentence;
}
