using System;
using System.IO;
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
        
        public static DiscordSocketClient Client { get; private set; }

        static void Main(string[] args)
        {
	        Console.Title =
		        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";

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
	        Console.Title =
		        $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";

            string nitroMessage = args.Message.Content;
            if ((nitroMessage.Contains("discord.gift/") && nitroMessage.Replace("https://discord.gift/", "").Length == 16) ||
                (nitroMessage.Contains("discordapp.com/gifts/") &&
                 nitroMessage.Replace("https://discordapp.com/gifts/", "").Length == 24) ||
                nitroMessage.Contains("discordapp.com/gifts/") || nitroMessage.Contains("discord.gift/"))
            {
                string nitroCode;
                if (nitroMessage.Replace("https://discord.gift/", "").Length == 16)
                {
                    nitroCode = nitroMessage.Replace("https://discord.gift/", "");
                }
                else if (nitroMessage.Replace("discord.gift/", "").Length == 16)
                {
                    nitroCode = nitroMessage.Replace("discord.gift/", "");
                }
                else if (nitroMessage.Replace("discordapp.com/gifts/", "").Length == 24)
                {
                    nitroCode = nitroMessage.Replace("discordapp.com/gifts/", "");
                }
                else
                {
                    nitroCode = nitroMessage.Replace("https://discordapp.com/gifts/", "");
                }
                
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";;
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
							$"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";;
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
	                $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";
            }
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            username = args.User.Username;
            Console.Title =
	            $"AutoNitro - By iLinked | Account: {username} | Hits: {successes}/{attempts} | Balance: ${balance} | Time: {DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss")}";

            Console.WriteLine($"[SUCCESS] Logged into {username}!");
        }
    }
}
