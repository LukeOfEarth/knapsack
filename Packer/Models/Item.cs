namespace com.mobiquity.packer.Models
{
    public class Item
    {
        public int Index { get; set; }
        public double Weight { get; set; }
        public decimal Cost { get; set; }

        public Item(int Index, double Weight, decimal Cost)
        {
            this.Index = Index;
            this.Weight = Weight;
            this.Cost = Cost;
        }
    }
}
