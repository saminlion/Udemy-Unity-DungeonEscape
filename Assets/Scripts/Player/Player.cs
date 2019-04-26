using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    //Variable for amount of diamonds
    public int _amountOfDiamonds;

    //Get Handle to Rigidbody
    private Rigidbody2D _rigid;

    //Variable for Jump Force
    public float _jumpforce = 5.0f;

    //Variable for Player Speed
    [SerializeField]
    private float _speed = 5.0f;

    private int health = 4;

    private bool _resetJump;

    //Handle to Player Animation
    private PlayerAnimation _playerAnim;

    //Handle to Player SpriteRenderer
    private SpriteRenderer _playerSprite;

    //Handle to Player SpriteRenderer
    private SpriteRenderer _swordArcSprite;

    private bool _grounded = false;

    private bool isDead;

    public bool hasFlameSword;

    public int Health { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        //Assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();

        //Assign Handle to Player Animation
        _playerAnim = GetComponent<PlayerAnimation>();

        _playerSprite = GetComponentInChildren<SpriteRenderer>();

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = health;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        Attack();
    }

    public void Damage(int damage)
    {
        //check for dead
        //if false
        if (isDead)
            return;

        _playerAnim.Hit();

        //remove 1 Health
        Health -= damage;

        //play Death Animation
        if (Health < 1)
        {
            _playerAnim.Death();
            isDead = true;
        }
        //else update ui display
        else
        {
            UIManager.Instance.UpdateLife(Health);
        }
    }

    void Movement()
    {
        //Horizontal Input for Left/Right

        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        _grounded = IsGrounded();

        Flip(move);

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        //current velocity = new velocity (x, current velocity.y)
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        //Play Run Animation
        _playerAnim.Move(move);       
    }

    void Attack()
    {
        if ((Input.GetMouseButtonDown(0) || CrossPlatformInputManager.GetButtonDown("A_Button")) && IsGrounded())
        {
            if (hasFlameSword)
                _playerAnim.SpecialAttack();
            else
            _playerAnim.Attack();
        }
    }

    void Flip(float move)
    {
        if (move > 0)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);

        if (hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;

        yield return new WaitForSeconds(0.1f);

        _resetJump = false;
    }

    public void AddGem(int amount)
    {
        _amountOfDiamonds += amount;
        UIManager.Instance.UpdateGemCount(_amountOfDiamonds);
    }
}
