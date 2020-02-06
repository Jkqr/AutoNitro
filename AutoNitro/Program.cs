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
        
        public static DiscordSocketClient Client { get; private set; }

        static void Main(string[] args)
        {
	        Console.Title =
		        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";

            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

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
            Client.Login(config.Token);
            
            Thread.Sleep(-1);
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
	        messages++;
	        
	        Console.Title =
		        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";

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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";;
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
	                $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";
            }
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            username = args.User.Username;
            Console.Title =
	            $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Messages: {messages}";

            Console.WriteLine($"[SUCCESS] Logged into {username}!");
        }
    }
}
