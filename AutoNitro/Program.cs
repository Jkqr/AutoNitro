using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Discord;
using Discord.Gateway;
using System.Threading;

namespace AutoNitro
{
    class Program
    {
        public static int attempts;
        public static int successes;
        public static string username;
        public static double balance;
        public static int messages;
        public static Config config;
        public static int giveawaysParticipated;
        public static int giveawaysTotal;
        
        public static DiscordSocketClient Client { get; private set; }

        static void Main(string[] args)
        {
	        Console.Title =
		        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";

            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            Console.ForegroundColor = ConsoleColor.DarkRed;
            
            while (string.IsNullOrEmpty(config.Token))
            {
                Console.Write("Your Discord token: ");
                config.Token = Console.ReadLine();
            }

            File.WriteAllText("Config.json", JsonConvert.SerializeObject(config));

            Client = new DiscordSocketClient();
            Client.OnLoggedIn += Client_OnLoggedIn;
            Client.OnMessageReceived += Client_OnMessageReceived;
            Client.OnMessageReactionAdded += Client_OnReactionAdded;
            Client.Login(config.Token);
            
            Thread.Sleep(-1);
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
	        messages++;
	        
	        Console.Title =
		        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";

            string nitroMessage = args.Message.Content;
            Regex regex = new Regex(@"[^\s]+iscord.gift/[^\s]+|[^\s]+iscordapp.com/gifts/[^\s]+");
            MatchCollection nitroCodeMatches = regex.Matches(nitroMessage);
            for (int i = 0; i < nitroCodeMatches.Count; i++)
            {
	            string nitroCode = nitroCodeMatches[i].ToString();
	            
	            nitroCode = nitroCode.Replace("https://discord.gift/", "");
	            nitroCode = nitroCode.Replace("https://discordapp.com/gifts/", "");
	            nitroCode = nitroCode.Replace("discord.gift/", "");
	            nitroCode = nitroCode.Replace("discordapp.com/gifts/", "");
                try
                {
                    Client.RedeemNitroGift(nitroCode, args.Message.ChannelId);
                    string planName = client.GetNitroGift(nitroCode).SubscriptionPlan.Name;
                    if (planName.Contains("Classic"))
					{
						planName = Convert.ToString("$5 Classic");
						Console.WriteLine(string.Concat(new string[]
						{
							"[SUCCESS] Redeemed Nitro Gift: ",
							nitroCode,
							" | Server: ",
							Client.GetGuild(args.Message.GuildId.Value).Name,
							" | Time: ",
							DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
							" | [",
							planName,
							"]"
						}), Console.ForegroundColor = ConsoleColor.Green);
						successes++;
						balance += 4.99;
						Console.Title =
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";;
					}
					else if (planName.Contains("Classic") && planName.Contains("Yearly"))
					{
						planName = Convert.ToString("$50 Nitro");
						Console.WriteLine(string.Concat(new string[]
						{
							"[SUCCESS] Redeemed Nitro Gift: ",
							nitroCode,
							" | Server: ",
							Client.GetGuild(args.Message.GuildId.Value).Name,
							" | Time: ",
							DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
							" | [",
							planName,
							"]"
						}), Console.ForegroundColor = ConsoleColor.Green);
						attempts++;
						balance += 49.99;
						Console.Title =
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";;
					}
					else if (planName == "Nitro Yearly")
					{
						planName = Convert.ToString("$100 Nitro");
						Console.WriteLine(string.Concat(new string[]
						{
							"[SUCCESS] Redeemed Nitro Gift: ",
							nitroCode,
							" | Server: ",
							Client.GetGuild(args.Message.GuildId.Value).Name,
							" | Time: ",
							DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
							" | [",
							planName,
							"]"
						}), Console.ForegroundColor = ConsoleColor.Green);
						successes++;
						balance += 99.99;
						Console.Title =
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";;
					}
					else if (planName.Contains("Quarterly"))
					{
						planName = Convert.ToString("3 Months");
						Console.WriteLine(string.Concat(new string[]
						{
							"[SUCCESS] Redeemed Nitro Gift: ",
							nitroCode,
							" | Server: ",
							Client.GetGuild(args.Message.GuildId.Value).Name,
							" | Time: ",
							DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
							" | [",
							planName,
							"]"
						}), Console.ForegroundColor = ConsoleColor.Green);
						successes++;
						balance += 29.97;
						Console.Title =
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";;
					}
					else
					{
						planName = Convert.ToString("$10 Nitro");
						Console.WriteLine(string.Concat(new string[]
						{
							"[SUCCESS] Redeemed Nitro Gift: ",
							nitroCode,
							" | Server: ",
							Client.GetGuild(args.Message.GuildId.Value).Name,
							" | Time: ",
							DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
							" | [",
							planName,
							"]"
						}), Console.ForegroundColor = ConsoleColor.Green);
						successes++;
						balance += 9.99;
						Console.Title =
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";;
					}
                }
                catch (DiscordHttpException ex)
                {
                    switch (ex.Code)
                    {
                        case DiscordError.NitroGiftRedeemed:
                            Console.WriteLine("[ERROR] Nitro gift already redeemed: " + nitroCode);
                            break;
                        case DiscordError.UnknownGiftCode:
                            Console.WriteLine("[ERROR] Invalid Nitro gift: " + nitroCode);
                            break;
                        default:
                            Console.WriteLine($"[ERROR] Unknown error: {ex.Code} | {ex.ErrorMessage}");
                            break;
                    }
                }

                attempts++;
                Console.Title =
	                $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";
            }
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            username = args.User.Username;
            Console.Title =
	            $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";

            Console.WriteLine($"[SUCCESS] Logged into {username}!");
            Console.WriteLine($"[CONFIG] Giveaway React Chance: {config.GiveAwayReactChance}%");
        }

        private static void Client_OnReactionAdded(DiscordSocketClient client, ReactionEventArgs args)
        {
	        if (args.Reaction.UserId.Equals(611040420872585226) || args.Reaction.UserId.Equals(582537632991543307) ||
	            args.Reaction.UserId.Equals(294882584201003009))
	        {
		        if (client.GetMessageReactions(args.Reaction.ChannelId, args.Reaction.MessageId,
			            args.Reaction.Emoji.GetMessegable()).Count == 1)
		        {
			        giveawaysTotal++;
			        if (new Random().Next(1, 100) <= config.GiveAwayReactChance)
			        {
				        client.AddMessageReaction(args.Reaction.ChannelId, args.Reaction.MessageId,
					        args.Reaction.Emoji.GetMessegable());
				        giveawaysParticipated++;
				        Console.WriteLine(
					        $"[Drops/GIVEAWAYS] Participated in https://discordapp.com/channels/{args.Reaction.GuildId}/{args.Reaction.ChannelId}/{args.Reaction.MessageId}");
				        Console.Title =
					        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";

			        }
			        else
			        {
				        Console.WriteLine(
					        $"[Drops/GIVEAWAYS] Did not participate in https://discordapp.com/channels/{args.Reaction.GuildId}/{args.Reaction.ChannelId}/{args.Reaction.MessageId} due to reaction chance. (Roll was ");
				        Console.Title =
					        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts}/{messages} | Balance: ${balance} | Drops/Giveaways Participated: {giveawaysParticipated}/{giveawaysTotal}";

			        }
		        }
	        }
        }
    }
}
