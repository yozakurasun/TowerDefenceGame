using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private int _charaPower;
    [SerializeField] private float _charaSpeed;
    [SerializeField] private float _attackRate;
    private Rigidbody2D _rb;
    private Animator _anim;
    private HitPoint _myHitPoint;
    private bool _isRed;
    private bool _isMove = true;
    private bool _isAttack = false;
    public GameObject HitBox;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _myHitPoint = gameObject.GetComponentInChildren<HitPoint>();
        if (gameObject.CompareTag("RedTeam"))
        {
            _isRed = true;
        }
        else
        {
            _isRed = false; 
        }
        _anim.SetTrigger("IsMove");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(HitBox == null)
        {
            _anim.SetTrigger("IsDie");
            _isMove = false;
            Invoke(nameof(DestoroyObject), 1.0f);
        }
    }

    private void Move()
    {
        if (_isRed && _isMove) 
        {
            _rb.velocity = new Vector2(-_charaSpeed, _rb.velocity.y);
        }
        else if (!_isRed && _isMove)
        {
            _rb.velocity = new Vector2(_charaSpeed, _rb.velocity.y);
        }

        if (!_isMove)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    IEnumerator AttackAction(HitPoint hitPoint)
    {
        while(hitPoint.IsDie == false && _myHitPoint.Hp > 0)
        {
            if(_isAttack) break;
            _isAttack = true;
            yield return new WaitForSeconds(_attackRate);
            if (hitPoint == null || _myHitPoint == null) break;
            hitPoint.Damage(_charaPower);
            _anim.SetTrigger("IsAttack");
            _isAttack = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isRed && collision.gameObject.CompareTag("BlueTeam") || !_isRed && collision.gameObject.CompareTag("RedTeam"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("HitBox"))
            {
                _isMove = false;      
            
                HitPoint hitPoint = collision.gameObject.GetComponent<HitPoint>();
                StartCoroutine(AttackAction(hitPoint));
            }

        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isRed && collision.gameObject.CompareTag("BlueTeam") || !_isRed && collision.gameObject.CompareTag("RedTeam"))
        {
            _isMove = true;
            _isAttack = false;
        }
    }

    public void DestoroyObject()
    {
        Destroy(gameObject);
    }
}
