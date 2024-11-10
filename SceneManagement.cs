using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    
    
        public void Schimbdescenaamornumaivreauplskillmemaaruncdelaetaj()
        {
            // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
            SceneManager.LoadScene("MeniuIntrebari", LoadSceneMode.Additive);
        }
    public void Pulamea()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

}