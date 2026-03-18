using UnityEngine;

public class ResetBestTime : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteKey("MejorTiempo");
        PlayerPrefs.Save();
        Debug.Log("Mejor tiempo reseteado!");
    }
}
