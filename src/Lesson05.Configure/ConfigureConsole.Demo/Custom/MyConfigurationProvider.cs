using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ConfigureConsole.Demo.Custom
{
    class MyConfigurationProvider : ConfigurationProvider//, IConfigurationProvider
    {
        Timer timer;
        public MyConfigurationProvider() : base()
        {
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 3000;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Load(true);
        }

        public override void Load()
        {
            Load(false);
        }

        /// <summary>
        /// 这里可以指定自定义数据获取方式
        /// </summary>
        /// <param name="reload"></param>
        void Load(bool reload)
        {
            this.Data["lastTime"] = DateTime.Now.ToString();
            {
                if (reload)
                {
                    base.OnReload();
                }
            }
        }

    }
}
