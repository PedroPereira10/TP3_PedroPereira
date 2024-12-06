using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Display : MonoBehaviour
{
    [SerializeField] private Image _skillCoolDown;
    [SerializeField] private FireBall_Skill _fireballSkill;

    void Update()
    {
        _skillCoolDown.fillAmount = _fireballSkill.GetCoolDownRatio();
    }
}
