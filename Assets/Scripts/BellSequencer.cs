using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BellSequencer : MonoBehaviour {
  /// <summary>
  /// The available bells to ring.
  /// </summary>
  public BellRinger[] Bells;
  /// <summary>
  /// The time interval between bell rings.
  /// </summary>
  public float TimeInterval = 0.75f;

  /// <summary>
  /// The current sequence of bells.
  /// </summary>
  public List<GameObject> CurrentSequence;
  /// <summary>
  /// The last bell in the sequence.
  /// </summary>
  public GameObject Last =>
    CurrentSequence[CurrentSequence.Count - 1].gameObject;

  void Start() {
    NewSequence();
    StartCoroutine(RingBells(CurrentSequence));
  }

  public void Success() {
    Last.GetComponent<BellRinger>().Ring();

    CurrentSequence.RemoveAt(CurrentSequence.Count - 1);
  }

  public void Fail() {
    // Play the failure sound.
    GetComponent<AudioSource>().Play();

    NewSequence();
    StartCoroutine(RingBells(CurrentSequence, 1.5f));
  }

  public void NewSequence() => CurrentSequence = GenerateSequence(5);

  List<GameObject> GenerateSequence(int length) {
    var sequence = new List<GameObject>();

    for (int i = 0; i < length; i++) {
      int j = Random.Range(0, Bells.Length);
      sequence.Add(Bells[j].gameObject);
    }

    return sequence;
  }

  IEnumerator RingBells(IEnumerable<GameObject> bells, float delay = 0) {
    yield return new WaitForSeconds(delay);

    foreach (var bell in bells) {
      bell.GetComponent<BellRinger>().Ring();
      yield return new WaitForSeconds(TimeInterval);
    }
  }
}
