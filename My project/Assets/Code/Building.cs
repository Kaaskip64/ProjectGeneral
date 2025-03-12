/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Class that has to be attached to each building prefab
public class Building : MonoBehaviour, IHoverPanel
{
    public bool Placed; //bool for other scripts to check if building has been placed

    [Space(20)]

    [Header("Properties")]
    public BuildingType buildingType; //slot for scriptable object which holds properties
    public int capacityMax;
    public int queueCount;
    public BoundsInt area; //size of the placement area. ALWAYS keep the z value 1, or else it messes up the calculation
    public float maintenanceCost = 5f;

    [Space(20)]
    [HideInInspector]
    public int capacityCount;
    public SpriteRenderer image;
    public GameObject AstarCollider;

    public Vector2 mouseFollowOffset;

    public Transform NPCTarget;

    [HideInInspector]
    public Vector3 prevPos;

    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    private BuildingSystem buildingSystem;
    private StageBuilder stageBuilder;


    private void Start()
    {
        if(buildingType == BuildingType.Audience)
        {
            polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        } else
        {
            boxCollider = gameObject.GetComponent<BoxCollider2D>();

        }
        prevPos = Vector3.zero;
        buildingSystem = BuildingSystem.currentInstance;
        stageBuilder = StageBuilder.currentInstance;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }

        if (Placed && InformationPanel.instance.currentHoveredBuilding == this && buildingSystem.currentSelectedBuilding == null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var building = ShopSystem.instance.productDict[gameObject.name];
                PlayerProperties.Instance.MoneyChange(building.Price * buildingSystem.buildingRefundRate);

                switch(buildingType)
                {
                    case BuildingType.Audience:
                        buildingSystem.audienceAreas.Remove(this);
                        break;
                    case BuildingType.Food:
                        buildingSystem.foodStands.Remove(this);
                        break;
                    case BuildingType.Bathroom:
                        buildingSystem.bathroomStands.Remove(this);
                        break;
                    case BuildingType.Beer:
                        buildingSystem.beerStands.Remove(this);
                        break;
                    case BuildingType.Merch:
                        buildingSystem.merchStands.Remove(this);
                        break;
                }

                area.x = buildingSystem.gridLayout.WorldToCell(gameObject.transform.position).x + 1;
                area.y = buildingSystem.gridLayout.WorldToCell(gameObject.transform.position).y + 1;

                BuildingSystem.SetTilesBlock(area, TileType.White, buildingSystem.DecoTileMap);

                BuildingSystem.SetTilesBlock(area, TileType.White, buildingSystem.MainTileMap);


                area.x = 0;
                area.y = 0;

                Destroy(gameObject);
                AstarPath.active.data.gridGraph.Scan();

            }

            if (Input.GetMouseButtonDown(0))
            {

                if (buildingType == BuildingType.Audience)
                {
                    polygonCollider.enabled = false;
                    if (stageBuilder.StageUI.activeInHierarchy)
                    {
                        stageBuilder.StageUI.SetActive(false);
                    }
                }
                else
                {
                    boxCollider.enabled = false;
                    if (stageBuilder.StageUI.activeInHierarchy)
                    {
                        stageBuilder.StageUI.SetActive(false);
                        stageBuilder.MainUI.SetActive(true);
                        stageBuilder.currentActiveStageUI = null;
                    }
                }
                buildingSystem.pickingUpBuilding = true;

                buildingSystem.currentSelectedBuilding = this;
                Placed = false;
                if (buildingType == BuildingType.Deco)
                {
                    buildingSystem.DecoTileMap.gameObject.SetActive(true);
                }
                else
                {
                    buildingSystem.MainTileMap.gameObject.SetActive(true);

                }

                area.x = buildingSystem.gridLayout.WorldToCell(gameObject.transform.position).x + 1;
                area.y = buildingSystem.gridLayout.WorldToCell(gameObject.transform.position).y + 1;

                BuildingSystem.SetTilesBlock(area, TileType.White, buildingSystem.DecoTileMap);

                BuildingSystem.SetTilesBlock(area, TileType.White, buildingSystem.MainTileMap);


                area.x = 0;
                area.y = 0;

                if (buildingType == BuildingType.Deco)
                {
                    buildingSystem.FollowBuilding(area, buildingSystem.DecoTileMap);
                }
                else
                {
                    buildingSystem.FollowBuilding(area, buildingSystem.MainTileMap);
                }

                if (image != null)
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            }
        }
    }

    public bool CanBePlaced() //returns whether or not the building can be placed based on the current location on the grid
    {
        Vector3Int positionInt = buildingSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt + area.position;

        if (buildingType == BuildingType.Deco && buildingSystem.CanTakeArea(areaTemp, buildingSystem.DecoTileMap))
        {
            return true;
        } else if (buildingSystem.CanTakeArea(areaTemp, buildingSystem.DecoTileMap) && buildingSystem.CanTakeArea(areaTemp, buildingSystem.MainTileMap))
        {

            return true;
        }

        return false;
    }

    public void Place() //When Place() is called, places building
    {
        Vector3Int positionInt = buildingSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (buildingType == BuildingType.Audience)
        {
            polygonCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = true;
        }

        prevPos = gameObject.transform.position;

        Placed = true;

            buildingSystem.TakeArea(areaTemp, buildingSystem.DecoTileMap);
            buildingSystem.TakeArea(areaTemp, buildingSystem.MainTileMap);

    }


    public void MaintenanceTick()
    {
        PlayerProperties.Instance.MoneyChange(-maintenanceCost);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(-mouseFollowOffset, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InformationPanel.instance.ShowHoverPanel(this);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InformationPanel.instance.HideHoverPanel();
    }
}
*/