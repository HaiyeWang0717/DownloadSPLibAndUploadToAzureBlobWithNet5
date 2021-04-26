using Microsoft.SharePoint.Client;
using System;
using System.Threading.Tasks;

namespace DownloadSPLibAndUploadToAzureBlobWithNet5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Uri site = new Uri("https://******.sharepoint.com/sites/10000034");
            string clientId = "";
            string clientSecret = "";

            // Note: The PnP Sites Core AuthenticationManager class also supports this
            using (var authenticationManager = new AuthenticationManager())
            {
                using (var context = authenticationManager.GetContext(site, clientId, clientSecret))
                {
                    context.Load(context.Web, p => p.Title);
                    await context.ExecuteQueryAsync();
                    Console.WriteLine($"Title: {context.Web.Title}");

                    var list = context.Web.Lists.GetByTitle("Links");
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem newItem = list.AddItem(itemCreateInfo);
                    newItem["Title"] = "My New Item!";
                    newItem.Update();
                    await context.ExecuteQueryAsync();
                    Console.WriteLine(newItem.Id);
                }
            }

        }
    }
}
