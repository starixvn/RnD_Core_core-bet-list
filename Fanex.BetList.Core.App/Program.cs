using Fanex.BetList.Core.Builder;
using Fanex.BetList.Core.Builder.ChoiceBuilder;
using Fanex.BetList.Core.Builder.OddsBuilder;
using Fanex.BetList.Core.Builder.StakeBuilder;
using Fanex.BetList.Core.Builder.StatusBuilder;
using Fanex.BetList.Core.Builder.TransBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using RemotingRef;

namespace Fanex.BetList.Core.App
{
    public class BetListBaseBuilderUnderTest : BetListBaseBuilder
    {
        public new IChoice CreateChoiceBuilder(int bettype)
        {
            return base.CreateChoiceBuilder(bettype);
        }

        public new IOdds CreateOddsBuilder(int bettype)
        {
            return base.CreateOddsBuilder(bettype);
        }

        public new IStake CreateStakeBuilder(int bettype)
        {
            return base.CreateStakeBuilder(bettype);
        }

        public new IStatus CreateStatusBuilder(int bettype)
        {
            return base.CreateStatusBuilder(bettype);
        }

        public new ITrans CreateTransBuilder(int bettype)
        {
            return base.CreateTransBuilder(bettype);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            var builder = new BetListBaseBuilderUnderTest();
            var length = 100000;

            sw.Start();

            for (int i = 0; i < length; i++)
            {
                var choice = builder.CreateChoiceBuilder(1);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            var temp = RefData.GetResourceDataByRefIdAndResourceId("GD_Baccarat", "1", "en", string.Empty);



            Console.ReadLine();
        }
    }
}
