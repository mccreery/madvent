using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandbellRinger : MonoBehaviour {
  public void Ring() {
    var audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }

  void OnMouseDown() {
    Ring();
  }
}
