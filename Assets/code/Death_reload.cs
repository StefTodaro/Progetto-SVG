using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_reload : MonoBehaviour
{
    public Vector2 respawnPosition;
    void Start()
    {
        
    }

    public void SetPlayerLastCheckpoint(Vector2 checkpoint)
    {
        respawnPosition = checkpoint;
    }

    public void RespawnPlayerAtLastCheckpoint()
    {
        // Ottieni la posizione dell'ultimo checkpoint
        

        // Respawn del giocatore alla posizione del checkpoint
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPosition;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RespawnPlayerAtLastCheckpoint();
    }
}
