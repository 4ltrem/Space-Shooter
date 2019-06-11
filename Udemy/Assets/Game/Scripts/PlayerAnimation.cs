using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.logWarnings = false;
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.isPlayerOne == true)
        {
            //if a key is pressed down or left key is down
            if (Input.GetKeyDown(KeyCode.A))
            {
                _anim.SetBool("Turn_Left", true);
                _anim.SetBool("Turn_Right", false);
            }
            //if w key is lifted up or up arrow is up
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _anim.SetBool("Turn_Right", true);
                _anim.SetBool("Turn_Left", false);

            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _anim.SetBool("Turn_Right", false);
                _anim.SetBool("Turn_Left", false);
            }
        }
        else
        {
            //if a key is pressed down or left key is down
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _anim.SetBool("Turn_Left", true);
                _anim.SetBool("Turn_Right", false);
            }
            //if w key is lifted up or up arrow is up
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _anim.SetBool("Turn_Right", true);
                _anim.SetBool("Turn_Left", false);

            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _anim.SetBool("Turn_Right", false);
                _anim.SetBool("Turn_Left", false);

            }
        }
    }
}
