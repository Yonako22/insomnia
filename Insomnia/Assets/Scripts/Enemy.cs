using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private GameObject _enemyPrefab;
   [SerializeField] private GameObject _cannon;

   public int uniteLife;
   [SerializeField] private float _uniteSpeed;
   [SerializeField] private float _uniteMeleDamage;
   [SerializeField] private float _uniteRangeDamage;
   [SerializeField] private float _uniteRange;
   [SerializeField] private bool isMele;
   private bool _enemyIsInRange;
   private float distance;

   

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
         transform.rotation = Quaternion.Euler(0,-180,0);
      }
      
      rb = gameObject.GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
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
      RaycastHit2D hit2D = Physics2D.Raycast(_cannon.transform.position, transform.right, _uniteRange);
      Debug.DrawRay(_cannon.transform.position, transform.right * _uniteRange, Color.red);

      if (hit2D.collider != null)
      {
            distance = Mathf.Abs(hit2D.point.y - transform.position.y); // Distance entre l'unité et l'ennemie touché
            _enemyIsInRange = true;
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