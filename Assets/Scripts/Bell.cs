using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bell : MonoBehaviour {
  public void Ring() {
    var audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }

  void OnMouseDown() {
    var sequencer = GameObject.Find("Sequencer").GetComponent<BellSequencer>();

    GetComponent<Rigidbody>().AddTorque(new Vector3(-0.05f, 0, 0), ForceMode.Impulse);
    if (gameObject == sequencer.Last.gameObject) {
      sequencer.Success();
    } else {
      sequencer.Fail();
    }
  }
}
