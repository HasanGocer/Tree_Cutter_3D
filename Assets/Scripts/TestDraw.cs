using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestDraw : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    bool touchPlane;
    public bool touchStartedOnPlayer;
    Touch touch;
    float yStandart = 0;
    float TempScale = 0;

    void Start()
    {
        touchStartedOnPlayer = false;
        rb.isKinematic = true;
    }

    private void OnMouseDown()
    {
        if (!touchStartedOnPlayer)
        {
            rb.isKinematic = false;
            touchStartedOnPlayer = true;
            touchPlane = true;
            RandomSystem.Instance.ObjectShake(this.gameObject);
            yStandart = transform.position.y;
            TempScale = transform.localScale.x;
            GetComponent<ObjectID>().scale = TempScale;
            StartCoroutine(Shake());
            StartCoroutine(DrawIenum());
        }
    }

    private IEnumerator DrawIenum()
    {
        yield return null;
        while (touchStartedOnPlayer)
        {
            yield return new WaitForEndOfFrame();
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (touchStartedOnPlayer)
                        {
                            Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                            Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                            RaycastHit hit;

                            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                            {
                                Debug.DrawLine(Camera.main.transform.position, direction, Color.red, 1);
                                Vector3 pos;
                                pos = new Vector3(hit.point.x, yStandart, hit.point.z);
                                touchPlane = true;
                                transform.position = Vector3.Lerp(this.transform.position, pos, 10f);
                            }
                        }
                        break;
                }
            }
            else
            {
                touchStartedOnPlayer = false;
            }
        }
        endTouch();
    }

    public void endTouch()
    {
        touchStartedOnPlayer = false;
        touchPlane = false;
        rb.isKinematic = true;
        transform.localScale = new Vector3(TempScale, TempScale, TempScale);
        RandomSystem.Instance.BackToThePlace(this.gameObject);
    }

    private IEnumerator Shake()
    {
        transform.DOShakeScale(0.3f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        float tempScale = (TempScale / 2) * 3;
        if (touchStartedOnPlayer)
            transform.localScale = new Vector3(tempScale, tempScale, tempScale);
        else
            transform.localScale = new Vector3(TempScale, TempScale, TempScale);
    }

    /*private void Update()
    {
        timer += Time.deltaTime / 8;
        if (Input.touchCount > 0 && GameManager.Instance.isStart)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    if (timer > timeForNextRay && touchStartedOnPlayer)
                    {
                        Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                        Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                        {
                            touchPlane = true;
                            cuberFollow.CuberFootPrint.Add(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                            Vector3 pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                            transform.position = Vector3.Slerp(this.transform.position, pos, 10f);
                            timer = 0;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (touchPlane)
                    {
                        touchStartedOnPlayer = false;
                        touchPlane = false;
                        CuberID cuberID = this.GetComponent<CuberID>();
                        StartCoroutine(CuberSystem.Instance.CallCubeBlastAfterMove(this, cuberID, this.gameObject, this.gameObject, CuberSystem.Instance.cubeMoveConstant, cuberID.CubeGameObject));
                        CounterSystem.Instance.CounterCameraIntegrated();
                        rb.isKinematic = true;
                    }
                    break;
            }
        }
    }*/
}
