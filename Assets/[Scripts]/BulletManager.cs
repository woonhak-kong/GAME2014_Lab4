using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }
    public GameObject bulletParent;
    public int playerBulletFired = 0;
    public int playerBulletLeft = 0;
    public int enemyBulletFired = 0;
    public int enemyBulletLeft = 0;
    [Range(10, 200)]
    public int playerBulletNumber = 50;
    [Range(10, 200)]
    public int enemyBulletNumber = 50;

    private BulletFactory bulletFactory;
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> enemyBulletPool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        playerBulletPool = new Queue<GameObject>();
        enemyBulletPool = new Queue<GameObject>();
        bulletFactory = FindObjectOfType<BulletFactory>();
        BuildBulletPool();
    }

    void BuildBulletPool()
    {
        for (int i = 0; i < playerBulletNumber; i++)
        {
            playerBulletPool.Enqueue(bulletFactory.CreateBullet(BulletType.PLAYER));
            playerBulletLeft++;
        }
        for (int i = 0; i < enemyBulletNumber; i++)
        {
            enemyBulletPool.Enqueue(bulletFactory.CreateBullet(BulletType.ENEMY));
            enemyBulletLeft++;
        }
    }


    public GameObject GetBullet(Vector2 position, BulletDirection direction, BulletType type)
    {
        if (playerBulletPool.Count < 1)
        {
            playerBulletPool.Enqueue(bulletFactory.CreateBullet(BulletType.PLAYER));
            playerBulletLeft++;
        }
        if (enemyBulletPool.Count < 1)
        {
            enemyBulletPool.Enqueue(bulletFactory.CreateBullet(BulletType.ENEMY));
            enemyBulletLeft++;
        }

        GameObject bullet = null;

        switch(type)
        {
            case BulletType.PLAYER:
                bullet = playerBulletPool.Dequeue();
                playerBulletFired++;
                playerBulletLeft--;
                
                break;
            case BulletType.ENEMY:
                bullet = enemyBulletPool.Dequeue();
                enemyBulletFired++;
                enemyBulletLeft--;
                break;
        }

        bullet.transform.position = position;
        bullet.SetActive(true);
        
        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        switch (type)
        {
            case BulletType.PLAYER:
                playerBulletPool.Enqueue(bullet);
                playerBulletFired--;
                playerBulletLeft++;

                break;
            case BulletType.ENEMY:
                enemyBulletPool.Enqueue(bullet);
                enemyBulletFired--;
                enemyBulletLeft++;
                break;
        }
    }

}
