using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;

namespace HotUpdate.Game1.Model
{
    public class Game1Model : AbstractModel
    {
        [Inject]
        public Utility.IStorage storage;
        public BindableProperty<int> Count { get; } = new BindableProperty<int>();
        protected override void OnInit()
        {
            //storage = this.GetUtility<Utility.IStorage>();
            Count.Value = storage.LoadInt(nameof(Count), 0);
            Count.Register(v => storage.SaveInt(nameof(Count), v));
        }
    }
}
