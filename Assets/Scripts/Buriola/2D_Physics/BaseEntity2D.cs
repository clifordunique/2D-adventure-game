﻿using System;
using UnityEngine;

namespace Buriola._2D_Physics
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class BaseEntity2D : MonoBehaviour
    {
        protected const float SKIN_WIDTH = .015f;

        private BoxCollider2D _collider;
        protected RaycastOrigins2D _raycastOrigins;
        public CollisionInfo2D CollisionInfo { get; protected set; }
        
        [SerializeField] protected LayerMask _collisionMask = default;
        [SerializeField, Range(2, 10)] protected int _horizontalRayCount = 4;
        [SerializeField, Range(2, 10)] protected int _verticalRayCount = 4;
        
        protected float _horizontalRaySpacing;
        protected float _verticalRaySpacing;
        
        public float DeltaTime { get; private set; }
        public float FixedDeltaTime { get; private set; }

        protected virtual void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            CalculateRaySpacing();
        }

        protected virtual void Update()
        {
            DeltaTime = Time.deltaTime;
        }

        protected virtual void FixedUpdate()
        {
            FixedDeltaTime = Time.fixedDeltaTime;
        }

        protected void UpdateRaycastOrigins()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(SKIN_WIDTH * -2);
            
            _raycastOrigins.BottomLeft    = new Vector2(bounds.min.x, bounds.min.y);
            _raycastOrigins.BottomRight   = new Vector2(bounds.max.x, bounds.min.y);
            _raycastOrigins.TopLeft       = new Vector2(bounds.min.x, bounds.max.y);
            _raycastOrigins.TopRight      = new Vector2(bounds.max.x, bounds.max.y);
        }

        private void CalculateRaySpacing()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(SKIN_WIDTH * -2);

            _horizontalRaySpacing = bounds.size.y / (_horizontalRayCount - 1);
            _verticalRaySpacing = bounds.size.x / (_verticalRayCount - 1);
        }
    }
}