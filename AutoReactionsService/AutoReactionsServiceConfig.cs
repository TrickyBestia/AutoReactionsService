// -----------------------------------------------------------------------
// <copyright file="AutoReactionsServiceConfig.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Interfaces;

namespace AutoReactionsService
{
    public class AutoReactionsServiceConfig : IConfig
    {
        public bool IsEnabled { get; set; } = false;

        public Dictionary<ulong, HashSet<ulong>> RoleReactions { get; set; } = new Dictionary<ulong, HashSet<ulong>>();
    }
}