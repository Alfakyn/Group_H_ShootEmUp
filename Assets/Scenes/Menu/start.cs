using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
    }
    public void SceneSwitch()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        //if hovering over the image, the start increasing with scale until specif level has bee reached
    }

}
