// <copyright file="GameType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// 
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Create</change>
// </history>

using System.Collections.Generic;
using System;

namespace Fanex.LiveCasinoSetting.Model
{    
    public class GameType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BetTypeId { get; set; }
        public List<StakeLevel> BettingLimit { get; set; }

        public bool IsDirty
        {
            get
            {
                bool isDirty = false;
                foreach (var stakeLevel in BettingLimit)
                {
                    if (stakeLevel.IsDirty)
                    {
                        isDirty = true;
                        break;
                    }
                }

                return isDirty;
            }
        }
    }
}
