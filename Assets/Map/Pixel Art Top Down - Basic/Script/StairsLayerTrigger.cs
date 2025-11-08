using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    // Improved stairs trigger to handle all directions (N, S, W, E)
    // and add slight tolerance for coordinate comparison

    public class StairsLayerTrigger : MonoBehaviour
    {
        public Direction direction;      // Direction of the stairs
        [Space]
        public string layerUpper;
        public string sortingLayerUpper;
        [Space]
        public string layerLower;
        public string sortingLayerLower;

        private const float tolerance = 0.1f;   // <-- 좌표 판정 오차 허용값

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 pos = other.transform.position;
            Vector2 triggerPos = transform.position;

            if (direction == Direction.South && pos.y < triggerPos.y + tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);

            else if (direction == Direction.North && pos.y > triggerPos.y - tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);

            else if (direction == Direction.West && pos.x < triggerPos.x + tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);

            else if (direction == Direction.East && pos.x > triggerPos.x - tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerUpper, sortingLayerUpper);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Vector2 pos = other.transform.position;
            Vector2 triggerPos = transform.position;

            if (direction == Direction.South && pos.y < triggerPos.y + tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);

            else if (direction == Direction.North && pos.y > triggerPos.y - tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);

            else if (direction == Direction.West && pos.x < triggerPos.x + tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);

            else if (direction == Direction.East && pos.x > triggerPos.x - tolerance)
                SetLayerAndSortingLayer(other.gameObject, layerLower, sortingLayerLower);
        }

        private void SetLayerAndSortingLayer(GameObject target, string layer, string sortingLayer)
        {
            target.layer = LayerMask.NameToLayer(layer);

            SpriteRenderer[] srs = target.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
                sr.sortingLayerName = sortingLayer;
        }

        public enum Direction
        {
            North,
            South,
            West,
            East
        }
    }
}
