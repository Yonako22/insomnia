using UnityEngine;

public class Enemy : MonoBehaviour
{

   [SerializeField] private GameObject _enemyPrefab;
   public int uniteLife;
   [SerializeField] private float _uniteSpeed;
   [SerializeField] private float _uniteMeleDamage;
   [SerializeField] private float _uniteRangeDamage;
   [SerializeField] private float _uniteRange;
   [SerializeField] private bool isMele;
   private bool _enemyIsInRange;
   private float distance;
   private GameObject target;
   
 
   
   public bool isFromPlayer1;
   
   Rigidbody2D rb;

   private void Awake()
   {
      if (isFromPlayer1)
      {
         gameObject.tag = "IsFromPlayer1";
         gameObject.layer = 6;
      }
      else
      {
         gameObject.tag = "IsFromPlayer2";
         gameObject.layer = 7;
      }
      
      rb = gameObject.GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      if (isFromPlayer1)
      {
         rb.velocity = Vector2.right * _uniteSpeed;
      }
      else
      {
         rb.velocity = Vector2.left * _uniteSpeed;
      }

      if (_enemyIsInRange)
      {
         if (isMele)
         {
           Strike();
         }
         else
         {
            if (distance <= 1)
            {
               Strike();
            }
            else
            {
               Shoot();
            }
         }
      }
      
   }

   private void FixedUpdate()
   {
      RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.forward, _uniteRange);
      Debug.DrawRay(this.gameObject.transform.position, Vector3.forward * _uniteRange * 100, Color.green);

      if (hit2D.collider != null)
      {
         distance = Mathf.Abs(hit2D.point.y - transform.position.y); // Distance entre l'unité et l'ennemie touché
         target = hit2D.transform.gameObject;
         
         if (isFromPlayer1)
         {
            if (hit2D.collider.CompareTag("IsFromPlayer2"))
            {
               _enemyIsInRange = true;
            }
         }
         else if (!isFromPlayer1)
         {
            if (hit2D.collider.CompareTag("IsFromPlayer1"))
            {
               _enemyIsInRange = true;
            }
         }
      }
   }

   public void SpawnUnite(Vector3 spawnPos)
   {
      Instantiate(_enemyPrefab,spawnPos, Quaternion.identity);
   }

   void Strike()
   {
      Debug.Log(("Strike"));
   }

   void Shoot()
   {
      Debug.Log(("Shoot"));
   }
}