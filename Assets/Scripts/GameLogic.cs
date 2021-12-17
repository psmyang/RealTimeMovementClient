using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameObject character;

    Vector2 characterPositionInPercent;
    Vector2 characterVelocityInPercent;

    void Start()
    {
        //Debug.Log(HalfCharacterSpeed);

        NetworkedClientProcessing.SetGameLogic(this);

        Sprite circleTexture = Resources.Load<Sprite>("Circle");

        character = new GameObject("Character");

        character.AddComponent<SpriteRenderer>();
        character.GetComponent<SpriteRenderer>().sprite = circleTexture;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.UpRight);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.UpLeft);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.DownRight);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.DownLeft);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.Right);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.Left);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.Up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.Down);
            }
            else
                NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.KeyboardInputUpdate + "," + KeyboardInputDirections.NoPress);
        }

        characterPositionInPercent += (characterVelocityInPercent * Time.deltaTime);

        Vector2 screenPos = new Vector2(characterPositionInPercent.x * (float)Screen.width, characterPositionInPercent.y * (float)Screen.height);
        Vector3 characterPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        characterPos.z = 0;
        character.transform.position = characterPos;

    }

    public void SetVelocityAndPosition(Vector2 vel, Vector2 pos)
    {
        characterVelocityInPercent = vel;
        characterPositionInPercent = pos;
    }

}


//We want to do input on the client and send ot server!
//
//update position on the server?! or on client??!?!?!?!?
//
//manage player position on server (collision detection!)
//
//
//
//is there any case for passing a location for the player to be at? 

//
//Add second charactrer
//
//
//
//so shouldnt the player handle its movement in case of connectivity issues?  and have the server correct?
//
///player accounts 
//game rooms or something something
//
//
//
//
//
//
//Debate if input signifier solution is ideal!!!
//