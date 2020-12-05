using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public string snowballTag = "snowball";
    public float recoveryTime = 3.0f;

    public GameObject targetModel;
    public Quaternion hitRotation;
    public float rotationSpeed = 90f;

    private bool hit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == snowballTag)
        {
            hit = true;
            Destroy(other.gameObject);
            StartCoroutine(recoverTarget());
        }
    }

    private IEnumerator recoverTarget()
    {
        yield return new WaitForSeconds(recoveryTime);
        hit = false;
    }

    private void Update()
    {
        Quaternion modelRotation;

        if (hit)
        {
            modelRotation = Quaternion.RotateTowards(targetModel.transform.rotation, hitRotation, rotationSpeed);
        }
        else
        {
            modelRotation = Quaternion.RotateTowards(hitRotation, targetModel.transform.rotation, rotationSpeed);
        }

        targetModel.transform.rotation = modelRotation;
    }
}
