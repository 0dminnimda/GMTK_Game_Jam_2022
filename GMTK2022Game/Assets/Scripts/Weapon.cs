using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected DamageLayer _damageLayer;

    [SerializeField]
    protected float _actionCooldown;

    protected float _nextActionTime = 0f;

    public abstract void Action();
}
