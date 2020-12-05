using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandbellRing : MonoBehaviour {
  public void OnMouseDown() {
    var audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }
}
