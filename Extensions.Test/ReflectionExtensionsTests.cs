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
}
