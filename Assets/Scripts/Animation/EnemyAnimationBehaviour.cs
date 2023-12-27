using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationBehaviour : MonoBehaviour
{
    enum FaceType
    {
        IDLE,
        ANGRY,
        HAPPY,
        HURT
    }

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SkinnedMeshRenderer[] _faceRenderers;


    private void SetFace(FaceType face)
    {
        foreach (SkinnedMeshRenderer meshRenderer in _faceRenderers)
        {
            meshRenderer.materials[1].SetInt("_Tile", (int)face);
        }
    }

    private void FaceGetHit()
    {
        SetFace(FaceType.HURT);
    }

    private void FaceNeutral()
    {
        SetFace(FaceType.IDLE);
    }

    private void FaceAngry()
    {
        SetFace(FaceType.ANGRY);
    }

}
