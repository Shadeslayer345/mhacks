using UnityEngine;
using System.Collections;
using BladeCast;

public class Player_Behavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitControllerListeners();
	}

	// Update is called once per frame
	void Update () {

	}

    void InitControllerListeners() {
        BCMessenger.Instance.RegisterListener("connect", 0, this.gameObject, "HandleControllerRegister");
        BCMessenger.Instance.RegisterListener("direction", 0, this.gameObject, "HandleRotate_ControllerInput");
    }

    void HandleControllerRegister() {
        print("Connected to controller");
    }

    void HandleRotate_ControllerInput(ControllerMessage msg) {
        if (msg.Payload.HasField("direction")) {
            string direction_value_raw = msg.Payload.GetField("direction").ToString();
            if (direction_value_raw)) {
                print(direction_value_raw);
            }
        } else {
            print("direction field not present");
        }
    }
}
