using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private float _energy;
    [SerializeField] private float _satiety;
    [SerializeField] private float _walkSpeed;

    private Pickaxe _pickaxe;
    public float Energy => _energy;
    public float Satiety => _satiety;

    public float WalkSpeed => _walkSpeed;

    private void OnEnable()
    {
        _pickaxe = GetComponentInChildren<Pickaxe>();
    }
    private void Start()
    {
        StartCoroutine(JustLiving());
        _pickaxe.gameObject.SetActive(false);

    }

    private IEnumerator JustLiving()
    {
        while (true) 
        {
            _energy -= Random.Range(0f, 0.05f);
            _satiety -= Random.Range(0f, 0.08f);
            yield return new WaitForSeconds(1f);
        }
    }

    public void TookSleep(Bed bed)
    {
        _energy += bed.Energy;
    }

    public void HadLunch(AppleTree tree)
    {
        _satiety += tree.Satiety;
    }

    public void GetPickaxe()
    {
        _pickaxe.gameObject.SetActive(true);
    }

    public void HidePickaxe()
    {
        _pickaxe.gameObject.SetActive(false);
    }


}
