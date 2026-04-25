using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Components.Tags;
using Engine.Core.Enums;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.EntitySystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;

namespace Game.Core.Systems.Npc;

public class EnemySpawnSystem : ISystem
{
    private readonly ComponentManager _componentManager;
    private readonly EntityManager _entityManager;

    private ComponentPool<Transform> _transformPool;
    private ComponentPool<Spawner> _spawnerPool;
    
    public readonly List<Entity> Enemies = new();
    
    private int _player;
    
    public EnemySpawnSystem(ComponentManager componentManager, EntityManager entityManager)
    {
        _componentManager = componentManager;
        _entityManager = entityManager;
    }

    public void Initialize()
    {
        _player = _componentManager.GetPool<PlayerTag>().GetIds().First();
        _transformPool = _componentManager.GetPool<Transform>();
        _spawnerPool = _componentManager.GetPool<Spawner>();
    }

    // texture is null (Enemy spawning)
    public void Update(float deltaTime)
    {
        ref var playerSpawner = ref _spawnerPool.Get(_player);
        ref var playerTransform = ref _transformPool.Get(_player);

        if (playerSpawner.MaxSpawns <= 0)
        {
            return;
        }
        
        var playerPosX = (int)playerTransform.Position.X;
        var playerPosY = (int)playerTransform.Position.Y;
        
        var randomXPos = new Random().Next(playerPosX - playerSpawner.Radius, playerPosX + playerSpawner.Radius);
        var randomYPos = new Random().Next(playerPosY - playerSpawner.Radius, playerPosY + playerSpawner.Radius);
        
        var randomEnemyPos = new Vector2(randomXPos, randomYPos);
        var randomEntityType = (EntityType)new Random().Next((int)EntityType.Melee, ((int)EntityType.Mage + 1));

        var enemy = _entityManager.CreateEnemy(randomEntityType, randomEnemyPos);
        Enemies.Add(enemy);
        
        playerSpawner.SpawnCount++;
        playerSpawner.MaxSpawns--;
        
        Debug.WriteLine($"Spawn Count: {Enemies.Count}");
    }
}