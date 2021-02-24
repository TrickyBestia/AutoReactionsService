// -----------------------------------------------------------------------
// <copyright file="ListReactions.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Text;
using System.Threading.Tasks;

using Discord;
using TrickyBot;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;
using TrickyBot.Services.SingleServerInfoProviderService.API.Features;

namespace AutoReactionsService.DiscordCommands
{
    public class ListReactions : ConditionDiscordCommand
    {
        public ListReactions()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("autoreactions.list"));
        }

        public override string Name { get; } = "autoreactions list";

        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<AutoReactionsService>();
            var response = new StringBuilder();
            response.AppendLine($"{message.Author.Mention} autoreactions:");
            foreach (var role in service.Config.RoleReactions)
            {
                response.Append($"<@&{role.Key}>: ");
                foreach (var reaction in role.Value)
                {
                    response.Append((await SSIP.Guild.GetEmoteAsync(reaction)).ToString()).Append(' ');
                }

                response.AppendLine();
            }

            await message.Channel.SendMessageAsync(response.ToString());
        }
    }
}