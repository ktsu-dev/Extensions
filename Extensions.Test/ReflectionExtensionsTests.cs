#pragma warning disable CA1822 // Mark members as static
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable IDE0060 // Remove unused parameter

namespace ktsu.Extensions.Tests;

using System.Reflection;
using ktsu.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		var derivedClass = new DerivedClass();
		derivedClass.DerivedMethod();
		Assert.AreEqual(nameof(DerivedClass.DerivedMethod), derivedClass.Value);
		derivedClass.BaseMethod();
		Assert.AreEqual(nameof(BaseClass.BaseMethod), derivedClass.Value);
	}

	[TestMethod]
	public void TryFindMethod_FindsMethodInDerivedClass()
	{
		var type = typeof(DerivedClass);
		string methodName = "DerivedMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethod_FindsMethodInBaseClass()
	{
		var type = typeof(DerivedClass);
		string methodName = "BaseMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethod_ReturnsFalseIfMethodNotFound()
	{
		var type = typeof(DerivedClass);
		string methodName = "NonExistentMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsFalse(result);
		Assert.IsNull(methodInfo);
	}

	[TestMethod]
	public void TryFindMethod_ThrowsOnNullType()
	{
		Type type = null!;
		string methodName = "SomeMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.ThrowsException<ArgumentNullException>(() => type.TryFindMethod(methodName, bindingFlags, out _));
	}

	[TestMethod]
	public void TryFindMethod_ThrowsOnNullOrEmptyMethodName()
	{
		var type = typeof(DerivedClass);
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.ThrowsException<ArgumentNullException>(() => type.TryFindMethod(null!, bindingFlags, out _));
		Assert.ThrowsException<ArgumentException>(() => type.TryFindMethod(string.Empty, bindingFlags, out _));
	}

	// Additional tests for edge cases and scenarios

	[TestMethod]
	public void TryFindMethod_FindsPrivateMethod()
	{
		var type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "PrivateMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethod_FindsStaticMethod()
	{
		var type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "StaticMethod";
		var bindingFlags = BindingFlags.Static | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethod_FindsMethodWithParameters()
	{
		var type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "MethodWithParameters";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	[TestMethod]
	public void TryFindMethod_ThrowsOnAmbiguousMatchForOverloadedMethod()
	{
		var type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "OverloadedMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		Assert.ThrowsException<AmbiguousMatchException>(() => type.TryFindMethod(methodName, bindingFlags, out var methodInfo));
	}

	[TestMethod]
	public void TryFindMethod_FindsGenericMethod()
	{
		var type = typeof(DerivedClassWithAdditionalMethods);
		string methodName = "GenericMethod";
		var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

		bool result = type.TryFindMethod(methodName, bindingFlags, out var methodInfo);

		Assert.IsTrue(result);
		Assert.IsNotNull(methodInfo);
		Assert.AreEqual(methodName, methodInfo.Name);
	}

	// Helper methods for additional tests

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
