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
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, BulletParent);
        switch (type)
        {
            case BulletType.PLAYER:
                bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                bullet.GetComponent<BuilletBehavior>().bulletDirection = BulletDirection.UP;

                break;
            case BulletType.ENEMY:
                bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                bullet.GetComponent<BuilletBehavior>().bulletDirection = BulletDirection.DOWN;
                break;
        }
        bullet.GetComponent<BuilletBehavior>().type = type;
        bullet.SetActive(false);
        return bullet;
    }

}
