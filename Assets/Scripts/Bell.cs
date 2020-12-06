using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bell : MonoBehaviour {
  private readonly float ringForce = -0.05f;

  public void Ring() {
    Shake();

    var audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }

  public void Shake() =>
    GetComponent<Rigidbody>().AddTorque(
      new Vector3(ringForce, 0, 0), ForceMode.Impulse
    );

  void OnMouseDown() {
    var sequencer = GameObject.Find("Sequencer").GetComponent<BellSequencer>();

    if (gameObject == sequencer.CurrentBell.gameObject) {
      sequencer.Success();
    } else {
      sequencer.Fail();
    }
  }
}
