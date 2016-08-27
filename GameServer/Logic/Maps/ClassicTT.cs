using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Core.Logic.RAF;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LeagueSandbox.GameServer.Logic.Maps
{
    class ClassicTT : Map
    {
        private List<List<Vector2>> _laneWaypoints = new List<List<Vector2>>
        {
            new List<Vector2>
            { // blue top
              new Vector2(988.4354f, 6820.1563f),
              new Vector2(1057.6921f, 7515.0127f),
              new Vector2(1162.3730f, 8255.5400f),
              new Vector2(1420.0624f, 8957.0381f),
              new Vector2(2031.9426f, 9545.1055f),
              new Vector2(2786.4866f, 9478.0664f),
              new Vector2(3603.4863f, 8769.7578f),
              new Vector2(4128.1831f, 7832.1460f),
              new Vector2(4543.0825f, 7366.0342f),
              new Vector2(5163.9004f, 7022.2563f),
              new Vector2(5827.2158f, 6883.7837f),
              new Vector2(6856.8760f, 6690.4360f),
              new Vector2(7858.0664f, 6926.5352f),
              new Vector2(8570.2002f, 7216.6108f),
              new Vector2(9305.2910f, 7800.1357f),
              new Vector2(9754.6670f, 8442.5703f),
              new Vector2(9986.9092f, 8967.7598f),
              new Vector2(10614.6367f, 9554.8301f),
              new Vector2(11395.4004f, 9539.2598f),
              new Vector2(12026.6650f, 8948.8262f),
              new Vector2(12166.6982f, 8421.3076f),
              new Vector2(12170.5879f, 7815.3389f),
              new Vector2(11936.3457f, 7074.7109f)
           },
           new List<Vector2>
           { // blue bot
             new Vector2(951.9963f, 5746.0674f),
             new Vector2(1159.9453f, 5312.1895f),
             new Vector2(1180.5271f, 4597.7148f),
             new Vector2(1430.6753f, 3789.5020f),
             new Vector2(2357.4102f, 3406.8762f),
             new Vector2(3905.0620f, 3338.3281f),
             new Vector2(4612.4170f, 3344.8848f),
             new Vector2(5370.1396f, 3322.1323f),
             new Vector2(6625.6914f, 3400.7454f),
             new Vector2(7788.2993f, 3368.9165f),
             new Vector2(9342.8662f, 3354.5730f),
             new Vector2(10481.5244f, 3368.3059f),
             new Vector2(11348.6963f, 3345.5156f),
             new Vector2(12288.2949f, 4108.7725f),
             new Vector2(12115.8164f, 5031.8457f),
             new Vector2(12050.5557f, 5747.0601f)
           },
                     new List<Vector2>
           { // red top
              new Vector2(11936.3457f, 7074.7109f),
              new Vector2(12170.5879f, 7815.3389f),
              new Vector2(12166.6982f, 8421.3076f),
              new Vector2(12026.6650f, 8948.8262f),
              new Vector2(11395.4004f, 9539.2598f),
              new Vector2(10614.6367f, 9554.8301f),
              new Vector2(9986.9092f, 8967.7598f),
              new Vector2(9754.6670f, 8442.5703f),
              new Vector2(9305.2910f, 7800.1357f),
              new Vector2(8570.2002f, 7216.6108f),
              new Vector2(7858.0664f, 6926.5352f),
              new Vector2(6856.8760f, 6690.4360f),
              new Vector2(5827.2158f, 6883.7837f),
              new Vector2(5163.9004f, 7022.2563f),
              new Vector2(4543.0825f, 7366.0342f),
              new Vector2(4128.1831f, 7832.1460f),
              new Vector2(3603.4863f, 8769.7578f),
              new Vector2(2786.4866f, 9478.0664f),
              new Vector2(2031.9426f, 9545.1055f),
              new Vector2(1420.0624f, 8957.0381f),
              new Vector2(1162.3730f, 8255.5400f),
              new Vector2(1057.6921f, 7515.0127f),
              new Vector2(988.4354f, 6820.1563f)
           },
           new List<Vector2>
           { // red bot
              new Vector2(12050.5557f, 5747.0601f),
              new Vector2(12115.8164f, 5031.8457f),
              new Vector2(12288.2949f, 4108.7725f),
              new Vector2(11348.6963f, 3345.5156f),
              new Vector2(10481.5244f, 3368.3059f),
              new Vector2(9342.8662f, 3354.5730f),
              new Vector2(7788.2993f, 3368.9165f),
              new Vector2(6625.6914f, 3400.7454f),
              new Vector2(5370.1396f, 3322.1323f),
              new Vector2(4612.4170f, 3344.8848f),
              new Vector2(3905.0620f, 3338.3281f),
              new Vector2(2357.4102f, 3406.8762f),
              new Vector2(1430.6753f, 3789.5020f),
              new Vector2(1180.5271f, 4597.7148f),
              new Vector2(1159.9453f, 5312.1895f),
              new Vector2(11936.3457f, 7074.7109f)
           },
        };

        private Dictionary<TeamId, float[]> _endGameCameraPosition = new Dictionary<TeamId, float[]>
        {
            { TeamId.TEAM_BLUE, new float[] { 1810.5286f, 6016.3965f, 188 } },
            { TeamId.TEAM_PURPLE, new float[] { 10874.4785f, 6016.8369f, 110 } }
        };

        public ClassicTT(Game game) : base(game, /*90*/5 * 1000, 30 * 1000, 90 * 1000, true, 1)
        {            
            if (!File.Exists("./MapData/Map4/Aipath.aimesh"))
            {
                Logger.LogCoreError("Failed to load TwistedTreeline data.");
                return;
            }
            byte[] aiPath = File.ReadAllBytes("./MapData/Map4/Aipath.aimesh");
            mesh = new RAF.AIMesh(aiPath);
            _collisionHandler.init(3); // Needs to be initialised after AIMesh

            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_OrderTurretShrine_A", -392.6644f, 6339.0498f, TeamId.TEAM_BLUE, TurretType.FountainTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T1_C_01_A", 1529.3630f, 6297.5181f, TeamId.TEAM_BLUE, TurretType.NexusTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T1_C_06_A", 1116.1393f, 8606.3184f, TeamId.TEAM_BLUE, TurretType.InhibitorTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T1_C_07_A", 1069.0569f, 3958.3298f, TeamId.TEAM_BLUE, TurretType.InhibitorTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T1_L_02_A", 3923.3262f, 8123.9878f, TeamId.TEAM_BLUE, TurretType.InnerTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T1_R_02_A", 3291.2434f, 3351.9097f, TeamId.TEAM_BLUE, TurretType.InnerTurret));

            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_ChaosTurretShrine_A", 13717.4023f, 6371.9492f, TeamId.TEAM_PURPLE, TurretType.FountainTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T2_C_01_A", 11727.5859f, 6265.4585f, TeamId.TEAM_PURPLE, TurretType.NexusTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T2_L_01_A", 12309.2529f, 8549.1895f, TeamId.TEAM_PURPLE, TurretType.InhibitorTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T2_R_01_A", 12332.4590f, 4017.7246f, TeamId.TEAM_PURPLE, TurretType.InhibitorTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T2_R_02_A", 10056.0459f, 3377.0791f, TeamId.TEAM_PURPLE, TurretType.InnerTurret));
            AddObject(new Turret(game, game.GetNewNetID(), "@Turret_T2_L_02_A", 9539.0908f, 8212.6357f, TeamId.TEAM_PURPLE, TurretType.InnerTurret));


            //TODO
            var COLLISION_RADIUS = 0;
            var SIGHT_RANGE = 1700;

            AddObject(new Inhibitor(game, 0xffd23c3e, "Barracks_T1_L1", TeamId.TEAM_BLUE, COLLISION_RADIUS, 633.5612f, 7945.7744f, SIGHT_RANGE)); //top
            AddObject(new Inhibitor(game, 0xff9303e1, "Barracks_T1_R1", TeamId.TEAM_BLUE, COLLISION_RADIUS, 639.4373f, 4357.0068f, SIGHT_RANGE)); //bot
            AddObject(new Inhibitor(game, 0xff6793d0, "Barracks_T2_L1", TeamId.TEAM_PURPLE, COLLISION_RADIUS, 12361.9043f, 7884.8813f, SIGHT_RANGE)); //top
            AddObject(new Inhibitor(game, 0xff26ac0f, "Barracks_T2_R1", TeamId.TEAM_PURPLE, COLLISION_RADIUS, 12504.2646f, 4275.3999f, SIGHT_RANGE)); //bot

            AddObject(new Nexus(game, 0xfff97db5, "HQ_T1", TeamId.TEAM_BLUE, COLLISION_RADIUS, 1810.5286f, 6016.3965f, SIGHT_RANGE));
            AddObject(new Nexus(game, 0xfff02c0f, "HQ_T2", TeamId.TEAM_PURPLE, COLLISION_RADIUS, 10874.4785f, 6016.8369f, SIGHT_RANGE));

            // Start at xp to reach level 1
            _expToLevelUp = new List<int> { 0, 280, 660, 1140, 1720, 2400, 3180, 4060, 5040, 6120, 7300, 8580, 9960, 11440, 13020, 14700, 16480, 18360 };

            // Announcer events
            _announcerEvents.Add(new Announce(game, 30 * 1000, Announces.WelcomeToSR, true)); // Welcome to SR
            if (_firstSpawnTime - 30 * 1000 >= 0.0f)
                _announcerEvents.Add(new Announce(game, _firstSpawnTime - 30 * 1000, Announces.ThirySecondsToMinionsSpawn, true)); // 30 seconds until minions spawn
            _announcerEvents.Add(new Announce(game, _firstSpawnTime, Announces.MinionsHaveSpawned, false)); // Minions have spawned (90 * 1000)
            _announcerEvents.Add(new Announce(game, _firstSpawnTime, Announces.MinionsHaveSpawned2, false)); // Minions have spawned [2] (90 * 1000)
        }

        public override void Update(long diff)
        {
            base.Update(diff);

            if (_gameTime >= 120 * 1000)
            {
                SetKillReduction(false);
            }
        }
        public override float GetGoldPerSecond()
        {
            return 1.9f;
        }

        public override Target GetRespawnLocation(int team)
        {
            switch (team)
            {
                case 0:
                    return new GameObjects.Target(1051.19f, 7283.599f);
                case 1:
                    return new GameObjects.Target(14364, 7277);
            }

            return new GameObjects.Target(1051.19f, 7283.599f);
        }
        public override float GetGoldFor(Unit u)
        {
            var m = u as Minion;
            if (m == null)
            {
                var c = u as Champion;
                if (c == null)
                    return 0.0f;

                float gold = 300.0f; //normal gold for a kill
                if (c.getKillDeathCounter() < 5 && c.getKillDeathCounter() >= 0)
                {
                    if (c.getKillDeathCounter() == 0)
                        return gold;
                    for (int i = c.getKillDeathCounter(); i > 1; --i)
                        gold += gold * 0.165f;

                    return gold;
                }

                if (c.getKillDeathCounter() >= 5)
                    return 500.0f;

                if (c.getKillDeathCounter() < 0)
                {
                    float firstDeathGold = gold - gold * 0.085f;

                    if (c.getKillDeathCounter() == -1)
                        return firstDeathGold;

                    for (int i = c.getKillDeathCounter(); i < -1; ++i)
                        firstDeathGold -= firstDeathGold * 0.2f;

                    if (firstDeathGold < 50)
                        firstDeathGold = 50;

                    return firstDeathGold;
                }

                return 0.0f;
            }

            switch (m.getType())
            {
                case MinionSpawnType.MINION_TYPE_MELEE:
                    return 19.0f + ((0.5f) * (int)(_gameTime / (180 * 1000)));
                case MinionSpawnType.MINION_TYPE_CASTER:
                    return 14.0f + ((0.2f) * (int)(_gameTime / (90 * 1000)));
                case MinionSpawnType.MINION_TYPE_CANNON:
                    return 40.0f + ((1.0f) * (int)(_gameTime / (180 * 1000)));
            }

            return 0.0f;
        }
        public override float GetExperienceFor(Unit u)
        {
            var m = u as Minion;

            if (m == null)
                return 0.0f;

            switch (m.getType())
            {
                case MinionSpawnType.MINION_TYPE_MELEE:
                    return 58.88f;
                case MinionSpawnType.MINION_TYPE_CASTER:
                    return 29.44f;
                case MinionSpawnType.MINION_TYPE_CANNON:
                    return 92.0f;
            }

            return 0.0f;
        }

        public override Tuple<TeamId, Vector2> GetMinionSpawnPosition(MinionSpawnPosition spawnPosition)
        {
            switch (spawnPosition)
            {
                case MinionSpawnPosition.SPAWN_BLUE_TOP:
                    return new Tuple<TeamId, Vector2>(TeamId.TEAM_BLUE, new Vector2(988.4354f, 6820.1563f));
                case MinionSpawnPosition.SPAWN_BLUE_BOT:
                    return new Tuple<TeamId, Vector2>(TeamId.TEAM_BLUE, new Vector2(951.9963f, 5746.0674f));
                case MinionSpawnPosition.SPAWN_RED_TOP:
                    return new Tuple<TeamId, Vector2>(TeamId.TEAM_PURPLE, new Vector2(11936.3457f, 7074.7109f));
                case MinionSpawnPosition.SPAWN_RED_BOT:
                    return new Tuple<TeamId, Vector2>(TeamId.TEAM_PURPLE, new Vector2(12050.5557f, 5747.0601f));
            }
            return new Tuple<TeamId, Vector2>(0, new Vector2());
        }
        public override void SetMinionStats(Minion minion)
        {
            // Same for all minions
            minion.GetStats().MoveSpeed.BaseValue = 325.0f;
            minion.GetStats().AttackSpeedFlat = 0.625f;

            switch (minion.getType())
            {
                case MinionSpawnType.MINION_TYPE_MELEE:
                    minion.GetStats().CurrentHealth = 475.0f + 20.0f * (int)(_gameTime / (float)(180 * 1000));
                    minion.GetStats().HealthPoints.BaseValue = 475.0f + 20.0f * (int)(_gameTime / (float)(180 * 1000));
                    minion.GetStats().AttackDamage.BaseValue = 12.0f + 1.0f * (int)(_gameTime / (float)(180 * 1000));
                    minion.GetStats().Range.BaseValue = 180.0f;
                    minion.GetStats().AttackSpeedFlat = 1.250f;
                    minion.setAutoAttackDelay(11.8f / 30.0f);
                    minion.setMelee(true);
                    break;
                case MinionSpawnType.MINION_TYPE_CASTER:
                    minion.GetStats().CurrentHealth = 279.0f + 7.5f * (int)(_gameTime / (float)(90 * 1000));
                    minion.GetStats().HealthPoints.BaseValue = 279.0f + 7.5f * (int)(_gameTime / (float)(90 * 1000));
                    minion.GetStats().AttackDamage.BaseValue = 23.0f + 1.0f * (int)(_gameTime / (float)(90 * 1000));
                    minion.GetStats().Range.BaseValue = 600.0f;
                    minion.GetStats().AttackSpeedFlat = 0.670f;
                    minion.setAutoAttackDelay(14.1f / 30.0f);
                    minion.setAutoAttackProjectileSpeed(650.0f);
                    break;
                case MinionSpawnType.MINION_TYPE_CANNON:
                    minion.GetStats().CurrentHealth = 700.0f + 27.0f * (int)(_gameTime / (float)(180 * 1000));
                    minion.GetStats().HealthPoints.BaseValue = 700.0f + 27.0f * (int)(_gameTime / (float)(180 * 1000));
                    minion.GetStats().AttackDamage.BaseValue = 40.0f + 3.0f * (int)(_gameTime / (float)(180 * 1000));
                    minion.GetStats().Range.BaseValue = 450.0f;
                    minion.GetStats().AttackSpeedFlat = 1.0f;
                    minion.setAutoAttackDelay(9.0f / 30.0f);
                    minion.setAutoAttackProjectileSpeed(1200.0f);
                    break;
            }
        }

        public override bool Spawn()
        {
            var positions = new List<MinionSpawnPosition>
            {
                MinionSpawnPosition.SPAWN_BLUE_TOP,
                MinionSpawnPosition.SPAWN_BLUE_BOT,
                MinionSpawnPosition.SPAWN_RED_TOP,
                MinionSpawnPosition.SPAWN_RED_BOT,
            };

            if (_waveNumber < 3)
            {
                for (var i = 0; i < positions.Count; ++i)
                {
                    Minion m = new Minion(_game, _game.GetNewNetID(), MinionSpawnType.MINION_TYPE_MELEE, positions[i], _laneWaypoints[i]);
                    AddObject(m);
                }
                return false;
            }

            if (_waveNumber == 3)
            {
                for (var i = 0; i < positions.Count; ++i)
                {
                    Minion m = new Minion(_game, _game.GetNewNetID(), MinionSpawnType.MINION_TYPE_CANNON, positions[i], _laneWaypoints[i]);
                    AddObject(m);
                }
                return false;
            }

            if (_waveNumber < 7)
            {
                for (var i = 0; i < positions.Count; ++i)
                {
                    Minion m = new Minion(_game, _game.GetNewNetID(), MinionSpawnType.MINION_TYPE_CASTER, positions[i], _laneWaypoints[i]);
                    AddObject(m);
                }
                return false;
            }
            return true;
        }

        public override int GetMapId()
        {
            return 1;
        }

        public override Vector2 GetSize()
        {
            return new Vector2(GetWidth() / 2, GetHeight() / 2);
        }

        public override int GetBluePillId()
        {
            return 2001;
        }

        public override float[] GetEndGameCameraPosition(TeamId team)
        {
            if (!_endGameCameraPosition.ContainsKey(team))
                return new float[] { 0, 0, 0 };

            return _endGameCameraPosition[team];
        }
    }
}
