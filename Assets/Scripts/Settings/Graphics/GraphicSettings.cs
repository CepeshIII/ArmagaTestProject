using UnityEngine;


public class GraphicSettings : MonoBehaviour
{
    [SerializeField] private int frameRate = 60;



    public void Start()
    {
        DontDestroyOnLoad(this);
    }


    void OnEnable()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;

    }
}
