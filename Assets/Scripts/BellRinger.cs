using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BellRinger : MonoBehaviour {
  public void Ring() {
    var audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }

  void OnMouseDown() {
    var sequencer = GameObject.Find("Sequencer").GetComponent<BellSequencer>();

    if (gameObject == sequencer.Last) {
      sequencer.Success();
    } else {
      sequencer.Fail();
    }
  }
}
