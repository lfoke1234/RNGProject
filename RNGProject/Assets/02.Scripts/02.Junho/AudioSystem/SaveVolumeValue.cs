using System.Collections.Generic;
using UnityEngine;

public class SaveVolumeValue : MonoBehaviour, ISaveManager
{
    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumSettings)
        {
            foreach (UI_VolumeSlider item in volumeSettings)
            {
                if (item.parameter == pair.Key)
                    item.LoadSlider(pair.Value);
            }
        }

    }

    public void SaveData(ref GameData _data)
    {
        _data.volumSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumSettings.Add(item.parameter, item.slider.value);
        }
    }
}
