namespace ktsu.io.Extensions.Tests;

using System.Reflection;
using ktsu.io.Extensions;
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
}
