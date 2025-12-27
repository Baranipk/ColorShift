using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeathState : IplayerState
{
    PlayerController controller;

    public PlayerDeathState(PlayerController controller)
    {
        this.controller = controller;
    }

    public async void Enter()
    {
        Debug.Log("Player Death");
        controller.GetComponent<PlayerAnimation>().Death();
        controller.GetComponent<PlayerInputHandler>().DeactivateInput();
        controller.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        await UniTask.Delay(3000);        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit(){}

    public void FixedUpdate(){}

    public void Update(){}
}
