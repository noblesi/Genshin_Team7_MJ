using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howl : BossSkill
{
    private float howl_Atk;
    private SphereCollider sphereColl;

    private void OnEnable()
    {
        if(sphereColl == null)
            sphereColl = GetComponent<SphereCollider>();
    }

    public override void SetAtk(float atk)
    {
        howl_Atk = GetSkillDamage(Skill.Howl) * atk;
    }

    public override IEnumerator DelayDamage()
    {
        sphereColl.enabled = true;
        yield return new WaitForSeconds(4.0f);
        sphereColl.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Hit = true;
            Character player = other.gameObject.GetComponentInChildren<Character>();
            StartCoroutine(DotDamage(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Hit = false;
        }
    }

    private IEnumerator DotDamage(Character player)
    {
        while (Hit)
        {
            player.TakeDamage(howl_Atk);
            yield return new WaitForSeconds(0.5f);

        }
    }
    
}
