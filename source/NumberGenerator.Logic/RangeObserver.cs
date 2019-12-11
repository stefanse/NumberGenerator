using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Anzahl der generierten Zahlen in einem bestimmten Bereich zählt. 
    /// </summary>
    public class RangeObserver : BaseObserver
    {
        #region Properties

        /// <summary>
        /// Enthält die untere Schranke (inkl.)
        /// </summary>
        public int LowerRange { get; private set; }
        
        /// <summary>
        /// Enthält die obere Schranke (inkl.)
        /// </summary>
        public int UpperRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der Zahlen, welche sich im Bereich befinden.
        /// </summary>
        public int NumbersInRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der gesuchten Zahlen im Bereich.
        /// </summary>
        public int NumbersOfHitsToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public RangeObserver(IObservable numberGenerator, int numberOfHitsToWaitFor, int lowerRange, int upperRange) : base(numberGenerator, int.MaxValue)
        {
            UpperRange = upperRange;
            LowerRange = lowerRange;
            NumbersOfHitsToWaitFor = numberOfHitsToWaitFor;

            if (numberOfHitsToWaitFor <= 0)
            {
                throw new ArgumentException($"Argument {nameof(numberOfHitsToWaitFor)} ist <= 0!");
            }

           if(lowerRange > upperRange)
            {
                throw new ArgumentException($"Argument {nameof(lowerRange)} ist größer als Argument {nameof(upperRange)}");
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder strgb = new StringBuilder();
            strgb.Append($"{base.ToString()} => ");
            strgb.Append($"{nameof(RangeObserver)} ");
            strgb.Append($"[{nameof(LowerRange)}='{LowerRange}'");
            strgb.Append($", {nameof(UpperRange)}='{UpperRange}'");
            strgb.Append($", {nameof(NumbersInRange)}='{NumbersInRange}'");
            strgb.Append($", {nameof(NumbersOfHitsToWaitFor)}='{NumbersOfHitsToWaitFor}']");

            return strgb.ToString();
        }

        public override void OnNextNumber(int number)
        {

            if(number > LowerRange && number <= UpperRange){
                NumbersInRange++;
                Console.ForegroundColor =ConsoleColor.Green;
                Console.WriteLine($"   >> {this.GetType().Name}: Number is in range ('{LowerRange}'-'{UpperRange}')!");
                Console.ResetColor();

             }

            if(NumbersInRange >= NumbersOfHitsToWaitFor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got ('{NumbersInRange}' number in the configured range => I am not interested in new numbers anymore!");
                DetachFromNumberGenerator();
            }
            base.OnNextNumber(number);
        }
        #endregion
    }
}
