using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour, IMovable, IDamage
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private SpriteRenderer _spritePlayer;
    [SerializeField] private ParticleSystem _stepsParticles;

    private PlayerStateMachine _playerStateMachine;
    private PlayerInput _input;
    private Health _health;

    public PlayerInput Input => _input;
    public PlayerConfig Config => _playerConfig;
    public PlayerView View => _playerView;
    public Transform Transform => transform;
    public ParticleSystem StepsParticles => _stepsParticles;
    public Health Health => _health;

    private void Awake()
    {
        _playerView.Initialize();
        _input = new PlayerInput();
        _playerStateMachine = new PlayerStateMachine(this);
        _health = new Health(100);
    }

    private void Update()
    {
        _playerStateMachine.Update();

        _playerStateMachine.HandleInput();
    }

    private void OnEnable() => _input.Enable();

    private void OnDisable() => _input.Disable();

    public GameObject SetSpawnerTower(SpawnerTower spawnerTower, Vector3 input)
    {
        GameObject towerSpawn = spawnerTower.SpawnTowerPosition(input);
        return towerSpawn;
    }

    public void ChangeThePlayersRotationX(bool rotationX) => _spritePlayer.flipX = rotationX;

    public void SetParticlesPosition(float offset)
        => _stepsParticles.transform.localPosition = new Vector3(offset, 0, 0);

    public void Damage(int damage)
    {   
        _health.TakeDamage(damage); 
    }
}