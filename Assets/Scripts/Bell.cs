using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bell : MonoBehaviour {
  /// <summary>
  /// The amount of force to exert on the bell when ringing it.
  /// </summary>
  readonly float ringForce = -0.05f;

  public void Ring() {
    Shake();
    GetComponent<AudioSource>().Play();
  }

  public void Shake() =>
    GetComponent<Rigidbody>().AddTorque(
      new Vector3(ringForce, 0, 0), ForceMode.Impulse
    );

  void OnMouseDown() {
    var sequencer = GameObject.Find("Sequencer").GetComponent<BellSequencer>();

    if (gameObject == sequencer.RequiredBell.gameObject) {
      sequencer.Success();
    } else {
      sequencer.Fail();
    }
  }
}
