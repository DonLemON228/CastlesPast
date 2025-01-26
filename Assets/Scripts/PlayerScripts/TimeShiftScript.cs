using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeShiftScript : MonoBehaviour
{
    [SerializeField] Animator m_timeshiftAnimator;
    [SerializeField] AudioSource m_timeshiftAudioSource;
    [SerializeField] AudioClip m_timeshiftClip1;
    [SerializeField] AudioClip m_timeshiftClip2;
    [SerializeField] float timeBeforeShift = 0.5f;
    [SerializeField] float shiftCooldown = 1;
    [SerializeField] Light2D light;
    [SerializeField] float oldLightValue;
    [SerializeField] float newLightValue;
    [SerializeField] PauseMenuScript pauseMenu;
    [SerializeField] List<GameObject> gridOldList = new List<GameObject>();
    [SerializeField] List<GameObject> gridNewList = new List<GameObject>();
    [SerializeField] List<GameObject> decorationsOldList = new List<GameObject>();
    [SerializeField] List<GameObject> decorationsNewList = new List<GameObject>();
    [SerializeField] List<InterectiveObjectsTimeShift> interectiveObjectsList = new List<InterectiveObjectsTimeShift>();
    [SerializeField] List<SwitcherDisable> switcherDisablesList = new List<SwitcherDisable>();
    public bool m_camUseTimeShift = true;
    private bool m_canActive = true;
    private bool m_isOld = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void TimeShift()
    {
        m_isOld = !m_isOld;

        if (interectiveObjectsList != null)
        {
            foreach (var interectiveObjects in interectiveObjectsList)
            {
                interectiveObjects.TimeShift(m_isOld);
            }
        }
        
        if (m_isOld)
        {
            light.intensity = newLightValue;
            m_timeshiftAudioSource.clip = m_timeshiftClip1;
            m_timeshiftAudioSource.Play();
            if (gridOldList != null)
            {
                foreach (var gridOld in gridOldList)
                {
                    gridOld.SetActive(false);
                }
            }

            if (decorationsOldList != null)
            {
                foreach (var decorationsOld in decorationsOldList)
                {
                    decorationsOld.SetActive(false);
                }
            }

            if (gridNewList != null)
            {
                foreach (var gridNew in gridNewList)
                {
                    gridNew.SetActive(true);
                }
            }

            if (decorationsNewList != null)
            {
                foreach (var decorationsNew in decorationsNewList)
                {
                    decorationsNew.SetActive(true);
                }
            }
            
            if (switcherDisablesList.Count != null)
            {
                foreach (var switcherDisables in switcherDisablesList)
                {
                    switcherDisables.SwitcherOn();
                }
            }
        }
        else
        {
            light.intensity = newLightValue;
            m_timeshiftAudioSource.clip = m_timeshiftClip2;
            m_timeshiftAudioSource.Play();
            
            if (gridOldList != null)
            {
                foreach (var gridOld in gridOldList)
                {
                    gridOld.SetActive(true);
                }
            }

            if (decorationsOldList != null)
            {
                foreach (var decorationsOld in decorationsOldList)
                {
                    decorationsOld.SetActive(true);
                }
            }

            if (gridNewList != null)
            {
                foreach (var gridNew in gridNewList)
                {
                    gridNew.SetActive(false);
                }
            }

            if (decorationsNewList != null)
            {
                foreach (var decorationsNew in decorationsNewList)
                {
                    decorationsNew.SetActive(false);
                }
            }
            
            if (switcherDisablesList != null)
            {
                foreach (var switcherDisables in switcherDisablesList)
                {
                    switcherDisables.SwitcherOff();
                }
            }
        }
        StartCoroutine(TimeShiftCooldown());
    }

    IEnumerator TimeShiftCooldown()
    {
        yield return new WaitForSeconds(shiftCooldown);
        m_canActive = true;
    }
    
    IEnumerator TimeBeforeTimeshift()
    {
        yield return new WaitForSeconds(timeBeforeShift);
        TimeShift();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && m_canActive && m_camUseTimeShift && !pauseMenu.isPaused)
        {
            m_canActive = false;
            m_timeshiftAnimator.SetTrigger("TimeShift");
            StartCoroutine(TimeBeforeTimeshift());
        }
    }
}
