using Unity.VisualScripting;
using UnityEngine;

public class SpellCasterAudio : AbstractAudioSource
{
    // TODO: add dictionary <SpellType, AudioClip>
    [SerializeField] private AudioClip shot;
    private SpellCaster spellCaster;
    protected override void Start()
    {
        sourceType = AudioSourceType.SFX;
        base.Start();
        spellCaster = GetComponent<SpellCaster>();
        spellCaster.SpellCasted.AddListener(OnSpellCasted);
    }
    private void OnSpellCasted(SpellType spellType)
    {
        base.PlaySound(shot);
    }
}