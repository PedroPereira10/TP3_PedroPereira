using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMaker : MonoBehaviour
{
    public void ResetAttack()
    {
        GetComponentInParent<Player>().ResetAttack();
    }
}

