using System.Numerics;

[System.Serializable]

public class SaveData
{
    public string characterId;
    public int scene;
    public int life;
    public Vector2 playerPosition;
    public int arrowAmount;
    public int bombAmount;
    public bool hasKey; //No se si es un int o un bool
    public bool hasEspada; 
    public bool hasArco; 
    public string[] inventory; //Tampoco sé el tipo de variable que es el inventario
}

