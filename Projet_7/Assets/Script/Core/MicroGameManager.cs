using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager
/// </summary>
public class MicroGameManager : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _virtualCamera;

    private static MicroGameManager instance;
    public static MicroGameManager Instance
    {
        get => instance;
        private set => instance = value;
    }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogAssertion("There is already an instance of the MicroGameManager in the scene");
            return;
        }

        instance = this;

        RegisterPlayer();
    }

    /* * * * * * *
     * GAME STATES
    * * * * * * */
    public void ReloadGame() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void EndGame() { 
    }

    private void Reset() {  
    }


    public void UpdateCinemachineCameraTracking(Transform target)
    {
        _virtualCamera.m_Follow = target;
        _virtualCamera.m_LookAt = target;
    }


    /// <summary>
    /// Enregistrement du player pour stocker sa référence.
    /// </summary>
    private void RegisterPlayer()
    {
    }
}
