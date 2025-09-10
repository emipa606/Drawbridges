using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace Drawbridges;

[HotSwappable]
public class Drawbridge : Building
{
    private Dictionary<IntVec3, TerrainDef> oldTerrains = new();

    private Graphic overlayGraphic; // Cache field for the overlay graphic
    private bool raised;

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        if (raised || respawningAfterLoad)
        {
            return;
        }

        foreach (var cell in this.OccupiedRect())
        {
            oldTerrains[cell] = cell.GetTerrain(map);
            map.terrainGrid.SetTerrain(cell, TerrainDefOf.Concrete);
        }
    }

    protected override void DrawAt(Vector3 drawLoc, bool flip = false)
    {
        // Draw the base graphic
        Graphic.Draw(drawLoc, Rotation, this);

        // Draw the overlay graphic if the drawbridge is not raised
        if (raised)
        {
            return;
        }

        // If overlayGraphic is not yet initialized, load it
        if (overlayGraphic == null)
        {
            var basePath = def.graphicData.texPath.Replace("_ground", "");
            var overlayPath = $"{basePath}_overlay";
            overlayGraphic = GraphicDatabase.Get<Graphic_Multi>(overlayPath, Graphic.Shader,
                def.graphicData.drawSize, DrawColor, DrawColorTwo);
        }

        // Draw the cached overlay graphic
        overlayGraphic.Draw(drawLoc + new Vector3(0, 5, 0), Rotation, this);
    }

    public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
    {
        RecoverTerrain();
        base.Destroy(mode);
    }

    private void SpawnBridge(Drawbridge newBridge)
    {
        var wasSelected = Find.Selector.IsSelected(this);
        newBridge.HitPoints = HitPoints;
        newBridge.SetFaction(Faction);
        if (newBridge.raised)
        {
            RecoverTerrain();
        }

        var pos = Position;
        var map = Map;
        Destroy();
        GenSpawn.Spawn(newBridge, pos, map, Rotation);
        if (wasSelected)
        {
            Find.Selector.Select(newBridge);
        }
    }

    private void RecoverTerrain()
    {
        //Log.Message($"{oldTerrains} - {oldTerrains.Select(x => $"{x.Key} - {x.Value}").ToStringSafeEnumerable()}");
        if (!oldTerrains.Any())
        {
            return;
        }

        foreach (var cell in this.OccupiedRect())
        {
            Map.terrainGrid.SetTerrain(cell, oldTerrains[cell]);
        }
    }

    public override IEnumerable<Gizmo> GetGizmos()
    {
        foreach (var gizmo in base.GetGizmos())
        {
            yield return gizmo;
        }

        switch (raised)
        {
            case false:
            {
                var openButton = new Command_Action
                {
                    defaultLabel = "Raise".Translate(),
                    defaultDesc = "RaiseDesc".Translate(),
                    icon = ContentFinder<Texture2D>.Get("Drawbridges/Raise"),
                    action = delegate
                    {
                        if (ThingMaker.MakeThing(ThingDef.Named($"{def.defName}Raised")) is not Drawbridge newBridge)
                        {
                            return;
                        }

                        newBridge.raised = true;
                        SpawnBridge(newBridge);
                    }
                };
                yield return openButton;
                break;
            }
            case true:
            {
                var closeButton = new Command_Action
                {
                    defaultLabel = "Lower".Translate(),
                    defaultDesc = "LowerDesc".Translate(),
                    icon = ContentFinder<Texture2D>.Get("Drawbridges/Lower"),
                    action = delegate
                    {
                        if (ThingMaker.MakeThing(ThingDef.Named(def.defName.Replace("Raised", ""))) is not Drawbridge
                            lowered)
                        {
                            return;
                        }

                        lowered.raised = false;
                        SpawnBridge(lowered);
                    }
                };
                yield return closeButton;
                break;
            }
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref raised, "raised");
        Scribe_Collections.Look(ref oldTerrains, "oldTerrains", LookMode.Value, LookMode.Def);
        if (Scribe.mode == LoadSaveMode.PostLoadInit)
        {
            oldTerrains ??= new Dictionary<IntVec3, TerrainDef>();
        }
    }
}