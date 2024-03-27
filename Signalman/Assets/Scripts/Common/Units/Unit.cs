using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected UnitType _type;
    protected string _name;
    protected float _maxHealth;
    protected float _maxJumpHeight;
    protected float _speed;
    protected float _rotateSpeed;
    protected float _gravityForce;

    public virtual void Init(UnitData unitData)
    {
        _type = unitData.Type;
        _name = unitData.Name;
        _maxHealth = unitData.MaxHealth;
        _maxJumpHeight = unitData.MaxJumpHeight;
        _speed = unitData.Speed;
        _rotateSpeed = unitData.RotateSpeed;

        Debug.Log($"{_name} инициализирован");
    }
}
