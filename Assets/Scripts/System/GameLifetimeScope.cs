using CameraSystem;
using Player;
using Player.States;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GrenadesData _grenadesData;
    [SerializeField] private EnemiesData _enemiesData;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerStateController>();
        builder.RegisterComponentInHierarchy<CameraController>();
        builder.RegisterComponentInHierarchy<PlayerAmmo>();
        
        builder.RegisterInstance(_grenadesData);
        builder.RegisterInstance(_enemiesData);
    }
}
