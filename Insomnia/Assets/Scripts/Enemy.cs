using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private GameObject _cannon;
   
   [SerializeField] private bool isMele;

   [Header("Statistiques de l'ennemie")]
   [SerializeField] private int uniteLife;
   [SerializeField] private float _uniteSpeed;
   
   [SerializeField] private int _uniteMeleDamage;
   [SerializeField] private int _uniteRangeDamage;
   
   [SerializeField] private float _uniteRange;
   private bool _enemyIsInRange;
   private float distance;
   
   [Header("Cooldown")]
   [SerializeField] private float strikeCd;
   [SerializeField] private float shootCd;
   private bool canEngage;

   private GameObject target;
   
   Rigidbody2D rb;
   [SerializeField] private bool canMove;

   private void Awake()
   {
      if (GameManager.instance.isFromPlayer1)
      {
         gameObject.tag = "IsFromPlayer1";
         gameObject.layer = 6;
      }
      else
      {
         gameObject.tag = "IsFromPlayer2";
         gameObject.layer = 7;
         transform.rotation = Quaternion.Euler(0,-180,0);
      }
      
      rb = gameObject.GetComponent<Rigidbody2D>();
      canEngage = true;
      canMove = true;
   }

   private void Update()
   {
      if (!canMove)
      {
         rb.constraints = RigidbodyConstraints2D.FreezeAll;
      }
      
      if (gameObject.tag == "IsFromPlayer1")
      {
         rb.velocity = Vector2.right * _uniteSpeed;
      }
      else
      {
            rb.velocity = Vector2.left * _uniteSpeed;
      }
      
      
      if (_enemyIsInRange)
      {
         canMove = false;
         
         if (isMele)
         {
            StartCoroutine(Strike());
         }
         else
         {
            if (distance <= 1)
            {
               StartCoroutine(Strike());
            }
            else
            {
               StartCoroutine(Shoot());
            }
         }
      }
      else
      {
         canMove = true;
      }

      if (uniteLife <= 0)
      {
         Destroy(gameObject);
         if (gameObject.tag == "IsFromPlayer1")
         {
            GameManager.instance.player2Gold += 10;
            GameManager.instance.UpdateUI();
         }
         else
         {
            GameManager.instance.player1Gold += 10;
            GameManager.instance.UpdateUI();
         }
      }
   }

   private void FixedUpdate()
   {
      RaycastHit2D hit2D = Physics2D.Raycast(_cannon.transform.position, transform.right, _uniteRange);
      Debug.DrawRay(_cannon.transform.position, transform.right * _uniteRange, Color.red);

      if (hit2D.collider != null)
      {
         if (hit2D.collider.gameObject.tag != gameObject.tag)
         {
             distance = Mathf.Abs(hit2D.point.y - transform.position.y); // Distance entre l'unité et l'ennemie touché
             _enemyIsInRange = true;
             target = hit2D.collider.gameObject;
         }
         else
         {
            _enemyIsInRange = false;
         }
      }
   }

   IEnumerator Strike()
   {
      if (canEngage)
      {
         if (target == GameManager.instance.basePlayer1)
         {
            GameManager.instance.basePlayer1Life -= _uniteMeleDamage;
         }
         else if (target == GameManager.instance.basePlayer2)
         {
            GameManager.instance.basePlayer2Life -= _uniteMeleDamage;
         }
         else
         {
            target.GetComponent<Enemy>().uniteLife -= _uniteMeleDamage;
         }
         canEngage = false;
         GameManager.instance.UpdateUI();
         yield return new WaitForSeconds(strikeCd);
         canEngage = true;
      }
   }

   IEnumerator Shoot()
   {
      if (canEngage)
      {
         if (target == GameManager.instance.basePlayer1)
         {
            GameManager.instance.basePlayer1Life -= _uniteRangeDamage;
         }
         else if (target == GameManager.instance.basePlayer2)
         {
            GameManager.instance.basePlayer2Life -= _uniteRangeDamage;
         }
         else
         {
            target.GetComponent<Enemy>().uniteLife -= _uniteRangeDamage;
         }
         canEngage = false;
         GameManager.instance.UpdateUI();
         yield return new WaitForSeconds(shootCd);
         canEngage = true;
      }
   }
}