using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mob_manager : MonoBehaviour
{
    public List<GameObject> Mobs;
    public GameObject cloud;

    public AudioClip respawnSound;
    // Start is called before the first frame update
    void Start()
    {
        Mobs = FindObjectsOfType<Mob_respawn>().Select(o => o.gameObject).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject mob in Mobs)
        {
            if (!mob.active)
            {
                MobSpawn(mob);
            }
        }
    }

    public void MobSpawn(GameObject mob)
    {

        mob.GetComponent<Mob_respawn>().respawnTimer += Time.deltaTime;

        if (mob.GetComponent<Mob_respawn>().respawnTimer >= 12)
        {
            MakeCloud(mob.transform);

            mob.GetComponent<ResettableObjects>().ResetState();
        }
    }

    public void MakeCloud(Transform t)
    {
        SoundEffectManager.Instance.PlaySoundEffect(respawnSound, t, 0.4f);
        Instantiate(cloud, t.position, cloud.transform.rotation);

    }



}
