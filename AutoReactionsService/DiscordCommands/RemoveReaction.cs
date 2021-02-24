// -----------------------------------------------------------------------
// <copyright file="RemoveReaction.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;
using System.Threading.Tasks;

using AutoReactionsService.API.Features;
using Discord;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;

namespace AutoReactionsService.DiscordCommands
{
    public class RemoveReaction : ConditionDiscordCommand
    {
        public RemoveReaction()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("autoreactions.remove"));
        }

        public override string Name { get; } = "autoreactions remove";

        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        protected override async Task Execute(IMessage message, string parameter)
        {
            var match = Regex.Match(parameter, @"(.+)\s(.+)");
            string rawRoleId = match.Result("$1");
            string rawReaction = match.Result("$2");
            if (!ulong.TryParse(Regex.Match(rawRoleId, @"<@&(.\d+)>").Result("$1"), out ulong roleId))
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid role!");
                return;
            }

            if (!ulong.TryParse(Regex.Match(rawReaction, @"<:.+:(\d+)>").Result("$1"), out ulong reactionId))
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid reaction!");
                return;
            }

            try
            {
                AutoReactions.RemoveRoleReaction(roleId, reactionId);
            }
            catch
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} autoreaction does not exist!");
                return;
            }

            await message.Channel.SendMessageAsync($"{message.Author.Mention} autoreaction removed.");
        }
    }
}