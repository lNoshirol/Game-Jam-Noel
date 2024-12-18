using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rig : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // On r�cup�re le skinned mesh renderer
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer != null)
        {
            // On parcourt les bones
            Transform[] bones = skinnedMeshRenderer.bones;

            foreach (Transform bone in bones)
            {
                // Afficher un gizmo sous forme de sph�re pour chaque bone
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(bone.position, 0.05f);
            }
        }
    }
}
