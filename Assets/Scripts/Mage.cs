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
    //cd
    private bool canSpeed = true;
    public GameObject speedCDSlide;
    public Slider speedCDUI;
    private float speedCD = 5f;

    //obj
    public GameObject beamObj;
    public Material qMat;
    public Transform beamFirePoint;
    public LineRenderer lr;
    public AudioSource speedUpSound;

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
    public Transform eFirePoint;

    [HideInInspector] public Vector3 eHit;

    bool canShoot = true;
    private float shootCD = 0.2f;
    public Slider shootCdUI;
    public GameObject shootCDSlide;

    public AudioSource slashSound;
    public AudioClip slashClip;

    public bool speedBuff = false;

    bool isCd;
    bool isspeedCD;
    private void Start()
    {
        isCd = false;
        isspeedCD = false;
        beamObj.SetActive(false);
        //aoeIndicator.SetActive(false);
        speedCDUI.value = 0;
        speedCD = 0;
        wCdUI.value = 0;
        wCD = 0;
        shootCdUI.value = 0;
        shootCD = 0;
    }

    private void Update()
    {

        #region mess
        //wCdUI.value = wCD;
        speedCDUI.value = speedCD;
        shootCdUI.value = shootCD;
        if (beamObj.activeSelf == true)
        {
            lr.SetPosition(0, beamFirePoint.position);
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

        #region q?
        if (shootCD > 0)
        {
            shootCDSlide.SetActive(true);
            shootCD -= Time.deltaTime;
            canShoot = false;
            if (shootCD < 0)
            {
                shootCD = 0;
            }
        }
        else if (shootCD <= 0)
        {
            shootCDSlide.SetActive(false);
            canShoot = true;
        }

        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shootSlash();
            }
        }
        #endregion

        #region e?
        if (speedCD > 0)
        {
            speedCDSlide.SetActive(true);
            speedCD -= Time.deltaTime;
            canSpeed = false;
            if (speedCD < 0)
            {
                speedCD = 0;
            }
        }
        else if (speedCD <= 0)
        {
            speedCDSlide.SetActive(false);
            canSpeed = true;
        }

        if (canSpeed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(speedChange());
                speedCD = 5;
            }
        }
        #endregion
    }

   

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
                Instantiate(eOnHitFX, hit.transform.position, Quaternion.identity);
                enemy.TakeDamage(50);
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
        GameObject proj = Instantiate(slashObj, eFirePoint.position, eFirePoint.rotation);
        Rigidbody2D eRb = proj.GetComponent<Rigidbody2D>();
        eRb.AddForce(beamFirePoint.right * 20f, ForceMode2D.Impulse);


        RaycastHit2D hit = Physics2D.Raycast(eFirePoint.position, eFirePoint.right);
        if (hit.point != null)
        {
            eHit = hit.point;

            if(proj.GetComponent<Collider2D>().CompareTag("Hittable"))
            {
                Debug.Log(hit.point);
                Instantiate(eOnHitFX, hit.point, Quaternion.LookRotation(hit.normal));
            }
            //Instantiate(eOnHitFX, hit.point, Quaternion.LookRotation(hit.normal));
        }

        //Debug.Log(hit.point);


        shootCD = .2f;
    }
    #endregion

    #region speed up


    IEnumerator speedChange()
    {
        speedUpSound.Play();
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        movement.speed = 8f;
        movement.hp += 30;
        speedBuff = true;
        yield return new WaitForSeconds(1f);
        movement.speed = 3f;
    }
    #endregion
}
