using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AddAudioEditor :Editor
{
    [MenuItem("HCG项目/添加Audio")]
    public static void AddAudio()
    {
        if (UnityEditor.Selection.activeGameObject == null)
        {
            var dir = GameObject.FindObjectOfType<PlayableDirector>();
            if (dir != null)
            {
                EditorUtility.DisplayDialog("选择Timeline", "未选择Timeline, 我替你随便选一个吧!", "好,请你吃饭");
                UnityEditor.Selection.activeGameObject = dir.gameObject;

            }
            else
            {
                EditorUtility.DisplayDialog("", "屏幕上并没有Timeline", "好,请你吃饭");

            }
            return;
        }

        var playableDirector = UnityEditor.Selection.activeGameObject.GetComponent<PlayableDirector>();
        if (playableDirector == null)
        {
            var dir = GameObject.FindObjectOfType<PlayableDirector>();
            if (dir != null)
            {
                EditorUtility.DisplayDialog("选择Timeline", "未选择Timeline, 我替你随便选一个吧!", "好,请你吃饭");
                UnityEditor.Selection.activeGameObject = dir.gameObject;

            }
            else
            {
                EditorUtility.DisplayDialog("", "屏幕上并没有Timeline", "好,请你吃饭");

            }
            return;
        }
        var playableAsset = playableDirector.playableAsset;
        if (playableAsset == null)
        {
            EditorUtility.DisplayDialog("", "这个里面没有timeline", "好,请你吃饭");
            return;
        }

        AudioSource source = GameObject.FindObjectOfType<AudioSource>();
        var oldBindings = playableAsset.outputs.ToArray();
        var timelineAsset = playableAsset as TimelineAsset;
        var audio = timelineAsset.CreateTrack<AudioTrack>(null, "test auodio");
        var audioOut = audio.outputs;
        playableDirector.SetGenericBinding(audio, source);
        var old = Selection.activeGameObject;
        Selection.activeGameObject = null;
        DelayUseAsync(old);

        Selection.activeGameObject = Camera.main.gameObject;
        foreach (var o in audioOut)
        {
            Debug.Log("o " + o.streamName);
        }
        AssetDatabase.SaveAssets();
        foreach (var b in oldBindings)
        {
            var obj = b.sourceObject as AnimationTrack;
            if (obj != null)
            {
                var clips = obj.GetClips();
                
                foreach(var c in clips)
                {
                    if (c.asset is AnimationPlayableAsset)
                    {
                    }
                }
            }
        }

        //var path = AssetDatabase.GetAssetPath(playableAsset);
        //if (string.IsNullOrEmpty(path))
        //    return;

        //string newPath = path.Replace(".", "(Clone).");
        //if (!AssetDatabase.CopyAsset(path, newPath))
        //{
        //    Debug.LogError("Couldn't Clone Asset");
        //    return;
        //}

        //var newPlayableAsset = AssetDatabase.LoadMainAssetAtPath(newPath) as PlayableAsset;
        //var gameObject = GameObject.Instantiate(UnityEditor.Selection.activeGameObject);
        //var newPlayableDirector = gameObject.GetComponent<PlayableDirector>();
        //newPlayableDirector.playableAsset = newPlayableAsset;

        //var oldBindings = playableAsset.outputs.ToArray();
        //var newBindings = newPlayableAsset.outputs.ToArray();

        //for (int i = 0; i < oldBindings.Length; i++)
        //{
        //    newPlayableDirector.SetGenericBinding(newBindings[i].sourceObject,
        //            playableDirector.GetGenericBinding(oldBindings[i].sourceObject)
        //        );
        //}
    }

    async static void DelayUseAsync(GameObject par)
    {
        await Task.Delay(10);
        Selection.activeGameObject = par;
    }
}
