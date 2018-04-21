using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;

public class comms : MonoBehaviour {

	int deltaT = 0;

	public WebSocket w;

	IEnumerator Start() {
		w = new WebSocket(new Uri("ws://localhost:8765"));
		yield return StartCoroutine(w.Connect());
		Debug.Log("sending str");
		w.SendString("handshake successful");
		int i = 0;
		while (true) {
			string reply = w.RecvString();
			if (reply != null) {
				Debug.Log(reply);
				w.SendString("Recieved message");
			}

			yield return i;

    }
		w.Close();

  }

	public void Update() {
		deltaT++;
    //Send stop command after update happened 300 times
		if(deltaT > 300) {
			w.SendString("stop");
		}
	}
}
