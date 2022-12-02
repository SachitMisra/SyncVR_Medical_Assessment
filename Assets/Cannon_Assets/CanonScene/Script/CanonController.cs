using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class CanonProperties
{
    public float elevation;
    public float power;
}
[System.Serializable]
public class CanonUI
{
    public TextMeshPro powerMeter;
}
[System.Serializable]
public class VFX
{
    public ParticleSystem canonBlast;
}
public class CanonController : MonoBehaviour
{

    public CanonProperties canonProperties;
    public CanonUI canonUI;
    public VFX vFX;
    public GameObject projectile;
    public GameObject launchPoint;
    public AudioClip shootsound;
    AudioSource source;
    Vector3 initialNosslePosition;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        initialNosslePosition = launchPoint.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Controls();
        UpdateUI();
        UpdateVFX();
        
    }
    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(shootsound, canonProperties.power/1000);

            GameObject launchThis = Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation) as GameObject;
            launchThis.tag = "Clone";
            launchThis.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, -canonProperties.power, 0));
            launchPoint.transform.position = launchPoint.transform.TransformPoint(new Vector3(0,canonProperties.power * Mathf.Pow(10,-4),0));
        }
        launchPoint.transform.position = Vector3.Lerp(launchPoint.transform.position,initialNosslePosition,Time.deltaTime*10);
        canonProperties.power = Mathf.Clamp(canonProperties.power + Input.GetAxis("Horizontal")*3,200,1000);
        canonProperties.elevation = Mathf.Clamp(canonProperties.elevation + Input.GetAxis("Vertical") * 0.5f, 100,170);
        launchPoint.transform.localEulerAngles = new Vector3(0,0, canonProperties.elevation);
    }
    void UpdateUI()
    {
        canonUI.powerMeter.rectTransform.localScale = new Vector3(canonProperties.power *5 * Mathf.Pow(10,-5), 0.05f,0.05f);
        canonUI.powerMeter.color = Color.Lerp(Color.yellow, Color.red, canonProperties.power/1000);
    }
    void UpdateVFX()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vFX.canonBlast.Play();
        }
        else
        {
            vFX.canonBlast.Stop();
        }
    }

}
