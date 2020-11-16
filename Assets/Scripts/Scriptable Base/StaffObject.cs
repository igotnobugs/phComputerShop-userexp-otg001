using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Staff Data")]
public class StaffObject : ScriptableObject
{
    public string fullname;
    public string nickname;
    public string initials;

    public Sprite portrait;
}
