﻿using System;
using System.Reflection;
using Ninject.Planning.Directives;
using Ninject.Selection;

namespace Ninject.Planning.Strategies
{
	public class ConstructorReflectionStrategy : IPlanningStrategy
	{
		public ISelector Selector { get; set; }

		public ConstructorReflectionStrategy(ISelector selector)
		{
			Selector = selector;
		}

		public void Execute(IPlan plan)
		{
			ConstructorInfo constructor = Selector.SelectConstructor(plan.Type);

			if (constructor != null)
				plan.Add(new ConstructorInjectionDirective(constructor));
		}
	}
}