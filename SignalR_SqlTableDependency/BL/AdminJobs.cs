using SignalR_SqlTableDependency.Hubs;

namespace SignalR_SqlTableDependency.BL
{
    public class AdminJobs
    {
        AdminHub adminHub;

        public AdminJobs(AdminHub adminHub)
        {
            this.adminHub = adminHub;
        }

        public async Task ProcessLoans()
        {
            var status = await RetrieveLoans();
            if (!status)
            {
                // if job fails, stop the further execution
                return;
            }

            status = await CalculateInterest();
            if (!status)
            {
                // if job fails, stop the further execution
                return;
            }

            status = await UpdateLoans();
            if (!status)
            {
                // if job fails, stop the further execution
                return;
            }

            status = await SaveLogs();
            if (!status)
            {
                // if job fails, stop the further execution
                return;
            }

            status = await SendEmails();
            if (!status)
            {
                // if job fails, stop the further execution
                return;
            }
        }

        private async Task<bool> RetrieveLoans()
        {
            var type = "Retrieve";
            var message = "Retrieving loans";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // add some delay so that it looks like it's processing someting
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";
            if (status == "completed")
                message = $"Retrieved the loans. ({delayInMS} milli seconds)";
            else
                message = "Failed to retrieve the loans.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else
                return false;
        }

        private async Task<bool> CalculateInterest()
        {
            var type = "Calculate";
            var message = "Calculating interest";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // add some delay so that it looks like it's processing someting
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";
            if (status == "completed")
                message = $"Calculated interest for loans. ({delayInMS} milli seconds)";
            else
                message = "Failed to calculate the interest.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else
                return false;
        }

        private async Task<bool> UpdateLoans()
        {
            var type = "Update";
            var message = "Updating loans";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // add some delay so that it looks like it's processing something
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";
            if (status == "completed")
                message = $"Updated the loans. ({delayInMS} milli seconds)";
            else
                message = "Failed to update the loans.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else
                return false;
        }

        private async Task<bool> SaveLogs()
        {
            var type = "Logs";
            var message = "Saving logs";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // add some delay so that it looks like it's processing someting
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";
            if (status == "completed")
                message = $"Saved the logs. ({delayInMS} milli seconds)";
            else
                message = "Failed to save the logs.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else
                return false;
        }

        private async Task<bool> SendEmails()
        {
            var type = "Emails";
            var message = "Sending emails";
            var status = "started";

            await adminHub.SendJobStatus(type, message, status);

            // add some delay so that it looks like it's processing someting
            int delayInMS = GetRandomDelay();
            Task.Delay(delayInMS).Wait();

            status = "completed";
            if (status == "completed")
                message = $"Emails are sent. ({delayInMS} milli seconds)";
            else
                message = "Failed to send the emails.";

            await adminHub.SendJobStatus(type, message, status);

            if (status == "completed")
                return true;
            else
                return false;
        }

        private int GetRandomDelay()
        {
            int min = 2000;
            int max = 5000;
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
