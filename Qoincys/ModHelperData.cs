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
using BTD_Mod_Helper.Api.Enums;

namespace qoincy
{

    class Main : BloonsMod
    {
        //https://github.com/gurrenm3/BloonsTD6-Mod-Helper/releases

        public class qoincy : ModTower
        {
            public override string Name => "QuincyDadofQuincy";
            public override string DisplayName => "Quincy Dad of Quincy";
            public override string Description => "Proud,strong and intelligent, Quincy Dad of Quincy uses his bow to perform feats of amazing skill.";
            public override string BaseTower => TowerType.DartMonkey;
            public override int Cost => 3000;
            public override int TopPathUpgrades => 5;
            public override int MiddlePathUpgrades => 5;
            public override int BottomPathUpgrades => 5;
            public override string TowerSet => TowerSetType.Primary;
            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {         
                //balance stuff
                //towerModel.display = "cddca470955e4e64da4063f1aa110f2b";
                towerModel.display = new PrefabReference() { guidRef = "cddca470955e4e64da4063f1aa110f2b" };
                //towerModel.GetBehavior<DisplayModel>().display = "cddca470955e4e64da4063f1aa110f2b";
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "cddca470955e4e64da4063f1aa110f2b" };
                var attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().CapDamage(9999);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().maxDamage = 9999;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.maxPierce = 99999;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.CapPierce(99999);
                attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 99;
                //attackModel.weapons[0].projectile.display = "62e990209b10d374d89f70c6f578def0";
                attackModel.weapons[0].projectile.display = new PrefabReference() { guidRef = "62e990209b10d374d89f70c6f578def0" };

                //pierce and damage
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce = 25;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage = 2;

                //change radius to 75% of 100 mortar
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 28 * 0.75f;



                //how many seconds until it shoots
                attackModel.weapons[0].Rate = 2.5f;

            }
            public override string Icon => "MonkeyIcons[QuincyIcon]";
            public override string Portrait => "f49307e95b921974cb9e1baadf2352fb";
        }
        public class EnlargedShots : ModUpgrade<qoincy>
        {
            public override string Name => "Sharperarrows";
            public override string DisplayName => "Sharper Shots";
            public override string Description => "Even Powerfull Arrows.";
            public override int Cost => 500;
            public override int Path => TOP;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 10;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 28 * 1.1f;
            }
            public override string Icon => "QuincyPortrait";
        }
        public class NadeOptimizer : ModUpgrade<qoincy>
        {
            public override string Name => "Sharp'erarrows";
            public override string DisplayName => "Sharp'er Arrows";
            public override string Description => "The Arrows Show the Bloons what Hell means.";
            public override int Cost => 900;
            public override int Path => TOP;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 15;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 2;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "f428ba21a9db9c441bf4b090c154edeb" };
                towerModel.display = new PrefabReference() { guidRef = "f428ba21a9db9c441bf4b090c154edeb" };
            }
            public override string Icon => "pp1";
            public override string Portrait => "9ab0f5e080bdcc64aa05f13996e32fe9";
        }
        public class HeavyBlasts : ModUpgrade<qoincy>
        {
            public override string Name => "HeavyArrows";
            public override string DisplayName => "Heavy Arrows";
            public override string Description => "Heavy Arrows";
            public override int Cost => 2000;
            public override int Path => TOP;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 8;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "9703101b845bf6e42a7e22e769cb0c40" };
                towerModel.display = new PrefabReference() { guidRef = "9703101b845bf6e42a7e22e769cb0c40" };
            }
            public override string Icon => "pp2";
            public override string Portrait => "4e77bdcfb2c17d04eb9e379e4ef6ef1c";
        }
        public class T101Feldhaubitz : ModUpgrade<qoincy>
        {
            public override string Name => "Smilz";
            public override string DisplayName => "Smilz";
            public override string Description => "Upgrades Stats";
            public override int Cost => 9000;
            public override int Path => TOP;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 13;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 50;
                attackModel.weapons[0].Rate = 1.75f;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 28 * 1.65f;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "f08231d7f2ac6f1438f76a6120910ccb" };
                towerModel.display = new PrefabReference() { guidRef = "f08231d7f2ac6f1438f76a6120910ccb" };
            }
            public override string Icon => "pp3";
            public override string Portrait => "9f09f9e47a9e71a4fa8e607df6077456";
        }
        public class TheBigBang : ModUpgrade<qoincy>
        {
            public override string Name => "Armorus";
            public override string DisplayName => "The Armorus Blostopus the third The Second and the First";
            public override string Description => "what the hell?";
            public override int Cost => 1000000;
            public override int Path => TOP;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var bananaGunProj = attackModel.weapons[0].projectile;
                bananaGunProj.AddBehavior(new DamageModel("DamageModel_", 6, 1000, true, true, true, BloonProperties.None, BloonProperties.None));
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 100;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 85;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 50;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "3db6fc68b301c6d48aac832f6894c384" };
                towerModel.display = new PrefabReference() { guidRef = "3db6fc68b301c6d48aac832f6894c384" };
                attackModel.weapons[0].Rate = 0f;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 999, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 999, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 999, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 999, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 999, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 100, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 10, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
            }
            public override string Icon => "pp4";
            public override string Portrait => "ec7590b408bb7014d93b3481c1873f93";
        }
        public class QuickdrawSight : ModUpgrade<qoincy>
        {
            public override string Name => "TheIdiotSight";
            public override string DisplayName => "Idiotic Sight";
            public override string Description => "Camo bloon go pop";
            public override int Cost => 450;
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            }
            public override string Icon => "QuincyPortrait";
            public override string Portrait => "QuincyPortrait";
        }
        public class Tenacious : ModUpgrade<qoincy>
        {
            public override string Name => "Pierce";
            public override string DisplayName => "Piercey";
            public override string Description => "Piercey got me good ";
            public override int Cost => 900;
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var fire = Game.instance.model.GetTowerFromId("MortarMonkey-002").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetBehavior<AddBehaviorToBloonModel>();
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(fire);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { 0, -1 };
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "f428ba21a9db9c441bf4b090c154edeb" };
                towerModel.display = new PrefabReference() { guidRef = "f428ba21a9db9c441bf4b090c154edeb" };
            }
            public override string Icon => "pp1";
            public override string Portrait => "pp1";
        }
        public class Adaptive : ModUpgrade<qoincy>
        {
            public override string Name => "Canpop";
            public override string DisplayName => "PopItsAPirate";
            public override string Description => "can damage every bloon type";
            public override int Cost => 900;
            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                DamageModel damageModel = towerModel.GetBehavior<DamageModel>();
                var bananaGunProj = attackModel.weapons[0].projectile;

                var normal = Game.instance.model.GetTowerFromId("BombShooter-050").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>();
                normal.setDamage(3);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.RemoveBehavior<AttackModel>();
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(normal);
                bananaGunProj.AddBehavior(new DamageModel("DamageModel_", 6, 2, true, true, true, BloonProperties.None, BloonProperties.None));


                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 10, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 10, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 10, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 10, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 10, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 10, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 10, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "9703101b845bf6e42a7e22e769cb0c40" };
                towerModel.display = new PrefabReference() { guidRef = "9703101b845bf6e42a7e22e769cb0c40" };
            }
            public override string Icon => "pp2";
            public override string Portrait => "pp2";
        }
        public class BouncyShots : ModUpgrade<qoincy>
        {
            public override string Name => "BouncyArrows";
            public override string DisplayName => "Bouncy Arrows";
            public override string Description => "oohhh Bouncy";
            public override int Cost => 2700;
            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.pierce += 3;
                var bouncy = Game.instance.model.GetTowerFromId("SniperMonkey-030").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<RetargetOnContactModel>();
                bouncy.distance = 100;
                attackModel.weapons[0].projectile.AddBehavior(bouncy);
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "f08231d7f2ac6f1438f76a6120910ccb" };
                towerModel.display = new PrefabReference() { guidRef = "f08231d7f2ac6f1438f76a6120910ccb" };
            }
            public override string Icon => "pp3";
            public override string Portrait => "pp3";
        }
        public class ExplosivesSpecialist : ModUpgrade<qoincy>
        {
            public override string Name => "Arrowpopper";
            public override string DisplayName => "Arrow Popper";
            public override string Description => "bro you got the whole squad laughing";
            public override int Cost => 22500;
            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                //balance stuff
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var bananaGunProj = attackModel.weapons[0].projectile;
                bananaGunProj.AddBehavior(new DamageModel("DamageModel_", 6, 300, true, true, true, BloonProperties.None, BloonProperties.None));
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 100, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 100, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 100, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 100, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 100, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 100, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 100, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "3db6fc68b301c6d48aac832f6894c384" };
                towerModel.display = new PrefabReference() { guidRef = "3db6fc68b301c6d48aac832f6894c384" };
                attackModel.range += 100000;
            }
            public override string Icon => "pp4";
            public override string Portrait => "pp4";
        }
        public class NimbleHands : ModUpgrade<qoincy>
        {
            public override string Name => "Stop";
            public override string DisplayName => "Stop it.";
            public override string Description => "STOP";
            public override int Cost => 1000;
            public override int Path => BOTTOM;
            public override int Tier => 1;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].Rate *= 0.8f;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 10;
            }
            public override string Icon => "QuincyPortrait";
            public override string Portrait => "QuincyPortrait";
        }
        public class TacticalLobber : ModUpgrade<qoincy>
        {
            public override string Name => "Bro";
            public override string DisplayName => "Bro";
            public override string Description => "Bro";
            public override int Cost => 10000;
            public override int Path => BOTTOM;
            public override int Tier => 2;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                towerModel.ignoreBlockers = true;
                attackModel.weapons[0].projectile.ignoreBlockers = true;
                attackModel.weapons[0].projectile.canCollisionBeBlockedByMapLos = false;
                attackModel.attackThroughWalls = true;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 10;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "f428ba21a9db9c441bf4b090c154edeb" };
                towerModel.display = new PrefabReference() { guidRef = "f428ba21a9db9c441bf4b090c154edeb" };
            }
            public override string Icon => "pp1";
            public override string Portrait => "pp1";
        }
        public class EMB : ModUpgrade<qoincy>
        {
            public override string Name => "Listen";
            public override string DisplayName => "You Dont Listen Right?";
            public override string Description => "Like your awakening the Monster";
            public override int Cost => 5000;
            public override int Path => BOTTOM;
            public override int Tier => 3;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var lasershock = Game.instance.model.GetTowerFromId("DartlingGunner-200").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>();
                lasershock.lifespan = 4;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(lasershock);
                attackModel.weapons[0].projectile.AddBehavior(lasershock);
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { 0, 1 };
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 10;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "9703101b845bf6e42a7e22e769cb0c40" };
                towerModel.display = new PrefabReference() { guidRef = "9703101b845bf6e42a7e22e769cb0c40" };
            }
            public override string Icon => "pp2";
            public override string Portrait => "pp2";
        }
        public class T102Jagdfaust : ModUpgrade<qoincy>
        {
            public override string Name => "Ugh";
            public override string DisplayName => "Ugh";
            public override string Description => "Final Chance";
            public override int Cost => 100000;
            public override int Path => BOTTOM;
            public override int Tier => 4;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.pierce += 4f;
                attackModel.weapons[0].Rate *= 3f;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 100;
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "f08231d7f2ac6f1438f76a6120910ccb" };
                towerModel.display = new PrefabReference() { guidRef = "f08231d7f2ac6f1438f76a6120910ccb" };
            }
            public override string Icon => "pp3";
            public override string Portrait => "pp3";
        }
        public class Zerfallen : ModUpgrade<qoincy>
        {
            public override string Name => "Monster";
            public override string DisplayName => "Monster";
            public override string Description => "Quincy's Father died now you've awakend the Monster!";
            public override int Cost => 5000000;
            public override int Path => BOTTOM;
            public override int Tier => 5;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var bananaGunProj = attackModel.weapons[0].projectile;
                bananaGunProj.AddBehavior(new DamageModel("DamageModel_", 6, 1000, true, true, true, BloonProperties.None, BloonProperties.None));
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce += 100;
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<DamageModel>().damage += 600;
                var genericDamage = Game.instance.model.GetTowerFromId("DartlingGunner-050").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<DamageModel>();
                attackModel.weapons[0].projectile.AddBehavior(genericDamage);
                attackModel.weapons[0].projectile.GetBehavior<DamageModel>().damage += 100000;
                attackModel.weapons[0].projectile.pierce += 100;
                attackModel.weapons[0].projectile.display = new PrefabReference() { guidRef = "88399aeca4ae48a44aee5b08eb16cc61" };
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Fortified", 1, 999, false, false) { tags = new string[] { "Fortified" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ceramic", 1, 999, false, false) { tags = new string[] { "Ceramic" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Moab", 1, 999, false, false) { tags = new string[] { "Moab" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bfb", 1, 999, false, false) { tags = new string[] { "Bfb" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Zomg", 1, 999, false, false) { tags = new string[] { "Zomg" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Ddt", 1, 10000, false, false) { tags = new string[] { "Ddt" }, collisionPass = 0 });
                attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("aaa", "Bad", 1, 40000, false, false) { tags = new string[] { "Bad" }, collisionPass = 0 });
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "3db6fc68b301c6d48aac832f6894c384" };
                towerModel.display = new PrefabReference() { guidRef = "3db6fc68b301c6d48aac832f6894c384" };
                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
                attackModel.weapons[0].Rate *= 0f;
            }
            public override string Icon => "pp4";
            public override string Portrait => "pp4";
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

                        if (!tts.namedMonkeyKey.ToLower().Contains("qoincy")) continue;
                        if (tts?.tower?.Node?.graphic?.transform != null)
                        {
                            tts.tower.Node.graphic.transform.localScale = new UnityEngine.Vector3(2f, 2f, 2f);

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