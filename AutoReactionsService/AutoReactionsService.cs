// -----------------------------------------------------------------------
// <copyright file="AutoReactionsService.cs" company="TrickyBestia">
// Copyright (c) TrickyBestia. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Discord.WebSocket;
using TrickyBot;
using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.SingleServerInfoProviderService.API.Features;

namespace AutoReactionsService
{
    public class AutoReactionsService : ServiceBase<AutoReactionsServiceConfig>
    {
        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Author = "TrickyBestia",
            Name = "AutoReactionsService",
            Version = new Version(1, 0, 0, 0),
            GithubRepositoryUrl = "https://github.com/TrickyBestia/AutoReactionsService",
        };

        protected override Task OnStart()
        {
            Bot.Instance.Client.MessageReceived += this.OnMessageReceived;
            return Task.CompletedTask;
        }

        protected override Task OnStop()
        {
            Bot.Instance.Client.MessageReceived -= this.OnMessageReceived;
            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(SocketMessage message)
        {
            var author = (SocketGuildUser)message.Author;
            foreach (var role in this.Config.RoleReactions)
            {
                foreach (var authorRole in author.Roles)
                {
                    if (authorRole.Id == role.Key)
                    {
                        foreach (var reactionId in role.Value)
                        {
                            var reaction = await SSIP.Guild.GetEmoteAsync(reactionId);
                            await message.AddReactionAsync(reaction);
                        }

                        break;
                    }
                }
            }
        }
    }
}