// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

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

		Assert.IsTrue(result);
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

		Assert.IsTrue(result);
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

		Assert.IsFalse(result);
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

		Assert.IsTrue(result);
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

		Assert.IsTrue(result);
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

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethodThrowsOnAmbiguousMatchForOverloadedMethod()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "OverloadedMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.ThrowsExactly<AmbiguousMatchException>(() => type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo));
	}

	[TestMethod]
	public void TryFindMethodFindsGenericMethod()
	{
		Type type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "GenericMethod";
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out MethodInfo? methodInfo);

		Assert.IsTrue(result);
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
}
