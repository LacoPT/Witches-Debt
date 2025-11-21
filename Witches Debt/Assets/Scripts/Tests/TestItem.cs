using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TestItem : MonoBehaviour, IActor
{
    public TestItemModel Model { get; private set; }
    public UnityEvent<TestItem> ItemPicked;
    public void Initialize(IInstanceModel model)
    {
        Model = model as TestItemModel;
        transform.position = Model.Position;
    }

    public void Initialize()
    {
        if (Model is null)
        {
            Model = new();
            Model.Position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPicked?.Invoke(this);
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return null;
        Destroy(gameObject);

    }
}
