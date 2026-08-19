using Microsoft.AspNetCore.SignalR;

public class AuctionHub : Hub
{
    public async Task JoinAuctionGroup(Guid auctionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(auctionId));
    }

    public async Task LeaveAuctionGroup(Guid auctionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(auctionId));
    }

    public static string GetGroupName(Guid auctionId) => $"auction-{auctionId}";

}