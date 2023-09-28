using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("老狼")]
    public Enemy enemy;
    [Header("老狼動畫")]
    public Animator aniEnemy;

    [Header("小黑動畫")]
    public Animator aniPlayer;
    [Header("小黑角色控制器")]
    public CharacterController characterPlayer;
    [Header("小黑第三人稱控制系統")]
    public ThirdPersonController controllerPlayer;

    private void OnTriggerEnter(Collider other)
    {
        print($"<color=#f69>碰到的物件：{other}</color>");

        if (other.name.Contains("過關區域")) Pass();
        if (other.name.Contains("老狼攻擊區域")) Lose();
    }

    private void Pass()
    {
        aniEnemy.SetBool("開關走路", false);
        enemy.enabled = false;
    }

    private void Lose()
    {
        aniPlayer.enabled = false;
        characterPlayer.enabled = false;
        controllerPlayer.enabled = false;
    }
}
