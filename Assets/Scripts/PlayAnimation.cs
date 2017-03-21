using UnityEngine;
using UnityEngine.Experimental.Director;

[RequireComponent(typeof(Animator))]
public class PlayAnimation : MonoBehaviour
{
    public AnimationClip clip;

    void Start()
    {
        // Wrap the clip in a playable
        var clipPlayable = AnimationClipPlayable.Create(clip);

        // Bind the playable to the player
        GetComponent<Animator>().Play(clipPlayable);
    }

}
