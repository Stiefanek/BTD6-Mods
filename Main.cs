using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Main.Scenes;
using Assets.Scripts.Models;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Powers;
using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.InGame.StoreMenu;
using Assets.Scripts.Unity.UI_New.Upgrade;
using Assets.Scripts.Utils;
using Harmony;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

using UnhollowerBaseLib;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using System.Net;
using Assets.Scripts.Unity.UI_New.Popups;
using TMPro;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.TowerFilters;
using Assets.Scripts.Unity.UI_New.Main.MonkeySelect;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using Assets.Scripts.Simulation.Towers.Behaviors;

namespace monko
{

    class Main : BloonsMod
    {
        //https://github.com/gurrenm3/BloonsTD6-Mod-Helper/releases

        public class Monko:ModTower
        {
            public override string Name => "Monko";
            public override string DisplayName => "Monko";
            public override string Description => "Monko pops Bloons and shows them what hell means";
            public override string BaseTower => "BombShooter";
            public override int Cost => 1500;
            public override int TopPathUpgrades => 5;
            public override int MiddlePathUpgrades => 5;
            public override int BottomPathUpgrades => 5;
            public override string TowerSet => "Magic";
            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {
                //balance stuff
                //towerModel.display = "33e6106304c401a4bb5f301ff3385643";
                towerModel.display = new PrefabReference() { guidRef = "33e6106304c401a4bb5f301ff3385643" };
                //towerModel.GetBehavior<DisplayModel>().display = "33e6106304c401a4bb5f301ff3385643";
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "33e6106304c401a4bb5f301ff3385643" };
                var attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().CapDamage(9999);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().maxDamage = 9999;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.maxPierce = 99999;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.CapPierce(99999);
                attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 99;
                //attackModel.weapons[0].projectile.display = "nill";
                attackModel.weapons[0].projectile.display = new PrefabReference() { guidRef = "nill" };

                //pierce and damage
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce = 5;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage = 2;

                //change radius to 75% of 100 mortar
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 28 * 0.75f;



                //how many seconds until it shoots
                attackModel.weapons[0].Rate = 2.0f;
            }
            public override string Icon => "monko_Icon";
            public override string Portrait => "monko_Portrait";
        }
        public class EnlargedShots : ModUpgrade<Monko>
        {
            public override string Name => "StrongerFists";
            public override string DisplayName => "Stronger Fists";
            public override string Description => "Monko slaps faster and better";
            public override int Cost => 1;
            public override int Path => TOP;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 10;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 3;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 28 * 1.1f;
                attackModel.weapons[0].Rate = 1.5f;
            }
            public override string Icon => "strongerfists_Icon";
        }
        public class NadeOptimizer : ModUpgrade<Monko>
        {
            public override string Name => "EvenStrongerFists";
            public override string DisplayName => "Even Stronger Fists";
            public override string Description => "Monko's Fists become even stronger";
            public override int Cost => 430;
            public override int Path => TOP;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 5;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 3;
            }
            public override string Icon => "Even_Icon";
        }
        public class HeavyBlasts : ModUpgrade<Monko>
        {
            public override string Name => "DestructionFists";
            public override string DisplayName => "Destruction Fists";
            public override string Description => "The Destruction Fists Destroy Every Type of Bloon that comes in his way";
            public override int Cost => 1002;
            public override int Path => TOP;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 8;
            }
            public override string Icon => "Destruction_Icon";
            public override string Portrait => "Destruction_Portrait";
        }
        public class T101Feldhaubitz : ModUpgrade<Monko>
        {
            public override string Name => "ShockingHand";
            public override string DisplayName => "Shoking Hand";
            public override string Description => "The Shocking Hands can now shoot laser beams?";
            public override int Cost => 4721;
            public override int Path => TOP;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var lasershock = Game.instance.model.GetTowerFromId("DartlingGunner-200").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>();
                lasershock.lifespan = 0.05F;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(lasershock);
                attackModel.weapons[0].projectile.AddBehavior(lasershock);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { 0, 1 };
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 0.5F;
            }
            public override string Icon => "Shocking_Icon";
            public override string Portrait => "Shocking_Portrait";
        }
        public class TheBigBang : ModUpgrade<Monko>
        {
            public override string Name => "TheBiggestFists";
            public override string DisplayName => "The Biggest Fists";
            public override string Description => "These Fists are So Gigantic";
            public override int Cost => 100000;
            public override int Path => TOP;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var Bomb = Game.instance.model.GetTowerFromId("DartlingGunner-500").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>();
                Bomb.lifespan = 0.05F;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(Bomb);
                attackModel.weapons[0].projectile.AddBehavior(Bomb);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { 0, 1 };
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 3;
            }
            public override string Icon => "Biggest_Icon";
            public override string Portrait => "Biggest_Portrait";
        }
        public class QuickdrawSight : ModUpgrade<Monko>
        {
            public override string Name => "MonkoTraining";
            public override string DisplayName => "Monko Training,";
            public override string Description => "Monko can see Camo Bloons. But not DDT's (need's M.I.B)";
            public override int Cost => 152;
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)

            {
                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetection", true));
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var normal = Game.instance.model.GetTowerFromId("BombShooter-050").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>();
                normal.setDamage(2);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.RemoveBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(normal);

                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 10, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 10, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 10, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 10, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 10, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 10, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 10, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
            }
            public override string Icon => "Even_Icon";
        }
        public class Tenacious : ModUpgrade<Monko>
        {
            public override string Name => "TheExercise";
            public override string DisplayName => "The Exercise";
            public override string Description => "The Exercise helps Monko to deal more damage";
            public override int Cost => 650;
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var fire = Game.instance.model.GetTowerFromId("MortarMonkey-002").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetBehavior<AddBehaviorToBloonModel>();
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(fire);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { 0, -1 };
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 35 * 1.1f;
            }
            public override string Icon => "Even_Icon";
        }
        public class Adaptive : ModUpgrade<Monko>
        {
            public override string Name => "BombTraining";
            public override string DisplayName => "Bomb Training Whoops";
            public override string Description => "Monko accidently went to bomb training now he slams the ground shooting bombs";
            public override int Cost => 912;
            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();


                var normal = Game.instance.model.GetTowerFromId("BombShooter-050").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>();
                normal.setDamage(2);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.RemoveBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(normal);

                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 10, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 10, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 10, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 10, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 10, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 10, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 10, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
            }
            public override string Icon => "Destruction_Icon";
        }
        public class BouncyShots : ModUpgrade<Monko>
        {
            public override string Name => "MonkosShots";
            public override string DisplayName => "Monko's Shots";
            public override string Description => "Monko gets Bouncing Bullets";
            public override int Cost => 1122;
            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.pierce += 3;
                var bouncy = Game.instance.model.GetTowerFromId("SniperMonkey-030").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<RetargetOnContactModel>();
                bouncy.distance = 100;
                attackModel.weapons[0].projectile.AddBehavior(bouncy);
            }
            public override string Icon => "Shocking_Icon";
            public override string Portrait => "Shocking_Portrait";
        }
        public class ExplosivesSpecialist : ModUpgrade<Monko>
        {
            public override string Name => "UNLEASHFUSTY";
            public override string DisplayName => "UNLEASH FUSTY";
            public override string Description => "Does too much damage to moabs etc.";
            public override int Cost => 200000;
            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                //balance stuff
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 80, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 80, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 80, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 80, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 80, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 10, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 150, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
            }
            public override string Icon => "Biggest_Icon";
            public override string Portrait => "Biggest_Portrait";
        }
        public class NimbleHands : ModUpgrade<Monko>
        {
            public override string Name => "Fasterfists";
            public override string DisplayName => "Faster Fists";
            public override string Description => "Give Monko a Guiness World Record for the fastest Slapping";
            public override int Cost => 1000;
            public override int Path => BOTTOM;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].Rate *= 0.7f;
            }
            public override string Icon => "strongerfists_Icon";
        }
        public class TacticalLobber : ModUpgrade<Monko>
        {
            public override string Name => "Wall Seing";
            public override string DisplayName => "Wall Seing";
            public override string Description => "Allows Monko to put his Fists over the Walls just to Pop the Bloons";
            public override int Cost => 1500;
            public override int Path => BOTTOM;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                towerModel.ignoreBlockers = true;
                attackModel.weapons[0].projectile.ignoreBlockers = true;
                attackModel.weapons[0].projectile.canCollisionBeBlockedByMapLos = false;
                attackModel.attackThroughWalls = true;
            }
            public override string Icon => "Even_Icon";
        }
        public class EMB : ModUpgrade<Monko>
        {
            public override string Name => "MDE";
            public override string DisplayName => "MDE";
            public override string Description => "Let's Monko Destroy Everyone Who Crosses his Path with lasers";
            public override int Cost => 1500;
            public override int Path => BOTTOM;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var lasershock = Game.instance.model.GetTowerFromId("DartlingGunner-200").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>();
                lasershock.lifespan = 0.1f;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(lasershock);
                attackModel.weapons[0].projectile.AddBehavior(lasershock);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { 0, 1 };
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 2;
            }
            public override string Icon => "Destruction_Icon";
        }
        public class T102Jagdfaust : ModUpgrade<Monko>
        {
            public override string Name => "DestructionFirst";
            public override string DisplayName => "Destruction. First.";
            public override string Description => "Every Bloon on his Path he destroys +10 Pierce +2 Damage";
            public override int Cost => 6000;
            public override int Path => BOTTOM;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.pierce += 10f;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 2;
                attackModel.weapons[0].Rate *= 0.5f;
            }
            public override string Icon => "Shocking_Icon";
            public override string Portrait => "Shocking_Portrait";
        }
        public class Zerfallen : ModUpgrade<Monko>
        {
            public override string Name => "Gomonko";
            public override string DisplayName => "Go Monko";
            public override string Description => "Destroys EVERYTHING +200 PIERCE AND +700 DAMAGE";
            public override int Cost => 1000000;
            public override int Path => BOTTOM;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetection", true));
                var normal = Game.instance.model.GetTowerFromId("BombShooter-050").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>();
                normal.setDamage(2);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.RemoveBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(normal);

                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 1000, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 1000, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 1000, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 1000, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 1000, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 1000, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 1000, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 200;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 700;

            }
            public override string Icon => "Biggest_Icon";
            public override string Portrait => "Biggest_Portrait";
        }




        [HarmonyPatch(typeof(InGame), "Update")]
        public class Update_Patch
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                if (!(InGame.instance != null && InGame.instance.bridge != null)) return;
                try
                {
                    foreach (var tts in InGame.Bridge.GetAllTowers())
                    {

                        if (!tts.namedMonkeyKey.ToLower().Contains("monko")) continue;
                        if (tts?.tower?.Node?.graphic?.transform != null)
                        {
                            tts.tower.Node.graphic.transform.localScale = new UnityEngine.Vector3(1.3f, 1.3f, 1.3f);

                        }

                    }
                }
                catch
                {

                }


            }
        }


    }
}