using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSixe;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform[] spawnPoints;

    private List<GameObject> handCards = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        if (handCards.Count >= maxHandSixe)
        {
            return;
        }
        GameObject g = Instantiate(cardPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        handCards.Add(g);
        UpdateCardPositions();
    }

    private void UpdateCardPositions()
    {
        if (handCards.Count == 0)
        {
            return;
        }

        float cardSpacing = 1f/maxHandSixe;
        float fisrtCarsdPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
           float p = fisrtCarsdPosition + i * cardSpacing;
           Vector3 splinePosition = spline.EvaluatePosition(p);
           Vector3 foward = spline.EvaluateTangent(p);
           Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, foward).normalized);
           handCards[i].transform.DOMove(splinePosition, 0.25f);
           handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }
}
