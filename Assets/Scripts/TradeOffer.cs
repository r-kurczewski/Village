public class TradeOffer
{
	public enum TradeMode { OnlyBuy, OnlySell, BuyAndSell}

	public TradeMode mode;
	public ResourceView resource;
	public int cost;
	public int reputationPerItem;



}