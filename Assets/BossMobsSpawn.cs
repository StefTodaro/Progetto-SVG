using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMobsSpawn : MonoBehaviour
{
    public GameObject[] mobs;
    public List<GameObject> mobsSpawned;
    public bool activated = false;
    public GameObject cloud;
    public float waitTime;

    public AudioClip spawnSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            foreach (GameObject mob in mobsSpawned)
            {
                if (!mob.active)
                {
                    mobsSpawned.Remove(mob);
                }
            }
        }

        if (mobsSpawned.Count == 0 && activated)
        {
            waitTime += Time.deltaTime;
            if (waitTime > 1.5f)
            {  
                //sconfitti tutti i nemici e passato il tempo di attesa si avvia l'ultima fase
                GameObject.FindObjectOfType<Boss_logic>().StartFight();
                GameObject.FindObjectOfType<Boss_logic>().MakeBlockFall();
                GameObject.FindObjectOfType<Boss_logic>().PassToNextPhase();
                activated = false;
                waitTime = 0;
            }
        }

    }

    public void SpawnMobs()
    {
        activated = true;
        while (mobsSpawned.Count < 4)
        {
            int mobToSpawnIndex = Random.Range(0, mobs.Length);
            if (!mobs[mobToSpawnIndex].active)
            {
                SoundEffectManager.Instance.PlaySoundEffect(spawnSound, mobs[mobToSpawnIndex].transform, 0.4f);
                Instantiate(cloud, mobs[mobToSpawnIndex].transform.position, cloud.transform.rotation);
                mobs[mobToSpawnIndex].SetActive(true);
                mobsSpawned.Add(mobs[mobToSpawnIndex]);
            }
        }
    }

    public void ResetMobInScene()
    {
        activated = false;

        foreach (var mob in mobs)
        {
            mob.SetActive(false);
        }
       
        
            foreach (var mob in mobsSpawned)
            {
                mob.SetActive(false);

            }
            mobsSpawned.Clear();
        
    }
}
