using TMPro;
using UnityEngine;
using Utilities;

public class RunDistanceController : MonoBehaviour
{
    #region Properties
    public static int CurrentRunDistance { get; private set; }
    public static int BestRunDistance { get; private set; }
    #endregion

    #region Fields
    [SerializeField] TextMeshPro bestRunDistanceTMP;
    #endregion

    #region Methods
    void Awake()
    {
        BestRunDistance = FileManager.DoesTheFileExist(nameof(BestRunDistance)) ?
            int.Parse(FileManager.LoadStringFromFile(nameof(BestRunDistance))) : 0;
        TryToOutputBestRunDistance(bestRunDistanceTMP);
    }
    public void TryToOutputBestRunDistance(TextMeshPro bestRunDistanceTMP)
    {
        if (BestRunDistance > 0)
        {
            bestRunDistanceTMP.gameObject.SetActive(true);
            bestRunDistanceTMP.text = BestRunDistance.ToString();
        }
    }

    public void SaveCurrentPlayerRunDistance(Transform player)
    {
        CurrentRunDistance = (int)Mathf.Ceil(player.position.z);
        if (BestRunDistance < CurrentRunDistance)
            BestRunDistance = CurrentRunDistance;
        FileManager.SaveStringToFile(BestRunDistance.ToString(), nameof(BestRunDistance));
    }
    #endregion
}