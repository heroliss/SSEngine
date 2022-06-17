using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace HotUpdate.Game1
{
    public class Game1Architecture : Architecture<Game1Architecture>
    {
        protected override void Init()
        {
            this.RegisterModel<Model.Game1Model>(new Model.Game1Model());
            this.RegisterUtility<Utility.IStorage>(new Utility.PlayerPrefsStorage());
            this.RegisterSystem<System.CountSystem>(new System.CountSystem());
        }
    }
}
