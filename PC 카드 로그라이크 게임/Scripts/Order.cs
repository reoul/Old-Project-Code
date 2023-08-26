﻿using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Renderer[] backRenderers;
    [SerializeField] private Renderer[] middleRenderers;
    [SerializeField] private string sortingLayerName;
    private int originOrder;

    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 3800 : originOrder);
    }

    public void SetOrder(int order)
    {
        foreach (var renderer in backRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = order;
        }
        foreach (var renderer in middleRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = order + 1;
        }
    }
}
