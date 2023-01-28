using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f; //Le damos un valor por defecto
    [SerializeField] GameObject bulletImpactEffect;
    [SerializeField] GameObject bulletImpactEffectOnEnemy;
    [SerializeField] int dmgBullet = 10;
    [SerializeField] int sfxToPlay;


    private Rigidbody2D bulletRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidBody.velocity = transform.right * bulletSpeed; //Queremos que se mueva solo en el eje x (local)
    }

    private void OnTriggerEnter2D(Collider2D collision) //Collision es el elemento con el que choicamos
    {
        
        //Si con lo que chocamos es un enemigo, ejecuta la funcion
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(bulletImpactEffectOnEnemy.transform, transform.position, Quaternion.Euler(0, 0, 0));
            collision.GetComponent<EnemyController>().DamageEnemy(dmgBullet);
            PlayWeaponSFX();
        }
        else
        {
            Instantiate(bulletImpactEffect.transform, transform.position, transform.localRotation);
        }

        Destroy(gameObject);
    }

    public void PlayWeaponSFX()
    {
        AudioManager.instance.PlaySFX(sfxToPlay);
    }
}
