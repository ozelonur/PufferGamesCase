namespace _GAME_.Scripts.GlobalVariables
{
    public static class CustomEvents
    {
        public const string OnGameStart = nameof(OnGameStart);
        public const string OnGameFailed = nameof(OnGameFailed);
        
        public const string SetPlayerTransform = nameof(SetPlayerTransform);
        public const string GetHit = nameof(GetHit);
        public const string Shot = nameof(Shot);
        public const string VisionRangeVisibility = nameof(VisionRangeVisibility);
        public const string Reload = nameof(Reload);
        public const string GenerateMagazine = nameof(GenerateMagazine);
        public const string DropMagazine = nameof(DropMagazine);
        public const string InsertMagazine = nameof(InsertMagazine);
        public const string Dash = nameof(Dash);
        
        public const string WeaponPassTheLeftHand = nameof(WeaponPassTheLeftHand);
        public const string WeaponPassTheRightHand = nameof(WeaponPassTheRightHand);
        public const string ThrowGrenade = nameof(ThrowGrenade);
        public const string SpawnGrenade = nameof(SpawnGrenade);
        public const string ThrowGrenadeToTarget = nameof(ThrowGrenadeToTarget);
        public const string AbortThrowingGrenade = nameof(AbortThrowingGrenade);
        public const string ShakeOnGrenadeExplode = nameof(ShakeOnGrenadeExplode);

        public const string ThrowOilBomb = nameof(ThrowOilBomb);
        public const string SpawnOilBomb = nameof(SpawnOilBomb);
        public const string ThrowOilBombToTarget = nameof(ThrowOilBombToTarget);
        public const string AbortThrowingOilBomb = nameof(AbortThrowingOilBomb);
        
        public const string EnableShotgun = nameof(EnableShotgun);
        public const string FireShotGun = nameof(FireShotGun);
        public const string DisableShotGun = nameof(DisableShotGun);
        public const string DisableShotGunRange = nameof(DisableShotGunRange);
        public const string ShakeCameraToShotGun = nameof(ShakeCameraToShotGun);

        public const string EnemyDead = nameof(EnemyDead);

        public const string StartCoolDown = nameof(StartCoolDown);
    }
}