using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAnimManager : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        Invoke(nameof(PlayAnimator), Random.Range(0f, 4f));
    }

    private void PlayAnimator()
    {
        if(_animator!=null)
        {
            _animator.enabled = true;
            _animator.Play(0);
        }
            
    }
}
