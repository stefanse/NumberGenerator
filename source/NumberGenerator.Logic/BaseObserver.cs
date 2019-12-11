﻿using System;
using System.ComponentModel;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Zahlen auf der Konsole ausgibt.
    /// Diese Klasse dient als Basisklasse für komplexere Beobachter.
    /// </summary>
    public class BaseObserver : IObserver
    {
        #region Fields

        private readonly IObservable _numberGenerator;

        #endregion

        #region Properties

        public int CountOfNumbersReceived { get; private set; }
        public int CountOfNumbersToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public BaseObserver(IObservable numberGenerator, int countOfNumbersToWaitFor)
        {
            if ( !(countOfNumbersToWaitFor > 0) )
            {
                throw new ArgumentException($"Argument {nameof(countOfNumbersToWaitFor)} ist kleiner <= 0!");
            }

            _numberGenerator = numberGenerator;
            CountOfNumbersToWaitFor = countOfNumbersToWaitFor;

            _numberGenerator.Attach(this);
        }

        #endregion

        #region Methods

        #region IObserver Members

        /// <summary>
        /// Wird aufgerufen wenn der NumberGenerator eine neue Zahl generiert hat.
        /// </summary>
        /// <param name="number"></param>
        public virtual void OnNextNumber(int number)
        {
            CountOfNumbersReceived++;

            // Sobald die Anzahl der max. Beobachtungen (_countOfNumbersToWaitFor) erreicht ist -> Detach()
            if (CountOfNumbersReceived >= CountOfNumbersToWaitFor)
            { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Received '{CountOfNumbersReceived}' of '{CountOfNumbersToWaitFor}' => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
                DetachFromNumberGenerator();
            }

        }

        #endregion

        public override string ToString()
        {
            StringBuilder strgb = new StringBuilder();
            strgb.Append($"{nameof(BaseObserver)} ");
            strgb.Append($"[{nameof(CountOfNumbersReceived)}='{CountOfNumbersReceived}', ");
            strgb.Append($"{nameof(CountOfNumbersToWaitFor)}='{CountOfNumbersToWaitFor}']");

            return strgb.ToString();
        }

        protected void DetachFromNumberGenerator()
        {
            _numberGenerator.Detach(this);
        }

        #endregion
    }
}
