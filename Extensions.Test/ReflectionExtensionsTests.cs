// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.Extensions.Tests;

using System.Reflection;

[TestClass]
public class ReflectionExtensionsTests
{
	public class BaseClass
	{
		public string Value { get; set; } = string.Empty;

		public void BaseMethod()
		{
			Value = nameof(BaseMethod);
		}
	}

	public class DerivedClass : BaseClass
	{
		public void DerivedMethod()
		{
			Value = nameof(DerivedMethod);
		}
	}

	[TestMethod]
	public void TestBaseClassDerivedClass()
	{
		DerivedClass derivedClass = new();
		derivedClass.DerivedMethod();
		Assert.AreEqual(nameof(DerivedClass.DerivedMethod), derivedClass.Value);
		derivedClass.BaseMethod();
		Assert.AreEqual(nameof(BaseClass.BaseMethod), derivedClass.Value);
	}

	[TestMethod]
	public void TryFindMethodFindsMethodInDerivedClass()
	{
		Type type = typeof(DerivedClass);
		string methodName = "DerivedMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true when the method exists in the derived class.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodFindsMethodInBaseClass()
	{
		Type type = typeof(DerivedClass);
		string methodName = "BaseMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true when the method exists in the base class.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodReturnsFalseIfMethodNotFound()
	{
		Type type = typeof(DerivedClass);
		string methodName = "NonExistentMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsFalse(result, "TryFindMethod should return false when the method does not exist.");
		Assert.IsNull(methodInfo);
	}

	[TestMethod]
	public void TryFindMethodThrowsOnNullType()
	{
		Type type = null!;
		string methodName = "SomeMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.ThrowsExactly<ArgumentNullException>(() => type.TryFindMethod(methodName, bindingFlags, out _));
	}

	[TestMethod]
	public void TryFindMethodThrowsOnNullOrEmptyMethodName()
	{
		Type type = typeof(DerivedClass);
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.ThrowsExactly<ArgumentNullException>(() => type.TryFindMethod(null!, bindingFlags, out _));
		Assert.ThrowsExactly<ArgumentException>(() => type.TryFindMethod(string.Empty, bindingFlags, out _));
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void TryFindMethodFindsPrivateMethod()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "PrivateMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true when the private method exists.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodFindsStaticMethod()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "StaticMethod";
		BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true when the static method exists.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodFindsMethodWithParameters()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "MethodWithParameters";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true when the method with parameters exists.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodPicksFewestParametersForOverloadedMethod()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "OverloadedMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true for an overloaded method name.");
		Assert.IsNotNull(methodInfo);
		Assert.IsEmpty(methodInfo.GetParameters());
	}

	[TestMethod]
	public void TryFindMethodDoesNotThrowForOverloadedFrameworkMethod()
	{
		bool result = typeof(string).TryFindMethod(nameof(string.Split), BindingFlags.Instance | BindingFlags.Public, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true for string.Split.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(nameof(string.Split), methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodFindsOverloadedPrivateMethodInBaseClass()
	{
		Type type = typeof(DerivedFromPrivateOverloads);
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

		bool result = type.TryFindMethod("Foo", bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true for an overloaded private method on a base class.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(typeof(BaseWithPrivateOverloads), methodInfo.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodResolvesOverloadsTheSameWayEveryTime()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		typeof(string).TryFindMethod(nameof(string.Split), bindingFlags, out MethodInfo? first);
		typeof(string).TryFindMethod(nameof(string.Split), bindingFlags, out MethodInfo? second);

		Assert.AreEqual(first, second);
	}

	[TestMethod]
	public void TryFindMethodFindsMethodInheritedFromBaseInterface()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = typeof(IDerivedInterface).TryFindMethod(nameof(IBaseInterface.BaseOp), bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should find a method declared on a base interface.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(typeof(IBaseInterface), methodInfo.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodFindsMethodTwoInterfaceLevelsUp()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = typeof(IGrandchildInterface).TryFindMethod(nameof(IBaseInterface.BaseOp), bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should find a method declared two interface levels up.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(typeof(IBaseInterface), methodInfo.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodPrefersDirectParentInterfaceOverMoreDistantOne()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = typeof(IDistanceDerived).TryFindMethod(nameof(IDistanceSibling.Foo), bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(typeof(IDistanceSibling), methodInfo.DeclaringType, "A direct parent interface should be searched before one two levels up.");
		Assert.IsEmpty(methodInfo.GetParameters());
	}

	[TestMethod]
	public void TryFindMethodInterfaceResultDoesNotChangeWhenAParentGainsABase()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		typeof(IDistanceDerived).TryFindMethod(nameof(IDistanceSibling.Foo), bindingFlags, out MethodInfo? withoutBase);
		typeof(IDistanceDerivedWithSiblingBase).TryFindMethod(nameof(IDistanceSiblingWithBase.Foo), bindingFlags, out MethodInfo? withBase);

		Assert.IsNotNull(withoutBase);
		Assert.IsNotNull(withBase);
		Assert.AreEqual(typeof(IDistanceSibling), withoutBase.DeclaringType);
		Assert.AreEqual(typeof(IDistanceSiblingWithBase), withBase.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodSearchesSharedInterfaceAncestorAtItsNearestDistance()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = typeof(IDistanceDiamond).TryFindMethod(nameof(IDistanceRoot.Foo), bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(typeof(IDistanceRoot), methodInfo.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodFindsMethodsOnGenericCollectionInterfaces()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.IsTrue(typeof(IList<int>).TryFindMethod(nameof(IList<>.Add), bindingFlags, out MethodInfo? add));
		Assert.AreEqual(typeof(ICollection<int>), add?.DeclaringType);

		Assert.IsTrue(typeof(IList<int>).TryFindMethod(nameof(IList<>.GetEnumerator), bindingFlags, out MethodInfo? getEnumerator));
		Assert.AreEqual(typeof(IEnumerable<int>), getEnumerator?.DeclaringType);

		Assert.IsTrue(typeof(ICollection<int>).TryFindMethod(nameof(ICollection<>.Add), bindingFlags, out MethodInfo? collectionAdd));
		Assert.AreEqual(typeof(ICollection<int>), collectionAdd?.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodFindsGenericMethod()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "GenericMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "TryFindMethod should return true when the generic method exists.");
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodIgnoresCaseWhenBindingFlagsAskForIt()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

		bool result = typeof(string).TryFindMethod("toupperinvariant", bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result, "IgnoreCase should let a lowercase name find ToUpperInvariant.");
		Assert.AreEqual(typeof(string).GetMethod("toupperinvariant", bindingFlags), methodInfo);
	}

	[TestMethod]
	public void TryFindMethodIgnoresCaseForBaseClassMethod()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

		bool result = typeof(DerivedClass).TryFindMethod("basemethod", bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(nameof(BaseClass.BaseMethod), methodInfo.Name);
		Assert.AreEqual(typeof(BaseClass), methodInfo.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodIgnoresCaseForInterfaceAncestorMethod()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

		bool result = typeof(IList<int>).TryFindMethod("add", bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result);
		Assert.AreEqual(typeof(ICollection<int>), methodInfo?.DeclaringType);
	}

	[TestMethod]
	public void TryFindMethodStaysCaseSensitiveWithoutIgnoreCase()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = typeof(string).TryFindMethod("toupperinvariant", bindingFlags, out MethodInfo? methodInfo);

		Assert.IsFalse(result);
		Assert.IsNull(methodInfo);
	}

	[TestMethod]
	public void TryFindMethodIgnoringCasePrefersExactCaseMatch()
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

		Assert.IsTrue(typeof(ClassWithCaseVariants).TryFindMethod("run", bindingFlags, out MethodInfo? lower));
		Assert.IsTrue(typeof(ClassWithCaseVariants).TryFindMethod("Run", bindingFlags, out MethodInfo? upper));
		Assert.IsTrue(typeof(ClassWithCaseVariants).TryFindMethod("RUN", bindingFlags, out MethodInfo? neither));

		Assert.AreEqual("run", lower?.Name, "An exact-case match should win over a closer overload.");
		Assert.AreEqual("Run", upper?.Name);
		Assert.AreEqual("Run", neither?.Name, "With no exact-case match the fewest-parameters rule applies.");
	}

	// Helper methods for additional tests
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Test class")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Test class")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Test class")]
	public class DerivedClassWithAdditionalMethods : DerivedClass
	{
		private void PrivateMethod()
		{
		}

		public static void StaticMethod()
		{
		}

		public void MethodWithParameters(int param1, string param2)
		{
		}

		public void OverloadedMethod()
		{
		}

		public void OverloadedMethod(int param)
		{
		}

		public void GenericMethod<T>(T param)
		{
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Test class")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Test class")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "IDE1006:Naming Styles", Justification = "The lowercase name is the case variant under test")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1708:Identifiers should differ by more than case", Justification = "The case variants are what the tests look up")]
	public class ClassWithCaseVariants
	{
		public void Run()
		{
		}

		public void run(int value)
		{
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Test class")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Test class")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Test class")]
	public class BaseWithPrivateOverloads
	{
		private void Foo(int value)
		{
		}

		private void Foo(string value)
		{
		}
	}

	public class DerivedFromPrivateOverloads : BaseWithPrivateOverloads
	{
	}

	public interface IBaseInterface
	{
		public void BaseOp();
	}

	public interface IDerivedInterface : IBaseInterface
	{
	}

	public interface IGrandchildInterface : IDerivedInterface
	{
	}

	public interface IDistanceRootBase
	{
	}

	public interface IDistanceRoot : IDistanceRootBase
	{
		public void Foo(int x);
	}

	public interface IDistanceBase : IDistanceRoot
	{
	}

	public interface IDistanceSibling
	{
		public void Foo();
	}

	public interface IDistanceDerived : IDistanceBase, IDistanceSibling
	{
	}

	public interface IDistanceSiblingBaseRoot
	{
	}

	public interface IDistanceSiblingBase : IDistanceSiblingBaseRoot
	{
	}

	public interface IDistanceSiblingWithBase : IDistanceSiblingBase
	{
		public void Foo();
	}

	public interface IDistanceDerivedWithSiblingBase : IDistanceBase, IDistanceSiblingWithBase
	{
	}

	public interface IDistanceDiamond : IDistanceBase, IDistanceRoot
	{
	}
}
