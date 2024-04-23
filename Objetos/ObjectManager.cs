using System.Collections.Generic;

public static class ObjectManager
{
    public static List<Objeto> Objetos { get; private set; }

    public static void AddGameObject(Objeto obj) => Objetos.Add(obj);

    public static void RemoveGameObject(Objeto obj) => Objetos.Remove(obj);
    
    public static void SetList(List<Objeto> list) => Objetos = list;

}