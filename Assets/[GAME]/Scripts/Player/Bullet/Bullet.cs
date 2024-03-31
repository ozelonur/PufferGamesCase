using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Enemy;
using UnityEngine;

namespace _GAME_.Scripts.Player.Bullet
{
    public class Bullet : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private GameObject muzzlePrefab;

        [SerializeField] private GameObject hitPrefab;

        [Header("Configurations")] [SerializeField]
        private bool rotate;

        [SerializeField] private float rotateAmount = 45f;
        [SerializeField] private float speed;

        #endregion

        #region Private Variables
        private float speedRandomness;
        private bool collided;
        private Rigidbody bulletRigidBody;
        private bool _canMove;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            bulletRigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_canMove)
            {
                return;
            }
            
            if (rotate)
            {
                transform.Rotate(0, 0, rotateAmount, Space.Self);
            }
            bulletRigidBody.position += (transform.forward) * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (collided)
            {
                return;
            }

            if (other.transform.TryGetComponent(out EnemyHealthController enemy))
            {
                enemy.TakeDamage(5);
            }

            collided = true;
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            // if (collided)
            // {
            //     return;
            // }
            //
            // if (other.transform.TryGetComponent(out EnemyHealthController enemy))
            // {
            //     enemy.TakeDamage(5);
            // }
            //
            // collided = true;
            //
            speed = 0;
            GetComponent<Rigidbody>().isKinematic = true;
            
            ContactPoint contact = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitPrefab != null)
            {
                GameObject hitVFX = Instantiate(hitPrefab, pos, rot);
                hitVFX.transform.parent = other.transform;
            
                ParticleSystem ps = hitVFX.GetComponent<ParticleSystem>();
                if (ps == null)
                {
                    ParticleSystem psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
                else
                    Destroy(hitVFX, ps.main.duration);
            }

            // StartCoroutine(DestroyParticle(0f));
            Destroy(gameObject);
        }

        #endregion

        #region Private Variables

        private IEnumerator DestroyParticle(float waitTime)
        {
            if (transform.childCount > 0 && waitTime != 0)
            {
                List<Transform> tList = transform.GetChild(0).transform.Cast<Transform>().ToList();

                while (transform.GetChild(0).localScale.x > 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    for (int i = 0; i < tList.Count; i++)
                    {
                        tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
            }

            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }

        #endregion

        #region Public Methods

        public void InitBullet(Transform parent)
        {
            GameObject muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.parent = parent;
            muzzleVFX.transform.localPosition = Vector3.zero;
            muzzleVFX.transform.localEulerAngles = Vector3.zero;
            ParticleSystem ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(muzzleVFX, ps.main.duration);
            }
            else
            {
                ParticleSystem psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }

            _canMove = true;
        }

        #endregion
    }
}