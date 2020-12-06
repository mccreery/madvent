using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BellSequencer : MonoBehaviour {
  /// <summary>
  /// The bells to ring in a sequence.
  /// </summary>
  public Bell[] Bells;

  /// <summary>
  /// The bell in the sequence that must be clicked.
  /// </summary>
  public Bell RequiredBell => currentSequence.Peek();

  /// <summary>
  /// The time interval between successive bell rings.
  /// </summary>
  public float TimeInterval = 0.75f;

  /// <summary>
  /// The current sequence of bells.
  /// </summary>
  Queue<Bell> currentSequence;

  /// <summary>
  /// Whether the sequence is being displayed or not.
  /// </summary>
  bool displaying = false;

  void Start() {
    CreateNewSequence();
  }

  public void Success() {
    if (displaying) {
      return;
    }

    RequiredBell.GetComponent<Bell>().Ring();
    currentSequence.Dequeue();

    if (currentSequence.Count == 0) {
      GameObject.Find("Bell Scoreboard").GetComponent<Scoreboard>().Score++;
      CreateNewSequence();
    }
  }

  public void Fail() {
    if (displaying) {
      return;
    }

    // Play the failure sound.
    GetComponent<AudioSource>().Play();

    foreach (var bell in Bells) {
      bell.Shake();
    }

    CreateNewSequence(1.5f);
  }

  public void CreateNewSequence(float delay = 0) {
    displaying = true;

    currentSequence = GenerateSequence(5);
    StartCoroutine(RingBells(currentSequence, delay));
  }

  Queue<Bell> GenerateSequence(int length) {
    var sequence = new Queue<Bell>();

    for (int i = 0; i < length; i++) {
      int j = Random.Range(0, Bells.Length);
      sequence.Enqueue(Bells[j]);
    }

    return sequence;
  }

  IEnumerator RingBells(IEnumerable<Bell> bells, float delay) {
    yield return new WaitForSeconds(delay);

    foreach (var bell in bells) {
      bell.Ring();
      yield return new WaitForSeconds(TimeInterval);
    }

    displaying = false;
  }
}
