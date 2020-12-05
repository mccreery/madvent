using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BellSequencer : MonoBehaviour {
  public HandbellRinger[] handbells;
  public float timeInterval = 0.75f;

  void Start() {
    var sequence = GenerateSequence(5);

    StartCoroutine(RingHandbells(sequence));
  }

  List<HandbellRinger> GenerateSequence(int length) {
    var sequence = new List<HandbellRinger>();

    for (int i = 0; i < length; i++) {
      int j = Random.Range(0, handbells.Length);
      sequence.Add(handbells[j]);
    }

    return sequence;
  }

  IEnumerator RingHandbells(IEnumerable<HandbellRinger> handbells) {
    foreach (var handbell in handbells) {
      handbell.Ring();
      yield return new WaitForSeconds(timeInterval);
    }
  }
}
