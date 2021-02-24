// -----------------------------------------------------------------------
// <copyright file="AddReaction.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using AutoReactionsService.API.Features;
using Discord;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;

namespace AutoReactionsService.DiscordCommands
{
    public class AddReaction : ConditionDiscordCommand
    {
        public AddReaction()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("autoreactions.add"));
        }

        public override string Name { get; } = "autoreactions add";

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
                AutoReactions.AddRoleReaction(roleId, reactionId);
            }
            catch (ArgumentException)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid role!");
                return;
            }
            catch (Exception ex) when (ex.Message == "Item is already exists.")
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} reaction already exists!");
                return;
            }

            await message.Channel.SendMessageAsync($"{message.Author.Mention} reaction added.");
        }
    }
}