public class GameData
{
    public int gold;
    public int probablityLevel;
    public int speedLevel;

    public SerializableDictionary<string, int> inventory;
    public string[] currentFactory;
    public SerializableDictionary<string, bool> collectedItems;

    public SerializableDictionary<string, float> volumSettings;

    public GameData()
    {
        this.gold = 0;
        probablityLevel = 1;
        speedLevel = 1;

        inventory = new SerializableDictionary<string, int>();
        currentFactory = new string[5];
        collectedItems = new SerializableDictionary<string, bool>();

        volumSettings = new SerializableDictionary<string, float>();
    }
}