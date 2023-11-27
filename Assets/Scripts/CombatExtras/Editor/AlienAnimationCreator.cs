using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.EditorGUILayout;

[CreateAssetMenu(fileName = "Alien Anims", menuName = "ScriptableObjects/Alien Animation Creator")]
public class AlienAnimationCreator : ScriptableObject
{
    //if you change the location of the generic animator/animations, put the new location here
    public const string animsFolder = "Assets/Animations/Aliens/";
    public const string genericAnimsFolder = animsFolder + "Generic (Use these as base)/";
    //the animator controller extension is always *.controller
    const string genericAnimatorName = "Generic Animator.controller";
    string genericAnimatorPath => genericAnimsFolder + genericAnimatorName;
    //the animation clip extension is always *.anim
    const string genericIdleAnimName = "Idle (Generic) (OVERRIDE THIS).anim";
    string genericIdleAnimPath => genericAnimsFolder + genericIdleAnimName;
    const string genericAttackAnimName = "Attack (Generic) (OVERRIDE THIS).anim";
    string genericAttackAnimPath => genericAnimsFolder + genericAttackAnimName;

    public string alienName;

    public bool useCustomAnimator;
    public RuntimeAnimatorController animator;

    public bool useCustomIdle;
    public Sprite[] idleSprites;
    public float idleAnimLength = 2;
    public AnimationClip idleAnim;

    public bool useCustomAttack;
    public Sprite attackSprite;
    public AnimationClip attackAnim;

    public void Generate()
    {
        if (string.IsNullOrEmpty(alienName))
        {
            EditorUtility.DisplayDialog("Invalid Settings", "Please enter a name for the alien.", "OK");
            return;
        }
        if (!useCustomIdle && (idleSprites == null || idleSprites.Length == 0))
        {
            EditorUtility.DisplayDialog("Invalid Settings", "Idle sprites missing. Either add an idle sprite or set \"Use Custom Idle\" to true.", "OK");
            return;
        }
        if (useCustomIdle && idleAnim == null)
        {
            EditorUtility.DisplayDialog("Invalid Settings", "Idle animation missing. Either add an idle animation or set \"Use Custom Idle\" to false. If no idle animation is desired, add an empty animation clip into \"Idle Anim\".", "OK");
            return;
        }
        if (!useCustomAttack && attackSprite == null)
        {
            EditorUtility.DisplayDialog("Invalid Settings", "Attack sprite missing. Either add an attack sprite or set \"Use Custom Attack\" to true.", "OK");
            return;
        }
        if (useCustomAttack && attackAnim == null)
        {
            EditorUtility.DisplayDialog("Invalid Settings", "Attack animation missing. Either add an attack animation or set \"Use Custom Attack\" to false. If no attack animation is desired, add an empty animation clip into \"Attack Anim\".", "OK");
            return;
        }

        if (!useCustomIdle)
        {
            //If you're checking here to try to automatically enable loop, don't bother. The scripting API doesn't support it.
            //The only 2 relevant properties are isLooping and wrapMode. isLooping is read-only and wrapMode is only for legacy clips.
            GenerateIdle();
            EditorUtility.DisplayDialog("Set Idle Loop", "Idle Animation generated. You'll need to manually set the animation to loop, as it defaults to not looping.", "OK");
        }
        if (!useCustomAttack)
        {
            GenerateAttack();
        }
        if (!useCustomAnimator)
        {
            GenerateAnimator();
            EditorUtility.DisplayDialog("Animator Generated", "Animator generated. If you're re-generating an existing alien's animations, you'll need to replace the animator in the alien's data SO.", "OK");
        }
        EditorUtility.SetDirty(this);
    }

    public void GenerateIdle()
    {
        //witness the beauty of the animation system's scripting API
        //I hate it, it took so much googling to figure out what to do, and there are still things I can't do from the script.
        //technically I can do these things by going directly into the file and changing it, but no.
        AnimationClip idleAnim = new AnimationClip();
        EditorCurveBinding spriteBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "Sprite",
            propertyName = "m_Sprite"
        };
        List<ObjectReferenceKeyframe> keys = new List<ObjectReferenceKeyframe>();
        if (idleAnimLength <= 0 || idleSprites.Length == 1)
            keys.Add(new ObjectReferenceKeyframe { time = 0, value = idleSprites[0] });
        else
        {
            int numSprites = idleSprites.Length;
            for (int i = 0; i < numSprites; i++)
            {
                keys.Add(new ObjectReferenceKeyframe { time = (float)i / numSprites * idleAnimLength, value = idleSprites[i] });
            }
            keys.Add(new ObjectReferenceKeyframe { time = idleAnimLength, value = idleSprites[0] });
        }
        AnimationUtility.SetObjectReferenceCurve(idleAnim, spriteBinding, keys.ToArray());
        AssetDatabase.CreateAsset(idleAnim, animsFolder + alienName + $"/Idle ({alienName}).anim");
        this.idleAnim = idleAnim;
    }

    public void GenerateAttack()
    {
        AnimationClip genericAttack = AssetDatabase.LoadAssetAtPath<AnimationClip>(genericAttackAnimPath);
        if(genericAttack == null)
        {
            EditorUtility.DisplayDialog("Missing Generic Attack Animation",
                "Unable to generate attack animation due to missing generic attack animation. " +
                "If generic attack animation file was moved/renamed, enter the new file path in AlienAnimationCreator.cs", "OK");
        }

        AnimationClip attackAnim = new AnimationClip();

        foreach (EditorCurveBinding curveBinding in AnimationUtility.GetCurveBindings(genericAttack))
        {
            AnimationCurve curve = AnimationUtility.GetEditorCurve(genericAttack, curveBinding);
            AnimationUtility.SetEditorCurve(attackAnim, curveBinding, curve);
        }

        EditorCurveBinding spriteBinding = new EditorCurveBinding
        {
            type = typeof(SpriteRenderer),
            path = "Sprite",
            propertyName = "m_Sprite"
        };
        ObjectReferenceKeyframe[] keys = { new ObjectReferenceKeyframe { time = 0, value = attackSprite } };
        AnimationUtility.SetObjectReferenceCurve(attackAnim, spriteBinding, keys);

        AssetDatabase.CreateAsset(attackAnim, animsFolder + alienName + $"/Attack ({alienName}).anim");
        this.attackAnim = attackAnim;
    }

    public void GenerateAnimator()
    {
        RuntimeAnimatorController genericAnimator = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(genericAnimatorPath);
        AnimationClip genericIdle = AssetDatabase.LoadAssetAtPath<AnimationClip>(genericIdleAnimPath);
        AnimationClip genericAttack = AssetDatabase.LoadAssetAtPath<AnimationClip>(genericAttackAnimPath);
        if (genericAnimator == null || genericIdle == null || genericAttack == null)
        {
            EditorUtility.DisplayDialog("Missing Generic Animations",
                "Unable to create animations due to missing generic animations. " +
                "The animator controller, idle animation, and attack animation are required to be in the same folder. " +
                "If the animations were moved or renamed, please enter the new path/names into Assets/Scripts/Combat/Editor/AlienAnimationCreator.cs", "OK");
            return;
        }

        AnimatorOverrideController animator = new AnimatorOverrideController(genericAnimator);

        List<KeyValuePair<AnimationClip, AnimationClip>> overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>
        {
            new KeyValuePair<AnimationClip, AnimationClip>(genericIdle, idleAnim),
            new KeyValuePair<AnimationClip, AnimationClip>(genericAttack, attackAnim)
        };
        animator.ApplyOverrides(overrides);
        AssetDatabase.CreateAsset(animator, animsFolder + alienName + $"/{alienName} Animator.overrideController");
        this.animator = animator;
    }
}

[CustomEditor(typeof(AlienAnimationCreator))]
public class AlienAnimationCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AlienAnimationCreator animCreator = (AlienAnimationCreator)target;

        PropertyField(serializedObject.FindProperty("alienName"));

        Space();

        PropertyField(serializedObject.FindProperty("useCustomAnimator"), new GUIContent("Use Custom Animator", "Animator will not be changed if this is set to true."));
        if(animCreator.useCustomAnimator)
            PropertyField(serializedObject.FindProperty("animator"));
        else
            DisplayReadOnly(serializedObject.FindProperty("animator"));

        Space();

        PropertyField(serializedObject.FindProperty("useCustomIdle"));
        SerializedProperty spriteProp = serializedObject.FindProperty("idleSprites");
        GUIContent spriteContent = new GUIContent(spriteProp.displayName, "This field is ignored when using custom idle animation.");
        SerializedProperty lengthProp = serializedObject.FindProperty("idleAnimLength");
        GUIContent lengthContent = new GUIContent(lengthProp.displayName, "The amount of time the idle animation lasts before looping.\nThis field is ignored when using custom idle animation.");
        SerializedProperty animProp = serializedObject.FindProperty("idleAnim");
        if(animCreator.useCustomIdle)
        {
            DisplayReadOnly(spriteProp, spriteContent);
            DisplayReadOnly(lengthProp, lengthContent);
            PropertyField(animProp);
        }
        else
        {
            PropertyField(spriteProp, spriteContent);
            PropertyField(lengthProp, lengthContent);
            DisplayReadOnly(animProp);
        }

        Space();

        PropertyField(serializedObject.FindProperty("useCustomAttack"));
        spriteProp = serializedObject.FindProperty("attackSprite");
        spriteContent = new GUIContent(spriteProp.displayName, "This field is ignored when using custom attack animation.");
        animProp = serializedObject.FindProperty("attackAnim");
        if (animCreator.useCustomAttack)
        {
            DisplayReadOnly(spriteProp, spriteContent);
            PropertyField(animProp);
        }
        else
        {
            PropertyField(spriteProp, spriteContent);
            DisplayReadOnly(animProp);
        }

        Space();

        if (animCreator.useCustomAnimator && animCreator.useCustomIdle && animCreator.useCustomAttack)
            GUI.enabled = false;
        bool generate = GUILayout.Button(new GUIContent("Generate Animations",
            "Creates a basic animation set based on the settings provided.\n" +
           $"Animations are generated in {AlienAnimationCreator.animsFolder + animCreator.alienName}."));
        GUI.enabled = true;
        if (generate)
        {
            animCreator.Generate();
            serializedObject.Update();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DisplayReadOnly(SerializedProperty property, GUIContent label = null)
    {
        if(label == null)
        {
            label = new GUIContent(property.displayName);
        }
        GUI.enabled = false;
        PropertyField(property, label);
        GUI.enabled = true;
    }
}