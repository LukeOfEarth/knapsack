using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Models
{
    public class Package
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public override string ToString()
        {
            if (!Items.Any())
            {
                return "-";
            }

            return string.Join(",", Items.OrderBy(i => i.Index).Select(i => i.Index));
        }
    }
}
