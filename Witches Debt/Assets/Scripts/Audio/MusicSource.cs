using Unity.VisualScripting;

public class MusicSource : AbstractAudioSource
{
    protected override void Start()
    {
        sourceType = AudioSourceType.Music;
        base.Start();
    }
}