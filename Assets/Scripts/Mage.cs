using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : MonoBehaviour
{
    [Header("Basic Vars")]
    public GameObject player;
    
    public LayerMask enemyLayer;
    float timeNextCd;

    [Space(10)]

    [Header("Q Attributes")]
    private bool canQ = true;
    public GameObject qCdSlide;
    public Slider qCdUI;
    private float qCD = 3f;
    private float nextFireQ = 0f;

    public GameObject beamObj;
    public Transform beamFirePoint;
    public LineRenderer lr;
    public AudioSource beamSound;

    

    [Space(10)]

    [Header("W Attributes")]
    private float wCD = 3f;
    public Slider wCdUI;
    public GameObject wCdSlide;

    public ParticleSystem blastFx;

    public AudioSource blastSoundSource;
    public AudioClip blastSoundClip;

    [Space(10)]

    [Header("E Attributes")]
    public GameObject slashObj;
    public ParticleSystem eOnHitFX;

    bool canE = true;
    private float eCD = 0.2f;
    public Slider eCdUI;
    public GameObject eCdSlide;

    public AudioSource slashSound;
    public AudioClip slashClip;

    bool isCd;
    bool isQCd;
    private void Start()
    {
        isCd = false;
        isQCd = false;
        beamObj.SetActive(false);
        //aoeIndicator.SetActive(false);
        qCdUI.value = 0;
        qCD = 0;
        wCdUI.value = 0;
        wCD = 0;
        eCdUI.value = 0;
        eCD = 0;
    }

    private void Update()
    {
        Debug.Log(isQCd);

        #region mess
        //wCdUI.value = wCD;
        qCdUI.value = qCD;
        eCdUI.value = eCD;
        if (beamObj.activeSelf == true)
        {
            lr.SetPosition(0, beamFirePoint.position);
        }
        #endregion
        #region q

        if (qCD > 0)
        {
            qCdSlide.SetActive(true);
            qCD -= Time.deltaTime;
            canQ = false;
            if (qCD < 0)
            {
                qCD = 0;
            }
        }
        else if (qCD <= 0)
        {
            qCdSlide.SetActive(false);
            canQ = true;
        }

        if (canQ)
        {

            if(Input.GetKey(KeyCode.Q))
            {
                beam();
                
                StartCoroutine(beamTimer());
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                beamSound.Play();
            }
            
        }

        #endregion
        #region w
        if (Input.GetKeyDown(KeyCode.W) && isCd == false)
        {
            blast();
            isCd = true;
            wCdUI.value = 3;
        }

        if (isCd == true)
        {

            float f = wCdUI.value - Time.deltaTime;
            //Debug.Log(f);
            wCdUI.value = f;
            if (wCdUI.value == 0)
            {
                isCd = false;
            }
        }
        #endregion
        #region e
        if (eCD > 0)
        {
            eCdSlide.SetActive(true);
            eCD -= Time.deltaTime;
            canE = false;
            if (eCD < 0)
            {
                eCD = 0;
            }
        }
        else if (eCD <= 0)
        {
            eCdSlide.SetActive(false);
            canE = true;
        }

        if (canE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                shootSlash();
            }
        }
        #endregion
    }

    #region Q Beam
    
    public void beam()
    {

        beamObj.SetActive(true);
        RaycastHit2D hitInfo = Physics2D.Raycast(beamFirePoint.position, beamFirePoint.right, enemyLayer);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            enemy.TakeDamage(5);
        }
        else
        {
            drawBeam(beamFirePoint.position, beamFirePoint.transform.right * 100);
        }
    }
    
    IEnumerator beamTimer()
    {
        yield return new WaitForSeconds(2f);

        qCD = 4;

        canQ = false;
        beamObj.SetActive(false);
    }



    void drawBeam(Vector2 firePoint, Vector2 range)
    {
        //lr.SetPosition(0, firePoint);
        lr.SetPosition(1, range);
    }


    #endregion

    #region W Blast
    private bool canW = true;
    public void blast()
    {
        blastSoundSource.PlayOneShot(blastSoundClip, 1f);
        blastFx.Play();
        //aoeIndicator.SetActive(true);
        //canW = false;

        Collider2D[] aoe = Physics2D.OverlapCircleAll(player.transform.position, 5, enemyLayer);

        foreach (Collider2D hit in aoe)
        {
            if (hit != null)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                enemy.TakeDamage(20);
            }
        }
        //StartCoroutine(blastCD());
    }

    IEnumerator blastCD()
    {
        yield return new WaitForSeconds(.1f);
        wCD = 3f;
        //aoeIndicator.SetActive(false);
    }
    #endregion

    #region E Slash Shoot
    void shootSlash()
    {
        slashSound.PlayOneShot(slashClip, 1.5f);
        GameObject proj = Instantiate(slashObj, beamFirePoint.position, beamFirePoint.rotation);
        Rigidbody2D eRb = proj.GetComponent<Rigidbody2D>();
        eRb.AddForce(beamFirePoint.right * 20f, ForceMode2D.Impulse);

        if(proj.GetComponent<ProjectileBehaviour>().hitPos != null)
        {
            Instantiate(eOnHitFX, proj.GetComponent<ProjectileBehaviour>().hitPos, Quaternion.identity);
            
        }

        eCD = .2f;
    }
    #endregion
}
