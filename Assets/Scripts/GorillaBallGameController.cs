using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GorillaBallGameController : GameController
{

    [SerializeField]
    CurrentGameType currentGameType;

    [SerializeField]
    GorillaGamePortalControl gorillaGamePortalControl;

    [SerializeField]
    List<GorillaHoopHandler> gorillaHoopHandlers;

    List<int> uniqueList;

    private void Awake()
    {
        uniqueList = new List<int>();
        for (int i=0; i < gorillaHoopHandlers.Count; i++)
        {
            uniqueList.Add(i);
        }
    }

    public override void InitializeGame()
    {
        currentGameType.SetGameTypeID(1);
        SetInitialPortals();


    }

    /// This is being called by every client, which is an issue. Need a better way to delay this
    public void SetInitialPortals()
    {
        List<int> threeUniqueIntegers = GetRandomUniqueIntegers(uniqueList, 3);

        Debug.Log("int1: " + threeUniqueIntegers[0] + " int2: " + threeUniqueIntegers[1] + " int3: " + threeUniqueIntegers[2]);
  
        gorillaGamePortalControl.SetPortal1ID(threeUniqueIntegers[0]);
        gorillaGamePortalControl.SetPortal2ID(threeUniqueIntegers[1]);
        gorillaGamePortalControl.SetPortal3ID(threeUniqueIntegers[2]);
    }

    public void ReportScore(int hoopID)
    {
        int portalId1 = gorillaGamePortalControl.GetPortal1ID();
        int portalId2 = gorillaGamePortalControl.GetPortal2ID();
        int portalId3 = gorillaGamePortalControl.GetPortal3ID();

        HashSet<int> exclusions = new HashSet<int> { portalId1, portalId2, portalId3 };

        if (portalId1 == hoopID)
        {
            gorillaGamePortalControl.SetPortal1ID(GetRandomIntExcluding(0, gorillaHoopHandlers.Count, exclusions));
        }

        if (portalId2 == hoopID)
        {
            gorillaGamePortalControl.SetPortal2ID(GetRandomIntExcluding(0, gorillaHoopHandlers.Count, exclusions));
        }

        if (portalId3 == hoopID)
        {
            gorillaGamePortalControl.SetPortal3ID(GetRandomIntExcluding(0, gorillaHoopHandlers.Count, exclusions));
        }
    }


    public static int GetRandomIntExcluding(int min, int max, HashSet<int> exclusions)
    {
        // Create a list of possible numbers within the range, excluding specified numbers
        List<int> availableNumbers = Enumerable.Range(min, max - min + 1)
                                               .Where(n => !exclusions.Contains(n))
                                               .ToList();

        if (availableNumbers.Count == 0)
        {
            throw new System.ArgumentException("No numbers available after exclusions.");
        }

        // Pick a random number from the available numbers using Unity's Random
        int randomIndex = UnityEngine.Random.Range(0, availableNumbers.Count);
        return availableNumbers[randomIndex];
    }


    List<int> GetRandomUniqueIntegers(List<int> list, int count)
    {
        // Shuffle the list and take the first 'count' elements
        return list.OrderBy(x => UnityEngine.Random.Range(0, list.Count)).Take(count).ToList();
    }

    public void UpdatePortalState(int lastId, int id)
    {
        Debug.Log("Gorilla lastId: " + lastId);
        Debug.Log("gorillaHoopHandlers Count: " + gorillaHoopHandlers.Count);
        if (lastId != -1) 
        {
            gorillaHoopHandlers[lastId].DisableHoop();
        }

        if (id != -1)
        {
            gorillaHoopHandlers[id].EnableHoop();
        }       
    }
}

public abstract class GameController : MonoBehaviour
{
    public abstract void InitializeGame();

}