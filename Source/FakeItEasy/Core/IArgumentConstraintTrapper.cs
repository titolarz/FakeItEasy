﻿namespace FakeItEasy.Core
{
    using System;
    using System.Collections.Generic;

    internal interface IArgumentConstraintTrapper
    {
        IEnumerable<IArgumentConstraint> TrapConstraints(Action actionThatProducesConstraint);
    }

    internal class ArgumentConstraintTrap
        : IArgumentConstraintTrapper
    {
        private static List<IArgumentConstraint> trappedConstraints;
        
        public IEnumerable<IArgumentConstraint> TrapConstraints(Action actionThatProducesConstraint)
        {
            trappedConstraints = new List<IArgumentConstraint>();
            var result = trappedConstraints;

            actionThatProducesConstraint.Invoke();

            trappedConstraints = null;
            
            return result;
        }

        public static void ReportTrappedConstraint(IArgumentConstraint constraint)
        {
            if (trappedConstraints != null)
            {
                trappedConstraints.Add(constraint);
            }
        }
    }
}