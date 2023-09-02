using Microsoft.AspNetCore.SignalR;
using SignalR_SqlTableDependency.Models;
using SignalR_SqlTableDependency.Repositories;

namespace SignalR_SqlTableDependency.Hubs
{
    public class DashboardHub : Hub
    {

        //For DashBoard Controller
        ProductRepository productRepository;
        SaleRepository saleRepository;
        CustomerRepository customerRepository;
        private readonly SignalRWithEFContext dbContext;

         public DashboardHub(IConfiguration configuration, SignalRWithEFContext dbContext)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            productRepository = new ProductRepository(connectionString);
            saleRepository = new SaleRepository(connectionString);
            customerRepository = new CustomerRepository(connectionString);
            this.dbContext = dbContext;
        }


        // Dashboard Controller
        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);

            var productsForGraph = productRepository.GetProductsForGraph();
            await Clients.All.SendAsync("ReceivedProductsForGraph", productsForGraph);
        }

        public async Task SendSales()
        {
            var sales = saleRepository.GetSales();
            await Clients.All.SendAsync("ReceivedSales", sales);

            var salesForGraph = saleRepository.GetSalesForGraph();
            await Clients.All.SendAsync("ReceivedSalesForGraph", salesForGraph);
        }

        public async Task SendCustomers()
        {
            var customers = customerRepository.GetCustomers();
            await Clients.All.SendAsync("ReceivedCustomers", customers);

            var customersForGraph = customerRepository.GetCustomersForGraph();
            await Clients.All.SendAsync("ReceivedCustomersForGraph", customersForGraph);
        }

        //For Admin Controller
        public async Task SendJobStatus(string type, string message, string status)
        {
            await Clients.All.SendAsync("ReceivedMessage", type, message, status);
        }




        //For Notification Controller


        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string username)
        {
            var hubConnections = dbContext.HubConnections.Where(con => con.Username == username).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
            }
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username
            };

            dbContext.HubConnections.Add(hubConnection);
            await dbContext.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = dbContext.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if (hubConnection != null)
            {
                dbContext.HubConnections.Remove(hubConnection);
                dbContext.SaveChangesAsync();
            }

            return base.OnDisconnectedAsync(exception);
        }

    }
}
