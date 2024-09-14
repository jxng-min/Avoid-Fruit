using _EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    [SerializeField]
    private string m_scene_name;

    public void SwitchScene()
    {
        GameEventBus.Publish(GameEventType.SETTING);
        SceneManager.LoadScene(m_scene_name);
    }
}