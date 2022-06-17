using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;

namespace HotUpdate.Game1
{
    internal class CountQuery : AbstractQuery<BindableProperty<int>>
    {
        [Inject]
        public Model.Game1Model gameModel;
        protected override BindableProperty<int> OnDo()
        {
            return gameModel.Count;
        }
    }
}
