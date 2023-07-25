using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ShakeCam : MonoBehaviour
{
    // Start is called before the first frame update
    public static ShakeCam Instance;

    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin m_Perlin;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
    }
    void Start()
    {
        m_Perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public IEnumerator Shake()
    {
        m_Perlin.m_AmplitudeGain = 1.0f;
        m_Perlin.m_FrequencyGain = 1.0f;
        yield return new WaitForSeconds(.4f);
        m_Perlin.m_AmplitudeGain = 0f;
        m_Perlin.m_FrequencyGain = 0f;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
