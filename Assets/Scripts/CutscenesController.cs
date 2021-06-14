using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class CutscenesController : MonoBehaviour
{
    private LevelManager _levelManageer;
    public GameObject player;
    public GameObject velha;
    public GameObject bofia;
    public GameObject charactersCutscene;
    public Animator rogerioCutsceneAnimator;
    public Animator velhaCutsceneAnimator;
    public Animator bofiaCutsceneAnimator;
    public Animator senhorTonoAnimator;
    public NavMeshAgent bofiaNavAgent;
    private Transform _velhaCutsceneWaypoint;
    private CameraManager _cameraManager;
    private Animator _sceneTransitionAnimator;
    
    void Start()
    {
        _levelManageer = FindObjectOfType<LevelManager>();
        _velhaCutsceneWaypoint = GameObject.FindGameObjectWithTag("VelhaCutsceneWaypoint").GetComponent<Transform>();
        _cameraManager = FindObjectOfType<CameraManager>();
        _sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
    }

    public void StartVelhaCutscene()
    {
        StartCoroutine(VelhaCutscene());
    }

    public void StartBofiaCutscene()
    {
        StartCoroutine(BofiaCutscene());
    }

    public void StartSenhorTonoCutscene()
    {
        StartCoroutine(SenhorTonoCutscene());
    }

    IEnumerator VelhaCutscene()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _cameraManager.SwitchCamera("BustedVelha");
        player.SetActive(false);
        velha.SetActive(false);
        bofia.SetActive(false);
        charactersCutscene.SetActive(true);
        rogerioCutsceneAnimator.SetTrigger("StartVelhaCutscene");
        velhaCutsceneAnimator.SetTrigger("StartCutscene");
        bofiaCutsceneAnimator.SetTrigger("StartVelhaCutscene");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        bofiaNavAgent.SetDestination(_velhaCutsceneWaypoint.position);
        while (Vector3.Distance(bofiaNavAgent.transform.position, _velhaCutsceneWaypoint.position) > 0.5f)
        {
            yield return null;
        }
        bofiaCutsceneAnimator.SetTrigger("PointWeapon");
        yield return new WaitForSeconds(1.5f);
        rogerioCutsceneAnimator.SetTrigger("Busted");
        yield return new WaitForSeconds(5f);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _levelManageer.LoadGameOVer();
    }
    
    IEnumerator BofiaCutscene()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _cameraManager.SwitchCamera("BustedBofia");
        player.SetActive(false);
        bofia.SetActive(false);
        charactersCutscene.SetActive(true);
        rogerioCutsceneAnimator.SetTrigger("StartBofiaCutscene");
        bofiaCutsceneAnimator.SetTrigger("StartBofiaCutscene");
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        bofiaCutsceneAnimator.SetTrigger("PointWeapon");
        yield return new WaitForSeconds(1.5f);
        rogerioCutsceneAnimator.SetTrigger("Busted");
        yield return new WaitForSeconds(5f);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _levelManageer.LoadGameOVer();
    }

    IEnumerator SenhorTonoCutscene()
    {
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _cameraManager.SwitchCamera("ParadiseEnding");
        player.SetActive(false);
        charactersCutscene.SetActive(true);
        senhorTonoAnimator.Rebind();
        senhorTonoAnimator.Update(0f);
        yield return new WaitForSeconds(0.5f);
        _sceneTransitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        senhorTonoAnimator.SetTrigger("Final");
        yield return new WaitForSeconds(7f);
        _sceneTransitionAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        _levelManageer.LoadGoodEnding();
    }
}
