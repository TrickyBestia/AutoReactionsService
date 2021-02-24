// -----------------------------------------------------------------------
// <copyright file="AutoReactions.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using TrickyBot;
using TrickyBot.Services.SingleServerInfoProviderService.API.Features;

namespace AutoReactionsService.API.Features
{
    public static class AutoReactions
    {
        public static void AddRoleReaction(ulong roleId, ulong reactionId)
        {
            var service = Bot.Instance.ServiceManager.GetService<AutoReactionsService>();

            if (!SSIP.Guild.Emotes.Any(emote => emote.Id == reactionId))
            {
                throw new ArgumentException(null, nameof(reactionId));
            }

            if (!service.Config.RoleReactions.ContainsKey(roleId))
            {
                service.Config.RoleReactions.Add(roleId, new HashSet<ulong>());
            }

            if (!service.Config.RoleReactions[roleId].Add(reactionId))
            {
                throw new Exception("Item is already exists.");
            }
        }

        public static void RemoveRoleReaction(ulong roleId, ulong reactionId)
        {
            var service = Bot.Instance.ServiceManager.GetService<AutoReactionsService>();

            if (!service.Config.RoleReactions.ContainsKey(roleId) || !service.Config.RoleReactions[roleId].Remove(reactionId))
            {
                throw new Exception();
            }
            else if (service.Config.RoleReactions[roleId].Count == 0)
            {
                service.Config.RoleReactions.Remove(roleId);
            }
        }
    }
}