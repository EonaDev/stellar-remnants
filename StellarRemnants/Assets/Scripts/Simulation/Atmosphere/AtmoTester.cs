namespace StellarRemnants.Simulation.Atmosphere {
    using UnityEngine;
    using System.Collections.Generic;

    public class AtmoTester : MonoBehaviour {
        // AtmoVolume volume1 = new FiniteAtmoVolume(
        //     1f, 293f, 
        //     new KeyValuePair<ulong, float>(AtmoCompound.NITROGEN.Signature, 0.78f),
        //     new KeyValuePair<ulong, float>(AtmoCompound.OXYGEN.Signature, 0.21f),
        //     new KeyValuePair<ulong, float>(AtmoCompound.CARBON_DIOXIDE.Signature, 0.01f)
        // );

        // AtmoVolume volume2 = new FiniteAtmoVolume(
        //     1f, 200f, 
        //     new KeyValuePair<ulong, float>(AtmoCompound.NITROGEN.Signature, 0.78f/2),
        //     new KeyValuePair<ulong, float>(AtmoCompound.OXYGEN.Signature, 0.21f/2),
        //     new KeyValuePair<ulong, float>(AtmoCompound.CARBON_DIOXIDE.Signature, 0.01f/2)
        // );

        // AtmoVolume volume3 = new FiniteAtmoVolume(
        //     1f, 100f, 
        //     new KeyValuePair<ulong, float>(AtmoCompound.METHANE.Signature, 0.7f),
        //     new KeyValuePair<ulong, float>(AtmoCompound.AMMONIA.Signature, 0.3f)
        // );

        // AtmoVolume volume4 = new InfiniteAtmoVolume(
        //     100f, 
        //     new KeyValuePair<ulong, float>(AtmoCompound.HYDROGEN.Signature, 0.9f),
        //     new KeyValuePair<ulong, float>(AtmoCompound.HELIUM.Signature, 0.1f)
        // );

        // Threshold threshold1;
        // Threshold threshold2;

        // void Start() {
        //     threshold1 = new Threshold(volume1, volume2, 1f, 1f);
        //     threshold2 = new Threshold(volume1, volume3, 1f, 1f);
        // }

        // // Update is called once per frame
        // void Update() {
            
        // }

        // void FixedUpdate() {
        //     threshold1.AtmoUpdate();
        //     threshold2.AtmoUpdate();
        //     threshold1.VolumeA.ApplyChanges();
        //     threshold1.VolumeB.ApplyChanges();
        //     threshold2.VolumeA.ApplyChanges();
        //     threshold2.VolumeB.ApplyChanges();
        //     Debug.Log("FIRST: " + threshold1.VolumeA);
        //     Debug.Log("SECOND: " + threshold1.VolumeB);
        //     Debug.Log("THIRD: " + threshold2.VolumeB);

        //     // TODO: Compound signatures are added to volume signature even when door is closed. This should not happen.
        // }
    }
}