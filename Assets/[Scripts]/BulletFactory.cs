using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    // Bullet Prefab
    public GameObject bulletPrefab;

    // Sprite Textures
    public Sprite playerBulletSprite;
    public Sprite enemyBulletSprite;

    // Bullet Parent
    public Transform BulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        GameObject test = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        test.GetComponent<BuilletBehavior>().speed = 0.0f;

        test.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;


    }

    private void Initialize()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprites/Bullet");
        enemyBulletSprite = Resources.Load<Sprite>("Sprites/EnemySmallBullet");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        BulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject CreateBullet(BulletType type = BulletType.PLAYER)
    {
        GameObject bullet = null;
        switch (type)
        {
            case BulletType.PLAYER:
                bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, BulletParent);
                bullet.GetComponent<BuilletBehavior>().speed = 0.0f;
                bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                bullet.GetComponent<BuilletBehavior>().bulletDirection = BulletDirection.UP;
                bullet.SetActive(false);
                break;
            case BulletType.ENEMY:
                bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, BulletParent);
                bullet.GetComponent<BuilletBehavior>().speed = 0.0f;
                bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                bullet.GetComponent<BuilletBehavior>().bulletDirection = BulletDirection.DOWN;
                bullet.SetActive(false);
                break;
        }
        return bullet;
    }

}
