using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public Module[] Modules;
    public Module StartModule;

    public int Iterations = 5;

    private void Start()
    {
        Module startingModule = Instantiate(StartModule, transform, false);
        List<Exit> pendingExits = new List<Exit>(startingModule.GetExits());
        
        for(int i = 0; i < Iterations; i++)
        {
            List<Exit> newExits = new List<Exit>();

            foreach (Exit exit in pendingExits)
            {
                string newTag = GetRandom(exit.Tags);
                var newModulePrefab = GetRandomWithTag(Modules, newTag);
                Module newModuleInstance = Instantiate(newModulePrefab, transform);
                Exit[] newModuleExits = newModuleInstance.GetExits();
                Exit exitToMatch = newModuleExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleExits);
                MatchExits(exit, exitToMatch);
                newExits.AddRange(newModuleExits.Where(e => e != exitToMatch));
            }
            pendingExits = newExits;
        }
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        var matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private static void MatchExits(Exit oldExit, Exit newExit)
    {
        Transform newModule = newExit.transform.parent;
        Vector3 forwardVectorToMatch = oldExit.backwardVector;
        float correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newExit.transform.forward);
        newModule.RotateAround(newExit.transform.position, Vector3.up, correctiveRotation);
        Vector3 correctivePosition = oldExit.transform.position - newExit.transform.position;
        newModule.transform.position += correctivePosition;
    }

    // Returns the signed angle this vector is rotated relative to global +Z Axis
    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
