using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTask", menuName = "Scriptable Object/Task")]
public class Task : ScriptableObject
{
    public string Description;
    public float TaskCount;
    public Sprite TaskSprite;

}
