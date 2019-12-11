using System;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg => Sum / (base.CountOfNumbersReceived + 1);

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {
          
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder strgb = new StringBuilder();
            strgb.Append($"{base.ToString()} => ");
            strgb.Append($"{nameof(StatisticsObserver)} ");
            strgb.Append($"[{nameof(Min)}='{Min}', ");
            strgb.Append($"{nameof(Max)}='{Max}', ");
            strgb.Append($"{nameof(Sum)}='{Sum}', ");
            strgb.Append($"{nameof(Avg)}='{Avg}']");

            return strgb.ToString();
        }

        public override void OnNextNumber(int number)
        {

            if(base.CountOfNumbersReceived == 0){
                Min = number;
                Max = number;
            }

            if(number < Min){
                Min = number;
            }
            else if (number > Max )
            {
                Max = number;
            }

            Sum =  Sum + number;

            base.OnNextNumber(number);
        }


        #endregion
    }
}
