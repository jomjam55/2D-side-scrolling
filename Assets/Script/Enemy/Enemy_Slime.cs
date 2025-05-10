using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum slimeType {big,medium,small}
public class Enemy_Slime : Enemy
{
    [Header("Slime spesific")]
    [SerializeField] private slimeType slimeType;
    [SerializeField] private int slimesToCreate;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreationVelocity;
    [SerializeField] private Vector2 maxCreationVelocity;


    #region States

    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeDeadState deadState { get; private set; }

  


    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
        battleState = new SlimeBattleState(this, stateMachine, "Move", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);
        deadState = new SlimeDeadState(this, stateMachine, "Idle", this);
        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);

        if (slimeType == slimeType.small)
            return;

        CreateSlimes(slimesToCreate, slimePrefab);
    }

    private void CreateSlimes(int _amountofSlime , GameObject _slimePrefab)
    {
        for(int i = 0; i < _amountofSlime; i++)
        {
            GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    public void SetupSlime(int _facingDir)
    {
        if(_facingDir != facingDir)
        {
            Flip();
        }

        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
        float yVelocity = Random.Range(minCreationVelocity.y, minCreationVelocity.y);

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * facingDir, yVelocity);

        
    }

   
}
