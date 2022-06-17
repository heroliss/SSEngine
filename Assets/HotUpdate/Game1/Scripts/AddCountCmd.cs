using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;

namespace HotUpdate.Game1
{
    internal class AddCountCmd : AbstractCommand
    {
        [Inject]
        public Model.Game1Model gameModel;
        protected override void OnExecute()
        {
            //gameModel = this.GetModel<Model.Game1Model>();
            gameModel.Count.Value++;
        }
    }
}
