using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BellSequencer : MonoBehaviour {
  /// <summary>
  /// The available bells to ring.
  /// </summary>
  public Bell[] Bells;
  /// <summary>
  /// The time interval between bell rings.
  /// </summary>
  public float TimeInterval = 0.75f;

  /// <summary>
  /// The current sequence of bells.
  /// </summary>
  public Queue<Bell> CurrentSequence;
  /// <summary>
  /// The current bell in the suquence to be matched.
  /// </summary>
  public Bell CurrentBell => CurrentSequence.Peek();

  void Start() {
    NewSequence();
    StartCoroutine(RingBells(CurrentSequence));
  }

  public void Success() {
    CurrentBell.GetComponent<Bell>().Ring();

    CurrentSequence.Dequeue();
  }

  public void Fail() {
    // Play the failure sound.
    GetComponent<AudioSource>().Play();

    foreach (var bell in Bells) {
      bell.Shake();
    }

    NewSequence();
    StartCoroutine(RingBells(CurrentSequence, 1.5f));
  }

  public void NewSequence() => CurrentSequence = GenerateSequence(5);

  Queue<Bell> GenerateSequence(int length) {
    var sequence = new Queue<Bell>();

    for (int i = 0; i < length; i++) {
      int j = Random.Range(0, Bells.Length);
      sequence.Enqueue(Bells[j]);
    }

    return sequence;
  }

  IEnumerator RingBells(IEnumerable<Bell> bells, float delay = 0) {
    yield return new WaitForSeconds(delay);

    foreach (var bell in bells) {
      bell.Ring();
      yield return new WaitForSeconds(TimeInterval);
    }
  }
}
