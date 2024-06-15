
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class levelManager : MonoBehaviour
{
    
    public static levelManager Instance { get; private set; }

    public List<int> levelOrder = new List<int> { 1, 3, 4, 5 };
    private int currentLevelIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    StatusLevel statusLevel;
    private void Start()
    {
        statusLevel = GameObject.FindGameObjectWithTag("wayPoint").GetComponent<StatusLevel>();
        if (statusLevel != null)
        {
            statusLevel.setStatusLevel(1, true);
        }
        else
        {
            Debug.LogError("StatusLevel not found in scene.");
        }
    }

    public void StartLevel(int levelNumber)
    {
        currentLevelIndex = levelOrder.IndexOf(levelNumber);

        SceneManager.LoadScene("Level" + levelNumber);
    }

    public void CompleteCurrentLevel()
    {
        if (currentLevelIndex < 0 || currentLevelIndex >= levelOrder.Count)
        {
            Debug.LogError("Invalid current level index: " + currentLevelIndex);
            return;
        }

        int currentLevelNumber = levelOrder[currentLevelIndex];
        PlayerPrefs.SetInt("Level" + currentLevelNumber + "Complete", 1);
        PlayerPrefs.Save();
        currentLevelIndex++;

        if (currentLevelIndex < levelOrder.Count - 1)
        {
            int nextLevelNumber = levelOrder[currentLevelIndex + 1];
            if(statusLevel != null)
            {
                statusLevel.setStatusLevel(nextLevelNumber, true);
            } else
            {
                Debug.LogError("StatusLevel notFound in scene");
            }
            
        }

        SceneManager.LoadScene("Selezione livelli");
    }
}
