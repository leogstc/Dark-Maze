using UnityEngine;
using System.Collections;

public class flamefps : MonoBehaviour
{

    private SysFpsCounter m_SysFpsCounter = null;

    void Awake()
    {
        DontDestroyOnLoad(this);

        Application.targetFrameRate = -1;

        m_SysFpsCounter = new SysFpsCounter();
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
