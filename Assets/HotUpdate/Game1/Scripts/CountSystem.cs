using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;
using UnityEngine;

namespace HotUpdate.Game1.System
{
    internal class CountSystem : AbstractSystem
    {
        [Inject]
        public Model.Game1Model GameModel;
        protected override void OnInit()
        {
            Debug.Log("System Init");
            GameModel.Count.Register(v =>
            {
                if (v % 10 == 0)
                {
                    this.SendEvent<Events.CountToTenEvent>();
                }
            }); //.UnRegisterWhenGameObjectDestroyed(); //TODO
        }
    }
}
