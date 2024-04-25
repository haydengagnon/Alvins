using common.resources;
using System;

namespace wServer.realm.entities
{
    partial class Player
    {
        public bool CheckInsurgency()
        {
            return Inventory[3] != null && Inventory[3].ObjectId == "Herculean Amulet";
        }

        public bool CheckDRage()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Drannol's Fury";
        }

        public bool CheckFRage()
        {
            return Inventory[1] != null && Inventory[1].ObjectId == "Fortification Shield";
        }

        public bool CheckSunMoon()
        {
            return Inventory[1] != null && Inventory[1].ObjectId == "Grand Curse" && Surge >= 50;
        }

        public bool CheckCrescent()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Lance of Honour";
        }

        public bool CheckGHelm()
        {
            return Inventory[1] != null && Inventory[1].ObjectId == "The Kingdoms Pride";
        }

        public bool CheckAnguish()
        {
            return Inventory[1] != null && Inventory[1].ObjectId == "Skull of the Titan";
        }

        public bool CheckAnubis()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Khonsu's Breastplate" && Surge >= 20;
        }

        public bool CheckForce()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Steel-Woven Hide" && HP <= Stats[0] / 2;
        }

        public bool CheckMerc()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Grace of the Colossus" && MP >= Stats[1] / 2;
        }

        public bool CheckRoyal()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "The Monarch's Grasp" && this.AnyEnemyNearby(10) == false;
        }

        public bool CheckDemo()
        {
            return Inventory[3] != null && Inventory[3].ObjectId == "Band of Exoneration" && HP >= (int)(Stats[0] * 0.8);
        }

        public bool CheckKar()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Evanescence";
        }

        public bool CheckTinda()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Endless Destruction" && MP >= Stats[1] / 2;
        }

        public bool CheckGilded()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Royal Dagger" && this.CountEntity(6, objType: null) >= 2;
        }

        public bool CheckAxe()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Scarlet Battle-Axe" && Surge >= 25;
        }

        public bool CheckMocking()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "The Mocking Raven";
        }

        public bool CheckResistance()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Interstellar Force";
        }

        public bool CheckAegis()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Aegis of the Devourer";
        }

        public bool CheckMoonlight()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Ascension";
        }

        public bool CheckNil()
        {
            return Inventory[2] != null && Inventory[2].ObjectId.Equals("Armor of Nil");
        }
        public bool CheckVBow()
        {
            return Inventory[0] != null && Inventory[0].ObjectId.Equals("Bow of the Void");
        }

        public bool CheckSource()
        {
            return Inventory[3] != null && Inventory[3].ObjectId.Equals("Sourcestone");
        }

        public bool CheckLode()
        {
            return Inventory[1] != null && Inventory[1].ObjectId.Equals("Quiver of the Shadows");
        }
        
        public bool CheckRobe()
        {
            return Inventory[2] != null && Inventory[2].ObjectId.Equals("Cash Robe");
        }

        public bool CheckBloodshed()
        {
            return Inventory[3] != null && Inventory[3].ObjectId.Equals("Bloodshed Ring");
        }

        public bool CheckOmni()
        {
            return Inventory[3] != null && Inventory[3].ObjectId.Equals("Omnipotence Ring");
        }

        public bool CheckSeal()
        {
            return Inventory[3] != null && Inventory[3].ObjectId.Equals("Orendands Ring");
        }

        public bool CheckStone()
        {
            return Inventory[0] != null && Inventory[0].ObjectId.Equals("stone sword");
        }
        
        public bool CheckTear()
        {
            return Inventory[3] != null && Inventory[3].ObjectId.Equals("Ghast Tear");
        }

        public bool CheckDJudgement()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Drannol's Judgement";
        }

        public bool CheckIoksRelief()
        {
            return Inventory[1] != null && Inventory[1].ObjectId == "Banner of Immense Protection" && Surge >= 5;
        }

        public bool CheckBleedingFang()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Blood-Fang" && Surge >= 2;
        }

        public bool CheckBifierce()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Eternal Deficit" && HP <= Stats[0] / 2;
        }

        public bool CheckIoksCourage()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Lance of Immense Protection" && Protection <= 0;
        }

        public bool CheckStarmind()
        {
            return Inventory[3] != null && Inventory[3].ObjectId == "Immunity Gauntlet" && Surge >= 60 && Inventory[1].MpCost > 0;
        }

        public bool CheckDranbielGarbs()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Cloth of the Battle-Mage" && MP >= Stats[1] / 2 && Inventory[1].MpCost > 0;
        }

        public bool CheckStarCrashRing()
        {
            return Inventory[3] != null && Inventory[3].ObjectId == "Asteroid" && Inventory[1].MpCost > 0;
        }

        public bool CheckTheInfernus()
        {
            return Inventory[2] != null && Inventory[2].ObjectId == "Flash-Fire" && Inventory[1].MpCost > 0;
        }

        public bool CheckMeteor()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Sky-Striker" && Inventory[1]?.ObjectId == "Book of Burning Souls" || Inventory[1]?.ObjectId == "Volcanic Vengeance" && Inventory[1].MpCost > 0 && new Random().NextDouble() < 0.14f;
        }

        public bool CheckDimensionalPrism()
        {
            return Inventory[1] != null && Inventory[1].ObjectId == "Prism of Mirrors" && Surge > 10; //&& Surge > 10
        }

        public bool CheckUrumi()
        {
            return Inventory[0] != null && Inventory[0].ObjectId == "Blades of Majesty" && Surge > 10;
        }

        public bool CheckT3Vit()
        {
            for (var i = 0; i < 4; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Ring of Superior Vitality"))
                    return true;
            return false;
        }
        /*
        public bool CheckAnyWelding()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                return true;
            return false;
        }
        public bool CheckAnyWelding5()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding6()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding7()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding8()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding9()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding10()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding11()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckAnyWelding12()
        {
            for (var i = 0; i < 20; i++)
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding1()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding2()
        {
            var i = 6;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding3()
        {
            var i = 7;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding4()
        {
            var i = 8;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding5()
        {
           var i = 9;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding6()
        {
            var i = 10;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding7()
        {
           var i = 11;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding8()
        {
            var i = 12; 
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding9()
        {
            var i = 13; 
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding10()
        {
            var i = 14; 
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding11()
        {
            var i = 15;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding12()
        {
            var i = 16; 
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding13()
        {
            var i = 17; 
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding14()
        {
            var i = 18; 
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding15()
        {
            var i = 19;
                if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T1 Upgrade"))
                    return true;
            return false;
        }
        public bool CheckWelding50()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding51()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding52()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding53()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding54()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding55()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding56()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding57()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding58()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding59()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding510()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding511()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding512()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding513()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding514()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding515()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T2 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding60()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding61()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding62()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding63()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding64()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding65()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding66()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding67()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding68()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding69()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding610()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding611()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding612()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding613()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding614()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding615()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T3 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding70()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding71()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding72()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding73()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding74()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding75()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding76()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding77()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding78()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding79()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding710()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding711()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding712()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding713()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding714()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding715()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T4 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding80()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding81()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding82()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding83()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding84()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding85()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding86()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding87()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding88()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding89()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding810()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding811()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding812()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding813()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding814()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding815()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T5 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding90()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding91()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding92()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding93()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding94()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding95()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding96()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding97()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding98()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding99()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding910()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding911()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding912()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding913()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding914()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding915()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T6 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding100()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding101()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding102()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding103()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding104()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding105()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding106()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding107()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding108()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding109()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1010()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1011()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1012()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1013()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1014()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1015()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T7 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding110()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding111()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding112()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding113()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding114()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding115()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding116()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding117()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding118()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding119()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1110()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1111()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1112()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1113()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1114()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1115()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T8 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding120()
        {
            var i = 4;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding121()
        {
            var i = 5;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding122()
        {
            var i = 6;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding123()
        {
            var i = 7;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding124()
        {
            var i = 8;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding125()
        {
            var i = 9;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding126()
        {
            var i = 10;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding127()
        {
            var i = 11;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding128()
        {
            var i = 12;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding129()
        {
            var i = 13;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1210()
        {
            var i = 14;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1211()
        {
            var i = 15;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1212()
        {
            var i = 16;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1213()
        {
            var i = 17;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1214()
        {
            var i = 18;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        public bool CheckWelding1215()
        {
            var i = 19;
            if (Inventory[i] != null && Inventory[i].ObjectId.Equals("Dreadcull T9 Upgrade"))
                return true;
            return false;
        }
        */
        public bool CheckSacredEffect(string eff)
        {
            for (var i = 0; i < 4; i++)
                if (Inventory[i] != null && Inventory[i].Sacred)
                    if (Inventory[i].SacredEffect == eff)
                        return true;
            return false;
        }

        public bool RollSacredEffect(string eff, int c)
        {
            int count = 0;
            for (var i = 0; i < 4; i++)
                if (Inventory[i] != null && Inventory[i].Sacred)
                    if (Inventory[i].SacredEffect == eff)
                        count++;
            int chance = count * c;
            if (Random.Next(100) < chance)
                return true;
            return false;
        }

        public double SacredBoost(string eff, double c)
        {
            int count = 0;
            for (var i = 0; i < 4; i++)
                if (Inventory[i] != null && Inventory[i].Sacred)
                    if (Inventory[i].SacredEffect == eff)
                        count++;
            return c * count;
        }

        public int[] PosEffects = { 12, 14, 17, 18, 19, 25, 49, 50, 52, 58, 59, 60, 61, 62 };

        public bool ResolveSlot0()
        {
            return Inventory[0] != null && Inventory[0].Legendary;
        }

        public bool ResolveSlot1()
        {
            return Inventory[1] != null && Inventory[1].Legendary;
        }

        public bool ResolveSlot2()
        {
            return Inventory[2] != null && Inventory[2].Legendary;
        }

        public bool ResolveSlot3()
        {
            return Inventory[3] != null && Inventory[3].Legendary;
        }
    }
}