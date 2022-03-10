using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum MeshType {Primative, Prefab} 
public class MeshBuilder : MonoBehaviour
{
    
    private const string BuilderMenuName = "Builder Options";
    
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    public Vector3Int PieceAmount;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    public Vector3 PieceSize;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    public Vector3 PieceDistance;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    public MeshType CurrentMeshType = MeshType.Primative;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    [HideIf("@CurrentMeshType == MeshType.Primative")]
    public GameObject ObjectMesh;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    [HideIf("@CurrentMeshType == MeshType.Prefab")]
    public PrimitiveType ObjectType;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    public bool PutMaterial = true;
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    [HideIf("@PutMaterial == false")]
    public Material WallMaterial;
    
    [FoldoutGroup(BuilderMenuName)]
    [OnValueChanged("CreateWall")]
    public Vector3 offsetmenu;


    
    [FoldoutGroup(BuilderMenuName)]
    [Button]
    public void CreateWall() {
        DeleteWall();
        if(!CanBuild()) return;
        for (int x = 0; x < PieceAmount.x; x++) {
            for (int y = 0; y < PieceAmount.y; y++) {
                for (int z = 0; z < PieceAmount.z; z++) {
                    if (x > 0 && x < PieceAmount.x - 1 && 
                        y > 0 && y < PieceAmount.y - 1 && 
                        z > 0 && z < PieceAmount.z - 1) 
                        continue;
                    
                    if (CurrentMeshType == MeshType.Prefab) {
                        GameObject obj = Instantiate(ObjectMesh);
                        obj.transform.position = GetWorldPosition(new Vector3Int(x, y, z));
                        obj.transform.localScale = PieceSize;
                        obj.transform.SetParent(transform);
                        obj.name = $"{ObjectType}_{x}_{y}_{z}";
                        if(PutMaterial)
                            obj.GetComponent<MeshRenderer>().material = WallMaterial;
                    }
                    else {
                        GameObject obj = GameObject.CreatePrimitive(ObjectType);
                        obj.transform.position = GetWorldPosition(new Vector3Int(x, y, z));
                        obj.transform.localScale = PieceSize;
                        obj.transform.SetParent(transform);
                        obj.name = $"{ObjectType}_{x}_{y}_{z}";
                        if(PutMaterial)
                            obj.GetComponent<MeshRenderer>().material = WallMaterial;
                    }
                }
            }
        }
    }

    [FoldoutGroup(BuilderMenuName)]
    [Button]
    public void DeleteWall() {
        DestroyAllChildren(transform);
    }
    
    bool CanBuild() {
        if (CurrentMeshType == MeshType.Primative) {
            if (PieceAmount.x <= 0 && PieceAmount.y <= 0 && PieceAmount.z <= 0) return false;
        }
        else {
            if (ObjectMesh == null) return false;
            if (PieceAmount.x <= 0 && PieceAmount.y <= 0 && PieceAmount.z <= 0) return false;
        }
        return true;
    }
    
    Vector3 GetWorldPosition(Vector3Int vector3Int) {
        float x, y, z;
        x = vector3Int.x * PieceSize.x * (PieceDistance.x);
        y = vector3Int.y * PieceSize.y * (PieceDistance.y);
        z = vector3Int.z * PieceSize.z * (PieceDistance.z);
        
        return new Vector3(x, y, z) + transform.position - (offsetmenu);
    }
    
    private void DestroyAllChildren(Transform transform) {
        if (transform.childCount <= 0) return;
        for (int i = transform.childCount - 1; i >= 0; i--) {
                #if UNITY_EDITOR
            if(!Application.isPlaying)
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
                #endif
            if(Application.isPlaying)
                GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    
    
}
