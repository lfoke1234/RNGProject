using UnityEngine;

public class SaveEntity : MonoBehaviour, ISerializationCallbackReceiver
{
    public void OnAfterDeserialize()
    {
        GetComponent<ISaveManager>();
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
